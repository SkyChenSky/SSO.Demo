using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSO.Demo.Toolkits.Enums;
using SSO.Demo.Web1.Model.Home;

namespace SSO.Demo.Web1.Instrumentation
{
    public static class UiExtension
    {
        public static IHtmlContent Scoll(this IHtmlHelper helper, List<HomeMenuModel> menus)
        {
            var parentHtml = new StringBuilder();
            menus.ForEach(menu =>
            {
                var childrenHmtl = new StringBuilder();
                menu.Children.ForEach(children =>
                {
                    childrenHmtl.AppendLine($"<dd><a data-url='{children.Url}' href='javascript:;'>{children.MenuName}</a></dd>");
                });

                parentHtml.Append(childrenHmtl.Length > 0
                    ? $"<li class='layui-nav-item'><a data-url='{menu.Url}' href='javascript:;'>{menu.MenuName}</a><dl class='layui-nav-child'>{childrenHmtl}</dl></li>"
                    : $"<li class='layui-nav-item'><a data-url='{menu.Url}' href='javascript:;'>{menu.MenuName}</a></li>");
            });

            var result = new StringBuilder($"<ul id='left-scoll' class='layui-nav layui-nav-tree'>{parentHtml}</ul>");

            return new HtmlString(result.ToString());
        }
    }
}
