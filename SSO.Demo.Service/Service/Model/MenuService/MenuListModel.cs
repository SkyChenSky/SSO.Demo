using System;
using System.Collections.Generic;

namespace SSO.Demo.Service.Service.Model.MenuService
{
    public class MenuListModel
    {
        public string SysMenuId { get; set; }

        public string ParentId { get; set; }

        public string MenuName { get; set; }

        public string Url { get; set; }

        public int Sort { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
