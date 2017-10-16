using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SSO.Demo.Toolkits.Enums;
using SSO.Demo.Toolkits.Extension;

namespace SSO.Demo.Toolkits.Helper.Tags
{
    /// <inheritdoc />
    /// <summary>
    /// 单选按钮组
    /// </summary>
    [HtmlTargetElement("dropdownlist", TagStructure = TagStructure.WithoutEndTag)]
    public class DropDownListTag : TagHelper
    {
        #region 初始化
        private const string ForAttributeName = "asp-for";
        private const string DataAttributeName = "asp-data";
        private const string UrlAttributeName = "asp-url";
        private const string DispalyAttributeName = "input-display";
        private const string DefaultTextAttributeName = "defaultText";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(DataAttributeName)]
        public Type Data { get; set; }

        [HtmlAttributeName(DispalyAttributeName)]
        public EInputDisplay InputDisplay { get; set; }

        [HtmlAttributeName(DefaultTextAttributeName)]
        public string DefaultText { get; set; }

        [HtmlAttributeName(UrlAttributeName)]
        public string Url { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlGenerator _generator;
        public DropDownListTag(IHtmlGenerator generator)
        {
            _generator = generator;
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.ThrowIfNull();
            output.ThrowIfNull();

            var kvList = Data.GetKeyValueList();
            var selectListItems = kvList.Select(a => new SelectListItem { Text = a.Key, Value = a.Value.ToString(), Selected = a.Value == (int)(For.Model ?? -1) }).ToList();

            if (!DefaultText.IsNullOrEmpty())
                selectListItems.Insert(0, new SelectListItem { Text = DefaultText, Value = "" });
            var tagBuilder = _generator.GenerateSelect(ViewContext, For.ModelExplorer, null, For.Name, selectListItems, null, false, null);

            output.Content.AppendHtml(tagBuilder);
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", InputDisplay.GetDisplayName());

            var idStr = NameAndIdProvider.CreateSanitizedId(ViewContext, For.Name,
                _generator.IdAttributeDotReplacement);
            if (Data == null && !Url.IsNullOrEmpty())
            {
                output.PostElement.SetHtmlContent(string.Format(@"<script type='text/javascript'>
                 $(function () {{
                    $('#{0}').bindSelectData('{1}','{2}');
                    }});
                </script>", idStr, Url, For.Model));
            }
        }
    }
}
