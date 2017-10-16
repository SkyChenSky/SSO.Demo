using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Toolkits.Enums
{
    public enum EInputDisplay
    {
        [Display(Name = "layui-input-block")]
        LayuiInputBlock = 0,
        [Display(Name = "layui-input-inline")]
        LayuiInputInline = 1,
        [Display(Name = "layui-inline")]
        LayuiInline = 1
    }
}
