using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model.User
{
    public class UserParams
    {
        [Required]
        public string UserId { get; set; }

        [Required, StringLength(16), Display(Name = "用户名"), Description("请输入用户名")]
        public string UserName { get; set; }

        [Required, StringLength(32), Display(Name = "密码"), Description("请输入密码")]
        public string Password { get; set; }

        [Required]
        public DateTime? CreateDateTime { get; set; }

        public Sex Sex { get; set; }
    }

    public enum Sex
    {
        [Display(Name = "男")]
        Man = 0,
        [Display(Name = "女")]
        Woman = 1
    }
}
