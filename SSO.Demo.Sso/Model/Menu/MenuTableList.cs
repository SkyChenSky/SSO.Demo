using System;
using SSO.Demo.Toolkits.Attribute;

namespace SSO.Demo.Sso.Model.Menu
{
    public class MenuTableList
    {
        public string SysMenuId { get; set; }

        [TableCols(Tile = "菜单名称", Width = 200)]
        public string MenuName { get; set; }

        [TableCols(Tile = "Url", Width = 150)]
        public string Url { get; set; }

        [TableCols(Tile = "排序", Width = 100)]
        public int Sort { get; set; }

        [TableCols(Tile = "创建时间", Width = 180)]
        public DateTime CreateDateTime { get; set; }
    }

}
