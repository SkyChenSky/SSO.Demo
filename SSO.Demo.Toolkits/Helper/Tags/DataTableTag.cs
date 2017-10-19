using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SSO.Demo.Toolkits.Attribute;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Toolkits.Helper.Tags
{
    /// <inheritdoc />
    /// <summary>
    /// 表单控件
    /// </summary>
    [HtmlTargetElement("datatable", TagStructure = TagStructure.WithoutEndTag)]
    public class DataTableTag : TagHelper
    {
        #region 初始化
        private const string IdAttributeName = "id";
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string PageAttributeName = "page";
        private const string LimitAttributeName = "limit";
        private const string LayFilterAttributeName = "lay-filter";
        private const string ToolBarIdAttributeName = "toolbar";
        private const string MultipleAttributeName = "multiple";
        private const string ColsModelAttributeName = "ColsModel";

        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        [HtmlAttributeName(IdAttributeName)]
        public string Id { get; set; }

        [HtmlAttributeName(PageAttributeName)]
        public bool? Page { get; set; }

        [HtmlAttributeName(LayFilterAttributeName)]
        public string Filter { get; set; }

        [HtmlAttributeName(LimitAttributeName)]
        public int? Limit { get; set; }

        [HtmlAttributeName(ToolBarIdAttributeName)]
        public string ToolBarId { get; set; }

        [HtmlAttributeName(MultipleAttributeName)]
        public bool Multiple { get; set; }

        [HtmlAttributeName(ColsModelAttributeName)]
        public Type ColsModel { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;
        public DataTableTag(IHtmlGenerator generator)
        {
            _generator = generator;
            if (!Page.HasValue)
                Page = true;

            if (!Limit.HasValue)
                Limit = 10;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("id", Id);
            if (!Filter.IsNullOrEmpty())
                output.Attributes.Add("lay-filter", Filter);

            var url = $"url:'/{Controller}/{Action}',";
            var page = $"page: {Page.ToString().ToLower()},";
            var limit = $"limit: {Limit},";
            var elem = $"elem: '#{Id}',";
            var id = $"id: '{Id}',";
            var multiple = Multiple ? "{ checkbox: true, fixed: true }," : "";
            var toolBar = ToolBarId.IsNullOrEmpty() ? "" : $"{{title: '操作', fixed: true, width: 160, align: 'center', toolbar: '#{ToolBarId}' }},";

            var propertiyDic = ColsModel.GetProperties().ToDictionary(k => k.Name, v => v.GetCustomAttribute<TableColsAttribute>());

            var cols = new StringBuilder(64);
            foreach (var dic in propertiyDic)
            {
                if (dic.Value == null)
                    continue;

                var field = dic.Value.Field ?? dic.Key.ToCamelCase();
                var title = dic.Value.Tile;
                var width = dic.Value.Width;
                var align = dic.Value.Align.GetDisplayName();
                cols.AppendLine($"{{ field: '{field}', title: '{title}', width: {width},align: '{align}' }},");
            }

            output.PostElement.SetHtmlContent(string.Format(@"<script type='text/javascript'>
                layui.use('table', function() {{
                var table = layui.table;
                table.render({{
                    {0}
                    {1}
                    {2}
                    height: 473,
                    cols: [[
                        {6}
                        {5}
                        {7}
                    ]],
                     {3}
                     {4}
                }});
            }});
            </script>", elem, id, url, page, limit, toolBar, multiple, cols));
        }
    }
}
