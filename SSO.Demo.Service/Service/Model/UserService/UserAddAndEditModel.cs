using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Service.Service.Model.UserService
{
    public class UserAddAndEditModel
    {
        [Display(Name = "主键")]
        public string SysUserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "手机号")]
        public string Mobile { get; set; }
    }
}
