using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;
using SSO.Demo.Web1.Model.User;

namespace SSO.Demo.Web1.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly SkyChenContext _skyChenContext;

        public UserController(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
        }

        public IActionResult Index()
        {
            var userList = _skyChenContext.User.ToList();

            return View(userList);
        }

        public IActionResult List()
        {
            var userList = _skyChenContext.User.ToList();
            return Json(new{ data = userList, count = userList.Count, code = 0,msg = "" } );
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserParams userParams)
        {
            var user = new User
            {
                CreateDateTime = DateTime.Now,
                UserId = Guid.NewGuid().ToString("N"),
                Password = userParams.Password,
                UserName = userParams.UserName
            };

            _skyChenContext.User.Add(user);
            _skyChenContext.SaveChanges();

            return View("Index");
        }
    }
}