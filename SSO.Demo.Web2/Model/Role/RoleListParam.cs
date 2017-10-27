using System;
using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web2.Model.Role
{
    public class RoleListParam
    {
        [Display(Name = "角色名")]
        public string RoleName { get; set; }

        [Display(Name = "状态")]
        public int? RoleStatus { get; set; }

        [Display(Name = "开始创建时间")]
        public DateTime? BeganCreateDateTime { get; set; }

        [Display(Name = "结束创建时间")]
        public DateTime? EndCreateDateTime { get; set; }
    }
}
