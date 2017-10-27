using System;
using SSO.Demo.Toolkits.Attribute;

namespace SSO.Demo.Web2.Model.Role
{
    public class RoleTableList
    {
        public string SysRoleId { get; set; }

        [TableCols(Tile = "角色名", Width = 180)]
        public string RoleName { get; set; }

        [TableCols(Tile = "角色状态", Width = 100)]
        public string RoleStatus { get; set; }

        [TableCols(Tile = "创建时间", Width = 200)]
        public DateTime CreateDateTime { get; set; }
    }

}
