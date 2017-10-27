using System;
using SSO.Demo.Toolkits.Attribute;

namespace SSO.Demo.Web2.Model.User
{
    public class UserTableList
    {
        public string SysUserId { get; set; }

        [TableCols(Tile = "用户名", Width = 180)]
        public string UserName { get; set; }

        [TableCols(Tile = "姓名")]
        public string RealName { get; set; }

        [TableCols(Tile = "手机号")]
        public string Mobile { get; set; }

        [TableCols(Tile = "电子邮箱", Width = 180)]
        public string Email { get; set; }

        [TableCols(Tile = "用户类型")]
        public string UserType { get; set; }

        [TableCols(Tile = "用户状态")]
        public string UserStatus { get; set; }

        [TableCols(Tile = "创建时间", Width = 200)]
        public DateTime CreateDateTime { get; set; }
    }

}
