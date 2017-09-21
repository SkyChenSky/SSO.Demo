using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model
{
    public class UserParams
    {
        [Required, StringLength(16)]
        public string UserName { get; set; }

        [Required, StringLength(32)]
        public string Password { get; set; }
    }
}
