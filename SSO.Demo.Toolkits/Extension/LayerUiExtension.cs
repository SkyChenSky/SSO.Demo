using System;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SSO.Demo.Toolkits.Extension
{
    public static class LayerUiExtension
    {
        public static IHtmlContent LayerUiDateTimePicker(this IHtmlHelper helper, string name, object value,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime,
            bool require = false)
        {
            var id = name.Replace(".", "_");

            var sb = new StringBuilder();

            var requireStr = require ? "require" : "";

            sb.AppendFormat(
                @"<input class='layui-input' id='{0}' name='{1}' value = '{2}' placeholder='{3}' type='text' {4}>", id,
                name, value, placeholder, requireStr);
            sb.AppendFormat(@"<script>
            layui.use('laydate',
            function() {{
                var laydate = layui.laydate;

                laydate.render({{
                    elem: '#{0}',
                    type: '{1}'
                }});
            }});</script>", id, layerUiDateTimeType.ToString());

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent LayerUiDateTimePickerFor<TModel, TResult>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TResult>> expression,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime)
        {
            return helper.LayerUiDateTimePicker("", "", placeholder, layerUiDateTimeType);
        }

        public static void weqwe()
        {
            TagBuilder builder = new TagBuilder("");
        }
    }

    public enum LayerUiDateTimeType
    {
        year = 0,
        month = 1,
        date = 2,
        time = 3,
        datetime = 4
    }
}