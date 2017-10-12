using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Model;
using SSO.Demo.Service.Service;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;
using SSO.Demo.Web1.Model.User;

namespace SSO.Demo.Web1.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(PageListParam<ListParam> pageListParam)
        {
            var where = ExpressionBuilder.True<User>();
            var listParam = pageListParam.Params;

            if (!listParam.UserName.IsNullOrEmpty())
                where = where.And(a => a.UserName.StartsWith(listParam.UserName));

            if (!listParam.UserId.IsNullOrEmpty())
                where = where.And(a => a.UserId == listParam.UserId);

            if (listParam.BeganCreateDateTime.HasValue)
                where = where.And(a => a.CreateDateTime >= listParam.BeganCreateDateTime);

            if (listParam.EndCreateDateTime.HasValue)
                where = where.And(a => a.CreateDateTime <= listParam.EndCreateDateTime);

            var result = _userService.PageList(where, pageListParam);

            return PageListResult(result);
        }

        [HttpGet]
        public IActionResult Add(string userId)
        {
            var user = _userService.GetByUserId(userId);
            if (user == null)
                return View();

            var viewModel = new UserParams
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                CreateDateTime = new DateTime(2013, 10, 10),
                Sex = Sex.Woman
            };
            return View(viewModel);
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

            var result = _userService.Add(user);

            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var result = _userService.DeleteByUserId(userId);

            return Json(result);
        }

        [HttpPost]
        public IActionResult BatchDelete(List<string> userIds)
        {
            var result = _userService.BatchDeleteUserIds(userIds);

            return Json(result);
        }
    }
}