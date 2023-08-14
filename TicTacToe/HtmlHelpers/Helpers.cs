using System.Web.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicTacToe.Models;
using FormMethod = System.Web.Mvc.FormMethod;
using TagBuilder = System.Web.Mvc.TagBuilder;
using TagRenderMode = System.Web.Mvc.TagRenderMode;

namespace TicTacToe.HtmlHelpers;

public static class Helpers
{
    public static IHtmlContent CellButton(this IHtmlHelper helper, bool disabled = false)
    {
        return SubmitButton(helper, " ? ", "cell-button", disabled);
    }

    public static IHtmlContent SubmitButton(this IHtmlHelper helper, 
        String text, 
        String cssClass = "", 
        bool disabled = false)
    {
        TagBuilder tagBuilder = new TagBuilder("button");
        if (cssClass != "")
        {
            tagBuilder.AddCssClass(cssClass);    
        }
        tagBuilder.MergeAttribute("type","submit");
        if (disabled)
        {
            tagBuilder.MergeAttribute("disabled","disabled");
        }

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

    public static IHtmlContent CellPreviewForLobby(this IHtmlHelper helper, TicTacToeModel model, int cell)
    {
        IHtmlContentBuilder contentBuilder =  new HtmlContentBuilder();
        contentBuilder.AppendHtml("<td class=\"cell\">");
        contentBuilder.AppendHtml("<code>");
        contentBuilder.Append(model.Field[cell]);
        contentBuilder.AppendHtml("</code>");
        contentBuilder.AppendHtml("</td>");
        return contentBuilder;
    }
    
}
  
