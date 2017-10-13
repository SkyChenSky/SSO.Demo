using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model.User
{
    public class UserParams
    {
        [Display(Name = "主键")]
        public string SysUserId { get; set; }

        [Display(Name = "用户名"), Required, StringLength(16)]
        public string UserName { get; set; }

        [Display(Name = "密码"), Required]
        public string Password { get; set; }

        [Display(Name = "姓名"), Required, StringLength(32)]
        public string RealName { get; set; }

        [Display(Name = "Email"), Required, StringLength(32)]
        public string Email { get; set; }

        [Display(Name = "手机号"), Required, StringLength(11)]
        public string Mobile { get; set; }
    }
}
