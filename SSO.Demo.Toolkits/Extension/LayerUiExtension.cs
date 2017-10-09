using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using SSO.Demo.Toolkits.Helper;

namespace SSO.Demo.Toolkits.Extension
{
    public static class LayerUiExtension
    {
        /// <summary>
        /// 返回属性的名称和值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static KeyValuePair<string, object> GetExpressionValue<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var key = ExpressionHelper.GetExpressionText(expression);
            var value = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            return new KeyValuePair<string, object>(key, value.Model);
        }

        public static IHtmlContent LayerUiDateTimePicker(this IHtmlHelper helper, string name, object value,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime,
            bool require = false)
        {
            var id = name.Replace(".", "_");

            var sb = new StringBuilder();

            var requireStr = require ? "require" : "";

            sb.AppendFormat(@"<input class='layui-input' id='{0}' name='{1}' value = '{2}' placeholder='{3}' type='text' {4}>", id, name, value, placeholder, requireStr);
            sb.AppendFormat(@"<script type='text/javascript'>
             layui.use('laydate',
                    function () {{
                        var laydate = layui.laydate;

                        laydate.render({{
                            elem: '#{0}',
                            type: '{1}'
                        }});
                    }});
            </script>", id, layerUiDateTimeType.ToString());

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent LayerUiDateTimePickerFor<TModel, TResult>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TResult>> expression,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime)
        {
            var modelProperty = helper.GetExpressionValue(expression);

            return helper.LayerUiDateTimePicker(modelProperty.Key, modelProperty.Value, placeholder, layerUiDateTimeType);
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