using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Service;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;
using SSO.Demo.Web2.Model.Home;

namespace SSO.Demo.Web2.Controllers
{
    public class SsoController : Controller
    {
        private readonly UserService _userService;

        public SsoController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult LogOn(LoginParams loginParams)
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
    }
}