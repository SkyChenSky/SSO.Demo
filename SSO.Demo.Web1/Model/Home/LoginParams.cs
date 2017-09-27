using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model.Home
{
    public class LoginParams
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
