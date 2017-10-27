using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Service;
using SSO.Demo.Service.Service.Model.MenuService;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Web1.Instrumentation;
using SSO.Demo.Web1.Model.Home;

namespace SSO.Demo.Web1.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MenuService _menuService;

        public HomeController(MenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            var scrollMenu = _menuService.ToListForScroll();
            return View(new HomeModel { Menus = scrollMenu });
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}