using System.ComponentModel.DataAnnotations;

namespace SSO.Demo.Web1.Model.User
{
    public class UserParams
    {
        [Required]
        public string UserId { get; set; }

        [Required, StringLength(16)]
        public string UserName { get; set; }

        [Required, StringLength(32)]
        public string Password { get; set; }
    }
}
