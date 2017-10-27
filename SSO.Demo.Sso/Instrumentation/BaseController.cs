using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Sso.Instrumentation
{
    [Authorize]
    public class BaseController : Controller
    {
        public JsonResult PageListResult(PageListResult pageListResult)
        {
            return Json(pageListResult, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }

        public JsonResult JsonResult(object data)
        {
            return Json(data, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });
        }

    }
}