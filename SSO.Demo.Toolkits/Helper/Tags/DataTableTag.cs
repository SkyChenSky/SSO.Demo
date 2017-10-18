using System.Collections.Generic;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

            var htmlAttributes = new Dictionary<string, object>
            {
                { "id",Id}
            };
            if (Filter.IsNullOrEmpty())
                htmlAttributes.Add("lay-filter", Filter);

            var tagBuilder = _generator.GenerateForm(ViewContext, null, null, null, null, htmlAttributes);
            output.MergeAttributes(tagBuilder);

            var url = $"url:'/{Controller}/{Action}',";
            var page = $"page: {Page},";
            var limit = $"limit: {Limit},";
            var elem = $"elem: '#{Id}',";
            var id = $"id: '{Id}',";
            var toolBar = ToolBarId.IsNullOrEmpty() ? "" : $"{{title: '操作', fixed: true, width: 160, align: 'center', toolbar: '#{ToolBarId}' }},";
            output.PostElement.SetHtmlContent(string.Format(@"<script type='text/javascript'>
                layui.use('table', function() {{
                var table = layui.table;

                table.render({{
                    {0}
                    {1}
                    {2}
                    height: 473,
                    cols: [[
                        {{ checkbox: true, fixed: true }},
                        {5}
                        {{ field: 'userName', title: '用户名', width: 200 }},
                        {{field: 'realName', title: '姓名', width: 120 }},
                        {{field: 'mobile', title: '手机号', width: 120 }},
                        {{field: 'email', title: 'Email', width: 200 }},
                        {{field: 'userType', title: '用户类型', width: 120 }},
                        {{field: 'userStatus', title: '状态', width: 100 }},
                        {{ field: 'createDateTime', title: '创建时间', width: 180 }}
                    ]],
                     {3}
                     {4}
                }});
            }});
            </script>", elem, id, url, page, limit, toolBar));
        }
    }
}
