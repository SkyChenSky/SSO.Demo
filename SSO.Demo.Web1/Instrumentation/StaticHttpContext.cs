using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SSO.Demo.Service;
using SSO.Demo.Toolkits;

namespace SSO.Demo.Web1.Instrumentation
{
    public static class StaticHttpContext
    {
        private static IHttpContextAccessor _accessor;

        public static HttpContext Current => _accessor.HttpContext;

        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public static User UserData => Current.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.UserData)?.Value
            .FromJson<User>();

    }
}
