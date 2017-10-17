using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SSO.Demo.Service.Context;
using SSO.Demo.Service.Entity;
using SSO.Demo.Service.Service.Model.MenuService;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Service.Service
{
    public class MenuService : IService
    {
        #region 初始化
        private readonly SkyChenContext _skyChenContext;
        private readonly DbSet<SysMenu> _sysMenu;

        public MenuService(SkyChenContext skyChenContext)
        {
            _skyChenContext = skyChenContext;
            _sysMenu = _skyChenContext.SysMenu;
        }
        #endregion

        public List<MenuListModel> ToList()
        {
            var menuList = _sysMenu.OrderBy(a => a.Sort).ThenBy(a => a.CreateDateTime).ToList();

            return Loop(menuList, null, 0);
        }

        private List<MenuListModel> Loop(List<SysMenu> sysMenus, string parentId, int lv)
        {
            string menuName = null;
            for (var i = 0; i < lv; i++)
                menuName += "--| ";

            var menuListModel = new List<MenuListModel>();
            foreach (var sysMenu in sysMenus.Where(a => a.ParentId == parentId))
            {
                menuListModel.Add(new MenuListModel
                {
                    Url = sysMenu.Url,
                    SysMenuId = sysMenu.SysMenuId,
                    ParentId = sysMenu.ParentId,
                    MenuName = menuName + sysMenu.MenuName,
                    CreateDateTime = sysMenu.CreateDateTime,
                    Sort = sysMenu.Sort
                });

                menuListModel.AddRange(Loop(sysMenus, sysMenu.SysMenuId, ++lv));
            }

            return menuListModel;
        }

        public SysMenu GetByMenuId(string sysMenuId)
        {
            return sysMenuId.IsNullOrEmpty() ? null : _sysMenu.FirstOrDefault(a => a.SysMenuId == sysMenuId);
        }

        public bool IsExist(string menuName)
        {
            return _sysMenu.Any(a => a.MenuName == menuName);
        }

        public ServiceResult Add(MenuAddAndEditModel model)
        {
            if (IsExist(model.MenuName))
                return ServiceResult.IsFailed("已存在该角色名！");

            model.SysMenuId = GuidHelper.NewOrder().ToString("N");
            var sysMenu = new SysMenu
            {
                CreateDateTime = DateTime.Now,
                MenuName = model.MenuName,
                ParentId = model.ParentId,
                Sort = model.Sort,
                SysMenuId = model.SysMenuId,
                Url = model.Url
            };
            _sysMenu.Add(sysMenu);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("添加成功！") : ServiceResult.IsFailed("添加失败！");
        }

        public ServiceResult Edit(MenuAddAndEditModel model)
        {
            var sysMenu = GetByMenuId(model.SysMenuId);
            sysMenu.MenuName = model.MenuName;
            sysMenu.SysMenuId = model.SysMenuId;
            sysMenu.ParentId = model.ParentId;
            sysMenu.Sort = model.Sort;
            sysMenu.Url = model.Url;

            _sysMenu.Update(sysMenu);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("编辑成功！") : ServiceResult.IsFailed("编辑失败！");
        }

        public ServiceResult DeleteByMenuId(string menuId)
        {
            var menu = _sysMenu.SingleOrDefault(a => a.SysMenuId == menuId);

            if (menu == null)
                return ServiceResult.IsFailed("不存在该菜单！");

            _sysMenu.Remove(menu);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }

        public ServiceResult BatchDeleteMenuIds(List<string> menuIds)
        {
            var menus = _sysMenu.Where(a => menuIds.Contains(a.SysMenuId)).ToList();
            if (!menus.Any())
                return ServiceResult.IsSuccess("请选择需要删除的菜单！");

            _sysMenu.RemoveRange(menus);
            var result = _skyChenContext.SaveChanges() > 0;

            return result ? ServiceResult.IsSuccess("删除成功！") : ServiceResult.IsFailed("删除失败！");
        }
    }
}
