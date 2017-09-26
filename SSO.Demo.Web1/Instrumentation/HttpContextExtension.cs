using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SSO.Demo.Service;
using SSO.Demo.Toolkits;

namespace SSO.Demo.Web1.Instrumentation
{
    public static class HttpContextExtension
    {
        public static User UserData(this HttpContext context)
        {
            return context.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.UserData)?.Value.FromJson<User>();
        }
    }
}
