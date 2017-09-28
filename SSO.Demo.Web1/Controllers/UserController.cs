using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Model;
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

        public IActionResult List(PageListParam<ListParam> pageListParam)
        {

            var userQueryable = _skyChenContext.User.Where(a => true);
            var listParam = pageListParam.Params;

            if (!listParam.UserName.IsNullOrEmpty())
                userQueryable = userQueryable.Where(a => a.UserName.StartsWith(listParam.UserName));

            if (!listParam.UserId.IsNullOrEmpty())
                userQueryable = userQueryable.Where(a => a.UserId == listParam.UserId);

            if (listParam.BeganCreateDateTime.HasValue)
                userQueryable = userQueryable.Where(a => a.CreateDateTime >= listParam.BeganCreateDateTime);

            if (listParam.EndCreateDateTime.HasValue)
                userQueryable = userQueryable.Where(a => a.CreateDateTime <= listParam.EndCreateDateTime);

            var userList = userQueryable.OrderBy(a => a.CreateDateTime).ToPageList(pageListParam);

            return PageListResult(userList, userList.Count);
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