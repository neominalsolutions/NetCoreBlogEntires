using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.TagHelpers
{

    [HtmlTargetElement("post-tag-list", Attributes = "tag-names")]
    public class PostTagHelper: TagHelper
    {

        public string[] TagNames { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            string tagListItems = string.Empty;

            // tag helper ile çalışırken sonuç html döndüğünden dolayı buraya link verirken asp-controller asp-action asp-area yazamayız. zaten onlarda proje çalışınca hrefe dönüyor. 
            foreach (var tagName in TagNames)
            {
                // # asciicode karşılığı link üzerinden gönderince %23 olduğu için filterede hata aldık.ondan değiştirdik.
                tagListItems += $"<li style='padding:2px;'><a href='/Post/List?tagName={tagName.Replace("#","%23")}'>{tagName}</a></li>";
            }

            output.Content.SetHtmlContent($"<div class='post-options'><div class='row'><div class='col-12'><ul class='post-tags'><li><i class='fa fa-tags'></i></li>{tagListItems}</ul></div></div></div>");

            base.Process(context, output);
        }

    }
}
