using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Demo.Service.Entity
{
    [Table("SYS_MENU"), Description("系统菜单表")]
    public class SysMenu
    {
        [Column("SYS_MENU_ID", TypeName = "char(32)"), Description("主键"), Key, Required, StringLength(32)]
        public string SysMenuId { get; set; }

        [Column("PARENT_ID", TypeName = "char(32)"), Description("父id"), StringLength(32)]
        public string ParentId { get; set; }

        [Column("MENU_NAME", TypeName = "nvarchar(16)"), Description("菜单名称"), Required, StringLength(32)]
        public string MenuName { get; set; }

        [Column("URL", TypeName = "varchar(32)"), Description("url"), Required, StringLength(32)]
        public string Url { get; set; }

        [Column("SORT"), Description("序号"), Required, StringLength(32)]
        public int Sort { get; set; }

        [Column("CREATE_DATETIME", TypeName = "datetime"), Description("创建时间"), Required, StringLength(32)]
        public DateTime CreateDateTime { get; set; }
    }
}
