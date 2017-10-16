using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SSO.Demo.Toolkits.Enums;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Toolkits.Helper.Tags
{
    /// <inheritdoc />
    /// <summary>
    /// 输入文本框
    /// </summary>
    [HtmlTargetElement("editor", TagStructure = TagStructure.WithoutEndTag)]
    public class EditorTag : TagHelper
    {
        #region 初始化
        private const string ValueAttributeName = "value";
        private const string PlaceholderAttributeName = "placeholder";
        private const string FormatAttributeName = "asp-format";
        private const string ForAttributeName = "asp-for";
        private const string DispalyAttributeName = "input-display";
        private const string TypeAttributeName = "type";
        private const string DisabledAttributeName = "disabled";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ValueAttributeName)]
        public object Value { get; set; }

        [HtmlAttributeName(PlaceholderAttributeName)]
        public string Placeholder { get; set; }

        [HtmlAttributeName(FormatAttributeName)]
        public string Format { get; set; }

        [HtmlAttributeName(DispalyAttributeName)]
        public EInputDisplay InputDisplay { get; set; }

        [HtmlAttributeName(TypeAttributeName)]
        public EInputType Type { get; set; }

        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;
        public EditorTag(IHtmlGenerator generator)
        {
            _generator = generator;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            var modelExplorer = For.ModelExplorer;
            var attributes = new Dictionary<string, object>
            {
                {PlaceholderAttributeName,Placeholder??modelExplorer.Metadata.Description??modelExplorer.Metadata.DisplayName },
                {"class", "layui-input"},
                {"type", Type.ToString()}
            };
            if (modelExplorer.Metadata != null && modelExplorer.Metadata.IsRequired)
                attributes["required"] = "required";
            if(Disabled)
                attributes["Disabled"] = "Disabled";

            var value = modelExplorer.Model ?? Value;

            var inputTagBuilder = _generator.GenerateTextBox(ViewContext, modelExplorer, For.Name, value, Format, attributes);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(inputTagBuilder);
            output.Attributes.Add("class", InputDisplay.GetDisplayName());
        }
    }
}
