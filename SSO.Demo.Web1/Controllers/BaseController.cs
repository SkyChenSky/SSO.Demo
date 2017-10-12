using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Web1.Controllers
{
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
    }
}