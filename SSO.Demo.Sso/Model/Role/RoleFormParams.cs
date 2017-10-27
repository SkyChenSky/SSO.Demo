using System.ComponentModel.DataAnnotations;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Sso.Model.Role
{
    public class RoleFormParams
    {
        [Display(Name = "主键"), StringLength(32)]
        public string SysRoleId { get; set; }

        [Display(Name = "角色名"), Required, StringLength(32)]
        public string RoleName { get; set; }

        public bool IsEdit => !SysRoleId.IsNullOrEmpty();
    }
}
