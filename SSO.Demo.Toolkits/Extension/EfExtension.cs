using System.Collections.Generic;
using System.Linq;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Toolkits.Extension
{
    public static class EfExtension
    {
        public static List<T> ToPageList<T>(this IEnumerable<T> dbContext, PageListParam pageListParam) where T : class
        {
            return dbContext.Skip((pageListParam.Page - 1) * pageListParam.Limit).Take(pageListParam.Limit).ToList();
        }
    }
}
