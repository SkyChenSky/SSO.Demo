using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Context;
using SSO.Demo.Service.Model;
using SSO.Demo.Service.Service;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Model;
using SSO.Demo.Web1.Model.User;

namespace SSO.Demo.Web1.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly SkyChenContext _skyChenContext;
        private readonly UserService _userService;

        public UserController(SkyChenContext skyChenContext, UserService userService)
        {
            _skyChenContext = skyChenContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
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

            var totalCount = userQueryable.Count();
            var userList = userQueryable.OrderBy(a => a.CreateDateTime).ToPageList(pageListParam);

            return PageListResult(userList, totalCount);
        }

        [HttpGet]
        public IActionResult Add(string userId)
        {
            if (!userId.IsNullOrEmpty())
            {
                var user = _skyChenContext.User.FirstOrDefault(a => a.UserId == userId);
                if (user != null)
                {
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
            }

            return View(new UserParams
            {
                CreateDateTime = new DateTime(2013, 10, 10),
                Sex = Sex.Woman
            });
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
            var result = _skyChenContext.SaveChanges() > 0;

            return Json(result ? ServiceResult.IsSuccess("添加成功") : ServiceResult.IsFailed("添加失败"));
        }

        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var result = _userService.Delete(userId);

            return Json(result);
        }

        [HttpPost]
        public IActionResult BatchDelete(List<string> userIds)
        {
            var users = _skyChenContext.User.Where(a => userIds.Contains(a.UserId)).ToList();

            _skyChenContext.User.RemoveRange(users);
            var result = _skyChenContext.SaveChanges() > 0;

            return Json(result ? ServiceResult.IsSuccess("删除成功") : ServiceResult.IsFailed("删除失败"));
        }
    }
}