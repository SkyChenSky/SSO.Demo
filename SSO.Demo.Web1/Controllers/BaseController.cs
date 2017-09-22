using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSO.Demo.Web1.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewData["UserName"] = HttpContext?.User?.Identity?.Name;
        }
    }
}