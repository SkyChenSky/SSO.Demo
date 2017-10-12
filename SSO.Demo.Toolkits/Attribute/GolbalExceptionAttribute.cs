using Microsoft.AspNetCore.Mvc.Filters;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Toolkits.Attribute
{
    public class GolbalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception.GetDeepestException();

                exception.WriteToFile("全局异常捕抓");

                //todo配置控制
                exception.WriteToMongo(new ExceptionMsg());
            }

            base.OnException(context);
        }
    }
}
