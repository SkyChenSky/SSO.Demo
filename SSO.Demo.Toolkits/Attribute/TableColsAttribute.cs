using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Toolkits.Attribute
{
    public class TableColsAttribute : System.Attribute
    {
        public TableColsAttribute()
        {
            Width = 150;
            Align = EAlign.Left;
        }
        public string Field { get; set; }

        public string Tile { get; set; }

        public int Width { get; set; }

        public EAlign Align { get; set; }
    }

    public enum EAlign
    {
        [Display(Name = "center")]
        Center = 0,
        [Display(Name = "right")]
        Right = 1,
        [Display(Name = "left")]
        Left = 2,
    }
}
