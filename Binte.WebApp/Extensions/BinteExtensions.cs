using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Binte.WebApp.Extensions
{
    public static class BinteExtensions
    {
        public static IHtmlContent BSTextBox<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            return htmlHelper.TextBoxFor(expression, new { @class = "form-control" });
        }
        public static IHtmlContent BSDropDown<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList)
        {
            return htmlHelper.DropDownListFor(expression,selectList, new { @class = "form-control select2bs4" });
        }
        public static IHtmlContent BSDisplayFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            StringBuilder builder= new StringBuilder();
            builder.Append($"<label for=\"{expression.Name}\" class=\"col-sm-2 col-form-label\">");
            builder.Append(htmlHelper.DisplayNameFor(expression));
            builder.Append("</label>");
            return new HtmlString(builder.ToString());

        }
    }
}
