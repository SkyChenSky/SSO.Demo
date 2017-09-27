using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Toolkits.Extension
{
    public static class HttpContextExtension
    {
        public static LoginUser LoginUserData(this HttpContext context)
        {
            return context.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.UserData)?.Value.FromJson<LoginUser>();
        }
    }
}
