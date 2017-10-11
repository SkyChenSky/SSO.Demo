using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using SSO.Demo.Toolkits.Enums;

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
        private static KeyValuePair<string, object> GetExpressionValue<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var key = ExpressionHelper.GetExpressionText(expression);
            var value = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData,
                htmlHelper.MetadataProvider);
            return new KeyValuePair<string, object>(key, value.Model);
        }

        #region 时间控件

        public static IHtmlContent LayerUiDateTimePicker(this IHtmlHelper helper, string name, object value,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime,
            bool require = false)
        {
            var id = name.Replace(".", "_");

            var sb = new StringBuilder();

            var requireStr = require ? "require" : "";

            sb.AppendLine($"<input class='layui-input' id='{id}' name='{name}' value = '{value}' placeholder='{placeholder}' type='text' {requireStr}>");

            var layerUiDateTimeTypeStr = layerUiDateTimeType.ToString();
            sb.AppendFormat(@"<script type='text/javascript'>
             layui.use('laydate',
                    function () {{
                        var laydate = layui.laydate;
                        laydate.render({{
                            elem: '#{0}',
                            type: '{1}'
                        }});
                    }});
            </script>", id, layerUiDateTimeTypeStr);

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent LayerUiDateTimePickerFor<TModel, TResult>(this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TResult>> expression,
            string placeholder = "", LayerUiDateTimeType layerUiDateTimeType = LayerUiDateTimeType.datetime,
            bool require = false)
        {
            var modelProperty = helper.GetExpressionValue(expression);

            return helper.LayerUiDateTimePicker(modelProperty.Key, modelProperty.Value, placeholder,
                layerUiDateTimeType, require);
        }

        #endregion

        #region 文本框控件

        public static IHtmlContent LayerUiEditor(this IHtmlHelper helper, string name, object value, string placeholder = "", EInputType inputType = EInputType.text, bool require = false)
        {
            var id = name.Replace(".", "_");

            var sb = new StringBuilder();

            var requireStr = require ? "require" : "";

            var inputTypeStr = inputType.ToString();

            sb.AppendLine(
                $@"<input class='layui-input' id='{id}' name='{name}' value = '{value}' placeholder='{
                        placeholder
                    }' type='{inputTypeStr}' {requireStr}>");

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent LayerUiEditorFor<TModel, TResult>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TResult>> expression, string placeholder = "", EInputType inputType = EInputType.text, bool require = false)
        {
            var modelProperty = helper.GetExpressionValue(expression);

            return helper.LayerUiEditor(modelProperty.Key, modelProperty.Value, placeholder, inputType, require);
        }

        #endregion
    }

    public enum LayerUiDateTimeType
    {
        datetime = 0,
        month = 1,
        date = 2,
        time = 3,
        year = 4
    }
}