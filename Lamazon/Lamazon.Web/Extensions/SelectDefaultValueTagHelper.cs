using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Lamazon.Web.Extensions
{
    [HtmlTargetElement(Attributes = "asp-default-value")]
    public class SelectDefaultValueTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-default-value")]
        public string DefaultValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldAddDefaultValue())
            {
                AddDefaultValue(output);
            }
        }

        private void AddDefaultValue(TagHelperOutput output)
        {
            output.Content.AppendHtml($"<option value=''>{DefaultValue}</option>");
        }

        private bool ShouldAddDefaultValue()
        {
            if (DefaultValue == null)
            {
                return false;
            }

            return true;
        }
    }
}
