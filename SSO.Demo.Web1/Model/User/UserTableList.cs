using System;
using SSO.Demo.Service.Enums;

namespace SSO.Demo.Web1.Model.User
{
    public class UserTableList
    {
        public string SysUserId { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Email { get; set; }

        public string UserType { get; set; }

        public string Mobile { get; set; }

        public string UserStatus { get; set; }

        public DateTime CreateDateTime { get; set; }
    }

}
