using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SSO.Demo.Service.Context;
using SSO.Demo.Service.Entity;
using SSO.Demo.Service.Enums;
using SSO.Demo.Service.Service.Model.UserService;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Service.Service
{
    public class UserService : IService
    {
        private readonly SkyChenContext _skyChenContext;

        public UserService(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
        }

        public PageListResult PageList(Expression<Func<SysUser, bool>> expression, PageListParam pageListParam)
        {
            var userQueryable = _skyChenContext.SysUser.Where(expression);

            var totalCount = userQueryable.Count();
            var userList = userQueryable.OrderBy(a => a.CreateDateTime).ToPageList(pageListParam);

            return new PageListResult(userList, totalCount);
        }

        public SysUser GetByUserId(string userId)
        {
            return userId.IsNullOrEmpty() ? null : _skyChenContext.SysUser.FirstOrDefault(a => a.SysUserId == userId);
        }

        public bool IsExist(string userName)
        {
            return _skyChenContext.SysUser.Any(a => a.UserName == userName);
        }

        public ServiceResult Add(UserAddAndEditModel model)
        {
            if (IsExist(model.UserName))
                return ServiceResult.IsFailed("已存在该用户名！");

            var sysUserId = GuidHelper.NewOrder().ToString("N");
            var sysUser = new SysUser
            {
                CreateDateTime = DateTime.Now,
                SysUserId = sysUserId,
                Password = EncryptPassword(model.Password, model.SysUserId),
                UserName = model.UserName,
                Email = model.Email,
                Mobile = model.Mobile,
                RealName = model.RealName,
                UserStatus = (int)EUserStatus.On,
                UserType = (int)EUserType.Normal
            };
            _skyChenContext.SysUser.Add(sysUser);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("添加成功！") : ServiceResult.IsFailed("添加失败！");
        }

        public ServiceResult Edit(UserAddAndEditModel model)
        {
            var sysUser = GetByUserId(model.SysUserId);

            sysUser.Email = model.Email;
            sysUser.Password = EncryptPassword(model.Password, model.SysUserId);
            sysUser.Mobile = model.Mobile;
            sysUser.RealName = model.RealName;

            _skyChenContext.SysUser.Update(sysUser);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("编辑成功！") : ServiceResult.IsFailed("编辑失败！");
        }

        public ServiceResult DeleteByUserId(string userId)
        {
            var user = _skyChenContext.SysUser.SingleOrDefault(a => a.SysUserId == userId);

            if (user == null)
                return ServiceResult.IsFailed("不存在改用户！");

            _skyChenContext.SysUser.Remove(user);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }

        public ServiceResult BatchDeleteUserIds(List<string> userIds)
        {
            var users = _skyChenContext.SysUser.Where(a => userIds.Contains(a.SysUserId)).ToList();
            if (!users.Any())
                return ServiceResult.IsSuccess("请选择需要删除的用户！");

            _skyChenContext.SysUser.RemoveRange(users);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }

        public ServiceResult<SysUser> CheckPassword(string userName, string password)
        {
            var sysUser = _skyChenContext.SysUser.FirstOrDefault(a =>
                a.UserName == userName);

            if (sysUser != null && sysUser.Password == EncryptPassword(password, sysUser.SysUserId))
            {
                return ServiceResult<SysUser>.IsSuccess("登录成功", sysUser);
            }

            return ServiceResult<SysUser>.IsFailed("帐号或密码错误", sysUser);
        }

        private static string EncryptPassword(string password, string salt)
        {
            return (password.EncodeMd5String() + salt).EncodeMd5String();
        }
    }
}
