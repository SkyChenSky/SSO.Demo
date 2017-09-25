using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Service;
using SSO.Demo.Toolkits;

namespace SSO.Demo.Web1.Controllers
{
    public class BaseController : Controller
    {

        public User GetLoginUserInfo()
        {
            return HttpContext.User.Claims.FirstOrDefault(a => a.Type.Contains("userdata"))?.Value.FromJson<User>();
        }
    }
}