using System;
using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Service
{
    public class User
    {
        [Key, StringLength(32)]
        public string UserId { get; set; }

        [Required, StringLength(16)]
        public string UserName { get; set; }

        [Required, StringLength(32)]
        public string Password { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
