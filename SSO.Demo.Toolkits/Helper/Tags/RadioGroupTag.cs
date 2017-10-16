using System;
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
    /// 单选按钮组
    /// </summary>
    [HtmlTargetElement("radioGroup", TagStructure = TagStructure.WithoutEndTag)]
    public class RadioGroupTag : TagHelper
    {
        #region 初始化
        private const string ForAttributeName = "asp-for";
        private const string DataAttributeName = "asp-data";
        private const string DispalyAttributeName = "input-display";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(DataAttributeName)]
        public Type Data { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(DispalyAttributeName)]
        public EInputDisplay InputDisplay { get; set; }

        private readonly IHtmlGenerator _generator;
        public RadioGroupTag(IHtmlGenerator generator)
        {
            _generator = generator;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            var modelExplorer = For.ModelExplorer;

            var kvList = Data.GetKeyValueList();
            kvList.ForEach(dic =>
            {
                var attributes = new Dictionary<string, object>
                {
                    {"title", dic.Key}
                };
                var inputTagBuilder = _generator.GenerateRadioButton(ViewContext,
                    modelExplorer,
                    For.Name,
                    modelExplorer.Model,
                    dic.Value == (int)(modelExplorer.Model ?? -1),
                    attributes);

                output.Content.AppendHtml(inputTagBuilder);
            });

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", InputDisplay.GetDisplayName());
        }
    }
}
