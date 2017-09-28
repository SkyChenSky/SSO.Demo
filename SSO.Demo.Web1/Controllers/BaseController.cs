using Microsoft.AspNetCore.Mvc;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Web1.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult PageListResult(object data, int totalCount, int code = 0, string msg = "")
        {
            return Json(new PageListResult { Data = data, Count = totalCount, Code = code, Msg = msg });
        }
    }
}