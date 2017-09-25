using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;

namespace SSO.Demo.Toolkits
{
    public class AuthenticationHelper
    {
        public const string AuthenticationToken = "AuthenticationToken";

        public const string SessionUserId = "UserId";

    }
}
