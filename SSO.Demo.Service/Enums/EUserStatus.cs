using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Service.Enums
{
    public enum EUserStatus
    {
        [Display(Name = "关闭")]
        Off = 0,
        [Display(Name = "启用")]
        On = 1
    }
}
