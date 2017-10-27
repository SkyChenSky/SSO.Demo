using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service.Entity;
using SSO.Demo.Service.Enums;
using SSO.Demo.Service.Service;
using SSO.Demo.Service.Service.Model.RoleService;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;
using SSO.Demo.Web2.Instrumentation;
using SSO.Demo.Web2.Model.Role;

namespace SSO.Demo.Web2.Controllers
{
    public class RoleController : BaseController
    {
        #region 初始化
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        #region 列表页
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(PageListParam<RoleListParam> pageListParam)
        {
            var where = ExpressionBuilder.True<SysRole>();
            var listParam = pageListParam.Params;

            if (!listParam.RoleName.IsNullOrEmpty())
                where = where.And(a => a.RoleName.StartsWith(listParam.RoleName));

            if (listParam.EndCreateDateTime.HasValue)
                where = where.And(a => a.CreateDateTime >= listParam.BeganCreateDateTime.Value);

            if (listParam.BeganCreateDateTime.HasValue)
                where = where.And(a => a.CreateDateTime <= listParam.EndCreateDateTime.Value);

            if (listParam.RoleStatus.HasValue)
                where = where.And(a => a.RoleStatus == listParam.RoleStatus.Value);

            var result = _roleService.PageList(where, pageListParam);
            result.Data = ((List<SysRole>)result.Data).Select(a => new RoleTableList
            {
                SysRoleId = a.SysRoleId,
                CreateDateTime = a.CreateDateTime,
                RoleName = a.RoleName,
                RoleStatus = ((ERoleStatus)a.RoleStatus).GetDisplayName()
            }).ToList();

            return PageListResult(result);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public IActionResult AddOrEdit(string roleId)
        {
            var user = _roleService.GetByRoleId(roleId);
            if (user == null)
                return View();

            var viewModel = user.ToDto<SysRole, RoleFormParams>();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(RoleFormParams roleFormParams)
        {
            var result = _roleService.Add(roleFormParams.ToDto<RoleFormParams, RoleAddAndEditModel>());

            return Json(result);
        }

        [HttpPost]
        public IActionResult Edit(RoleFormParams roleFormParams)
        {
            var result = _roleService.Edit(roleFormParams.ToDto<RoleFormParams, RoleAddAndEditModel>());

            return Json(result);
        }
        #endregion

        #region 删除
        [HttpPost]
        public IActionResult Delete(string roleId)
        {
            var result = _roleService.DeleteByRoleId(roleId);

            return Json(result);
        }

        [HttpPost]
        public IActionResult BatchDelete(List<string> roleIds)
        {
            var result = _roleService.BatchDeleteRoleIds(roleIds);

            return Json(result);
        }
        #endregion
    }
}