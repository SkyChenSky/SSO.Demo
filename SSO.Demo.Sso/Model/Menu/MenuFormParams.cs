using System.ComponentModel.DataAnnotations;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Sso.Model.Menu
{
    public class MenuFormParams
    {
        [Display(Name = "主键"), StringLength(32)]
        public string SysMenuId { get; set; }

        [Display(Name = "菜单名"), Required, StringLength(32)]
        public string MenuName { get; set; }

        [Display(Name = "Url"), Required, StringLength(32)]
        public string Url { get; set; }

        [Display(Name = "父菜单"), StringLength(32)]
        public string ParentId { get; set; }

        [Display(Name = "排序"), Required]
        public int Sort { get; set; }

        public bool IsEdit => !SysMenuId.IsNullOrEmpty();
    }
}
