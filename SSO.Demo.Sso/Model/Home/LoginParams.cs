using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Sso.Model.Home
{
    public class LoginParams
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
