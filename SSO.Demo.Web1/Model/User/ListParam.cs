using System;
using System.ComponentModel.DataAnnotations;
using SSO.Demo.Service.Enums;

namespace SSO.Demo.Web1.Model.User
{
    public class ListParam
    {
        [Display(Name = "主键")]
        public string UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "手机号")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public int? UserType { get; set; }

        public int? UserStatus { get; set; }

        [Display(Name = "开始创建时间")]
        public DateTime? BeganCreateDateTime { get; set; }

        [Display(Name = "结束创建时间")]
        public DateTime? EndCreateDateTime { get; set; }
    }
}
