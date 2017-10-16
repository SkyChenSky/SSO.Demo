using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Demo.Service.Entity
{
    [Table("SYS_USER"), Description("系统用户表")]
    public class SysUser
    {
        [Column("SYS_USERID", TypeName = "char(32)"), Description("主键"), Key, Required, StringLength(32)]
        public string SysUserId { get; set; }

        [Column("USER_NAME", TypeName = "varchar(16)"), Description("用户名"), Required, StringLength(16)]
        public string UserName { get; set; }

        [Column("PASSWORD", TypeName = "varchar(32)"), Description("密码"), Required, StringLength(32)]
        public string Password { get; set; }

        [Column("REAL_NAME", TypeName = "nvarchar(16)"), Description("姓名"), Required, StringLength(32)]
        public string RealName { get; set; }

        [Column("EMAIL", TypeName = "varchar(32)"), Description("Email"), Required, StringLength(32)]
        public string Email { get; set; }

        [Column("USER_TYPE"), Description("用户类型"), Required]
        public int UserType { get; set; }

        [Column("MOBILE", TypeName = "char(11)"), Description("手机号"), Required, StringLength(11)]
        public string Mobile { get; set; }

        [Column("USER_STATUS"), Description("状态"), Required]
        public int UserStatus { get; set; }

        [Column("CREATE_DATETIME", TypeName = "datetime"), Description("创建时间"), Required]
        public DateTime CreateDateTime { get; set; }
    }
}
