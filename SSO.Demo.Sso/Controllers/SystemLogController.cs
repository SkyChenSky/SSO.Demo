using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Sso.Instrumentation;

namespace SSO.Demo.Sso.Controllers
{
    public class SystemLogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}