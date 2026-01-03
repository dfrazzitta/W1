using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace W1.Helpers
{
    public static class CustomHtmlHelpers
    {

        public static string GreetUser(this IHtmlHelper htmlHelper, string name)
        {
            return $"<strong>Hello, {name}!</strong>";
        }

        public static IHtmlContent ImageLink(this IHtmlHelper htmlHelper, string name, string lightbox)
        {
            var imagename = HtmlEncoder.Default.Encode(name);
            var imgTag = new TagBuilder("<a>");
            imgTag.Attributes.Add("href", name);
            var lightbox2 = HtmlEncoder.Default.Encode(lightbox);
            imgTag.Attributes.Add("data-lightbox=", lightbox2);

            return imgTag;


            // var htmlString = new HtmlString($"< a href = {imagename} data-lightbox=\"image"\ >  </ a >");
            //  var htmlString = new HtmlString($"<a    {imagename}  > Image />");
            // return htmlString;
        }


    }
}