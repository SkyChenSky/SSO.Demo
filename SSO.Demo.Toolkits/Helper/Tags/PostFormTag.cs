using System.Collections.Generic;
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
    [HtmlTargetElement("postform")]
    public class PostFormTag : TagHelper
    {
        #region 初始化
        private const string IdAttributeName = "id";
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";

        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        [HtmlAttributeName(IdAttributeName)]
        public string Id { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;
        public PostFormTag(IHtmlGenerator generator)
        {
            _generator = generator;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            output.TagName = "form";
            output.TagMode = TagMode.StartTagAndEndTag;

            var htmlAttributes = new Dictionary<string, object>
            {
                { "class", "layui-form layui-form-pane" },
                { "id",Id}
            };

            var tagBuilder = _generator.GenerateForm(ViewContext, Action, Controller, null, "post", htmlAttributes);

            output.MergeAttributes(tagBuilder);
            output.PostElement.SetHtmlContent(@"<script type='text/javascript'>
                 layui.use('form', function () {{
                    var form = layui.form;
                    form.render();
                }});
            </script>");
        }
    }
}
