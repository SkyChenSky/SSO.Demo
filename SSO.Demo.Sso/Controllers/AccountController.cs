using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Service;
using SSO.Demo.Sso.Instrumentation;
using SSO.Demo.Sso.Model.Home;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Sso.Controllers
{
    public class AccountController : BaseController
    {
        #region 初始化
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region 登录
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginParams loginParams)
        {
            var result = _userService.CheckPassword(loginParams.UserName, loginParams.Password);

            if (result.TData != null)
            {
                SignIn(new LoginUser
                {
                    LoginDateTime = DateTime.Now,
                    UserId = result.TData.SysUserId,
                    UserName = result.TData.UserName
                });
            }

            return Json(result);
        }
        #endregion

        #region 注销
        public IActionResult Logout()
        {
            SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region 私有方法
        private void SignIn(LoginUser loginUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginUser.UserId),
                new Claim(ClaimTypes.UserData,loginUser.ToJson()),
                new Claim("Salt",Guid.NewGuid().ToString("N"))
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Basic"));

            HttpContext.SignInAsync(AuthenticationHelper.AuthenticationToken, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = true
            });
        }

        private void SignOut()
        {
            HttpContext.SignOutAsync(AuthenticationHelper.AuthenticationToken);
        }
        #endregion
    }
}