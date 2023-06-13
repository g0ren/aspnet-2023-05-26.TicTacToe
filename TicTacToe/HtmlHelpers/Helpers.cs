using System.Web.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TagBuilder = System.Web.Mvc.TagBuilder;
using TagRenderMode = System.Web.Mvc.TagRenderMode;

namespace TicTacToe.HtmlHelpers;

public static class Helpers
{
    public static IHtmlContent CellButton(this IHtmlHelper helper)
    {
        return SubmitButton(helper, " ? ", "cell-button");
    }

    public static IHtmlContent SubmitButton(this IHtmlHelper helper, String text, String cssClass = "")
    {
        TagBuilder tagBuilder = new TagBuilder("button");
        if (cssClass != "")
        {
            tagBuilder.AddCssClass(cssClass);    
        }
        tagBuilder.MergeAttribute("type","submit");

        IHtmlContentBuilder contentBuilder = new HtmlContentBuilder();
        contentBuilder.AppendHtml(tagBuilder.ToString(TagRenderMode.StartTag));
        contentBuilder.Append(text);
        contentBuilder.AppendHtml(tagBuilder.ToString(TagRenderMode.EndTag));

        return contentBuilder;
    }

    public static IHtmlContent Break(this IHtmlHelper helper)
    {
        TagBuilder tagBuilder = new TagBuilder("br");
        IHtmlContentBuilder contentBuilder = new HtmlContentBuilder();
        contentBuilder.AppendHtml(tagBuilder.ToString(TagRenderMode.SelfClosing));

        return contentBuilder;
    }
    
}
  
