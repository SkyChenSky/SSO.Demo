using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Demo.Service.Entity
{
    [Table("SYS_ROLE"), Description("系统角色表")]
    public class SysRole
    {
        [Column("SYS_ROLEID", TypeName = "char(32)"), Description("主键"), Key, Required, StringLength(32)]
        public string SysRoleId { get; set; }

        [Column("ROLE_NAME", TypeName = "nvarchar(16)"), Description("主键"), Required, StringLength(32)]
        public string RoleName { get; set; }

        [Column("ROLE_STATUS"), Description("状态"), Required]
        public int RoleStatus { get; set; }

        [Column("CREATE_DATETIME", TypeName = "datetime"), Description("创建时间"), Required]
        public DateTime CreateDateTime { get; set; }
    }
}
