using System.Collections.Generic;
using System.Linq;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Toolkits.Extension
{
    public static class EfExtension
    {
        public static List<T> ToPageList<T, TParam>(this IEnumerable<T> dbContext, PageListParam<TParam> pageListParam) where T : class where TParam : new()
        {
            return dbContext.Skip((pageListParam.Page - 1) * pageListParam.Limit).Take(pageListParam.Limit).ToList();
        }
    }
}
