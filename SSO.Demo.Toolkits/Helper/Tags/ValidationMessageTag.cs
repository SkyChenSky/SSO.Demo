using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Toolkits.Helper.Tags
{
    /// <inheritdoc />
    /// <summary>
    /// 验证标签
    /// </summary>
    [HtmlTargetElement("ValidationMessage", TagStructure = TagStructure.WithoutEndTag)]
    public class ValidationMessageTag : TagHelper
    {
        private const string ValidationForAttributeName = "asp-validation-for";

        public ValidationMessageTag(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            var inputTagBuilder = Generator.GenerateValidationMessage(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                null,
                null,
                null);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(inputTagBuilder);
            output.Attributes.Add("class", "layui-form-mid");
            output.Attributes.Add("style", "color: red");
        }
    }
}
