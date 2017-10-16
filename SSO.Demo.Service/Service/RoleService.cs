using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SSO.Demo.Service.Context;
using SSO.Demo.Service.Entity;
using SSO.Demo.Service.Enums;
using SSO.Demo.Service.Service.Model.RoleService;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Service.Service
{
    public class RoleService : IService
    {
        private readonly SkyChenContext _skyChenContext;

        public RoleService(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
        }

        public PageListResult PageList(Expression<Func<SysRole, bool>> expression, PageListParam pageListParam)
        {
            var roleQueryable = _skyChenContext.SysRole.Where(expression);

            var totalCount = roleQueryable.Count();
            var userList = roleQueryable.OrderBy(a => a.CreateDateTime).ToPageList(pageListParam);

            return new PageListResult(userList, totalCount);
        }

        public SysRole GetByRoleId(string sysRole)
        {
            return sysRole.IsNullOrEmpty() ? null : _skyChenContext.SysRole.FirstOrDefault(a => a.SysRoleId == sysRole);
        }

        public bool IsExist(string roleName)
        {
            return _skyChenContext.SysRole.Any(a => a.RoleName == roleName);
        }

        public ServiceResult Add(RoleAddAndEditModel model)
        {
            if (IsExist(model.RoleName))
                return ServiceResult.IsFailed("已存在该角色名！");

            var sysUserId = GuidHelper.NewOrder().ToString("N");
            var sysUser = new SysRole
            {
                CreateDateTime = DateTime.Now,
                RoleStatus = (int)ERoleStatus.On,
                SysRoleId = sysUserId,
                RoleName = model.RoleName
            };
            _skyChenContext.SysRole.Add(sysUser);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("添加成功！") : ServiceResult.IsFailed("添加失败！");
        }

        public ServiceResult Edit(RoleAddAndEditModel model)
        {
            var sysRole = GetByRoleId(model.SysRoleId);
            sysRole.RoleName = model.RoleName;

            _skyChenContext.SysRole.Update(sysRole);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("编辑成功！") : ServiceResult.IsFailed("编辑失败！");
        }

        public ServiceResult DeleteByRoleId(string roleId)
        {
            var role = _skyChenContext.SysRole.SingleOrDefault(a => a.SysRoleId == roleId);

            if (role == null)
                return ServiceResult.IsFailed("不存在该角色！");

            _skyChenContext.SysRole.Remove(role);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }

        public ServiceResult BatchDeleteRoleIds(List<string> roleIds)
        {
            var roles = _skyChenContext.SysRole.Where(a => roleIds.Contains(a.SysRoleId)).ToList();
            if (!roles.Any())
                return ServiceResult.IsSuccess("请选择需要删除的用户！");

            _skyChenContext.SysRole.RemoveRange(roles);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }
    }
}
