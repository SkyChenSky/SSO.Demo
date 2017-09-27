using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Toolkits.Attribute
{
    public class GobalMvcModelValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var builder = new StringBuilder();

            if (!context.ModelState.IsValid)
            {
                foreach (var key in context.ModelState.Keys)
                {
                    var errors = context.ModelState[key].Errors;

                    foreach (var error in errors)
                    {
                        if (builder.Length > 0)
                            builder.Append("<br/>");

                        builder.Append(error.ErrorMessage);
                    }
                }
                context.Result = new ContentResult
                {
                    Content = ServiceResult.IsFailed(builder.ToString()).ToJson(),
                    ContentType = "application/json"
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
