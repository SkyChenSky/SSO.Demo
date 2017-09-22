using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SSO.Demo.Web1
{
    public class FormsAuthTicketDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _authenticationScheme;

        public FormsAuthTicketDataFormat(string authenticationScheme)
        {
            _authenticationScheme = authenticationScheme;
        }

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            //Get FormsAuthenticationTicket from asp.net web api
            var formsAuthTicket = GetFormsAuthTicket(protectedText);
            var name = "";//formsAuthTicket.Name;
            DateTime issueDate = formsAuthTicket.IssueDate;
            DateTime expiration = formsAuthTicket.Expiration;

            //Create AuthenticationTicket
            var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) }, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = issueDate,
                ExpiresUtc = expiration
            };
            var ticket = new AuthenticationTicket(claimsPrincipal, authProperties, _authenticationScheme);
            return ticket;
        }

        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }

        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException();
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}