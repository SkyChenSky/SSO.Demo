using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Service;
using SSO.Demo.Web2.Instrumentation;
using SSO.Demo.Web2.Model.Home;

namespace SSO.Demo.Web2.Controllers
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