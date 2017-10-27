using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSO.Demo.Service.Entity;
using SSO.Demo.Service.Service;
using SSO.Demo.Service.Service.Model.MenuService;
using SSO.Demo.Sso.Instrumentation;
using SSO.Demo.Sso.Model.Menu;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Sso.Controllers
{
    public class MenuController : BaseController
    {
        #region 初始化
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        #endregion

        #region 列表页
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var pageList = _menuService.ToList();

            var viewResult = pageList.Select(a => new MenuTableList
            {
                SysMenuId = a.SysMenuId,
                CreateDateTime = a.CreateDateTime,
                MenuName = a.MenuName,
                Url = a.Url,
                Sort = a.Sort
            }).ToList();

            return JsonResult(new PageListResult(viewResult, viewResult.Count));
        }
        #endregion

        #region 编辑
        [HttpGet]
        public IActionResult AddOrEdit(string menuId)
        {
            var menu = _menuService.GetByMenuId(menuId);
            if (menu == null)
                return View();

            var viewModel = menu.ToDto<SysMenu, MenuFormParams>();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(MenuFormParams menuFormParams)
        {
            var result = _menuService.Add(menuFormParams.ToDto<MenuFormParams, MenuAddAndEditModel>());

            return Json(result);
        }

        [HttpPost]
        public IActionResult Edit(MenuFormParams menuFormParams)
        {
            var result = _menuService.Edit(menuFormParams.ToDto<MenuFormParams, MenuAddAndEditModel>());

            return Json(result);
        }
        #endregion

        #region 删除
        [HttpPost]
        public IActionResult Delete(string menuId)
        {
            var result = _menuService.DeleteByMenuId(menuId);

            return Json(result);
        }

        [HttpPost]
        public IActionResult BatchDelete(List<string> menuIds)
        {
            var result = _menuService.BatchDeleteMenuIds(menuIds);

            return Json(result);
        }
        #endregion

        #region 公共方法

        public IActionResult GetMenusForSelect(string value)
        {
            var pageList = _menuService.ToList();

            var viewResult = pageList.Select(a => new SelectListItem
            {
                Value = a.SysMenuId,
                Text = a.MenuName,
                Selected = a.SysMenuId == value
            }).ToList();

            return JsonResult(viewResult);
        }

        #endregion
    }
}