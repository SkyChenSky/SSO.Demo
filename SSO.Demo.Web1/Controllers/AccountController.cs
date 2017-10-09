using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;
using SSO.Demo.Web1.Model.Home;

namespace SSO.Demo.Web1.Controllers
{
    public class AccountController : BaseController
    {
        #region 初始化
        private readonly SkyChenContext _skyChenContext;

        public AccountController(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
        } 
        #endregion

        #region 登录
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginParams loginParams)
        {
            var loginSuccessUser = _skyChenContext.User.FirstOrDefault(a =>
                a.UserName == loginParams.UserName && a.Password == loginParams.Password);

            if (loginSuccessUser != null)
            {
                SignIn(new LoginUser { LoginDateTime = DateTime.Now, UserId = loginSuccessUser.UserId, UserName = loginSuccessUser.UserName });

                return Json(ServiceResult.IsSuccess("登录成功"));
            }

            return Json(ServiceResult.IsFailed("帐号或密码错误"));
        }
        #endregion

        #region 注销
        [Authorize]
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