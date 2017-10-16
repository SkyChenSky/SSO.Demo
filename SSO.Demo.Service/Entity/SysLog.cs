using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Demo.Service.Entity
{
    [Table("SYS_LOG"), Description("系统权限日志表")]
    public class SysLog
    {
        [Column("SYS_LOG_ID"), Description("主键"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Required]
        public int SysLogId { get; set; }

        [Column("SYSTEM_ID", TypeName = "char(32)"), Description("系统主键"), Required, StringLength(32)]
        public string SystemId { get; set; }

        [Column("OPERATE", TypeName = "nvarchar(16)"), Description("动作"), Required, StringLength(32)]
        public string Operate { get; set; }

        [Column("REMARK", TypeName = "nvarchar(32)"), Description("备注"), Required, StringLength(64)]
        public string Remark { get; set; }

        [Column("CREATE_USERNAME", TypeName = "varchar(16)"), Description("创建人"), Required, StringLength(16)]
        public string CreateUserName { get; set; }

        [Column("CREATE_DATETIME", TypeName = "datetime"), Description("创建时间"), Required]
        public DateTime CreateDateTime { get; set; }
    }
}
