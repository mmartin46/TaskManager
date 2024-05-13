using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TaskManagerGUI.Helpers
{
    public class HeadLabelTagHelper : TagHelper
    {
        public string ?Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var outerElement = new TagBuilder("h3");
            outerElement.Attributes.Add("style", "font-weight: 300; color: white;");
            outerElement.InnerHtml.AppendHtml(Message);


            output.Content.AppendHtml(outerElement);
        }

    }
}
