using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model.User
{
    public class UserParams
    {
        [Description("主键")]
        public string SysUserId { get; set; }

        [Description("用户名"), Required, StringLength(16)]
        public string UserName { get; set; }

        [Description("密码"), Required]
        public string Password { get; set; }

        [Description("姓名"), Required, StringLength(32)]
        public string RealName { get; set; }

        [Description("Email"), Required, StringLength(32)]
        public string Email { get; set; }

        [Description("手机号"), Required, StringLength(11)]
        public string Mobile { get; set; }
    }
}
