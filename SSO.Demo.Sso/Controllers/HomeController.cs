using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Service;
using SSO.Demo.Sso.Instrumentation;
using SSO.Demo.Sso.Model.Home;

namespace SSO.Demo.Sso.Controllers
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