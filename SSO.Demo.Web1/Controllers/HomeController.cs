using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;
using SSO.Demo.Toolkits;
using SSO.Demo.Web1.Model;

namespace SSO.Demo.Web1.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SkyChenContext _skyChenContext;

        public HomeController(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
        }

        public IActionResult Index()
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
                SignIn(loginSuccessUser);

                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "Home");
        }

        private void SignIn(User loginSuccessUser)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId",loginSuccessUser.UserId),
                new Claim(ClaimTypes.Name,loginSuccessUser.UserName),
                new Claim("LoginDateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new Claim(ClaimTypes.UserData,loginSuccessUser.ToJson())
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

        [Authorize]
        public IActionResult Logout()
        {
            SignOut();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}