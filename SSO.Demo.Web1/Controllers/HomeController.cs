using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;

namespace SSO.Demo.Web1.Controllers
{
    public class HomeController : Controller
    {
        private readonly EfDbContext _context;

        public HomeController(EfDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}