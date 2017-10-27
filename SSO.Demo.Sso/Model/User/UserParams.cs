using System.ComponentModel.DataAnnotations;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Sso.Model.User
{
    public class UserParams
    {
        [Display(Name = "主键")]
        public string SysUserId { get; set; }

        [Display(Name = "用户名"), Required(ErrorMessage = "姓名必填"), StringLength(16, ErrorMessage = "用户名不大于16字符")]
        public string UserName { get; set; }

        [Display(Name = "密码"), Required(ErrorMessage = "密码必填")]
        public string Password { get; set; }

        [Display(Name = "姓名"), Required(ErrorMessage = "姓名必填"), StringLength(32, ErrorMessage = "姓名不大于32字符")]
        public string RealName { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "Email必填"), StringLength(32, ErrorMessage = "Email不大于32字符")]
        public string Email { get; set; }

        [Display(Name = "手机号"), Required(ErrorMessage = "手机号必填"), StringLength(11, ErrorMessage = "手机号不大于11字符")]
        public string Mobile { get; set; }

        public bool IsEdit => !SysUserId.IsNullOrEmpty();
    }
}
