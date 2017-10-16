using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Service.Enums
{
    public enum EUserType
    {
        [Display(Name = "普通管理员")]
        Normal = 0,
        [Display(Name = "超级管理员")]
        Super = 1
    }
}
