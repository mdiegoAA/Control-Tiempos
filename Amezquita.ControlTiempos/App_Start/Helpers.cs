// ----------------------------------------------------------------------------------------------
// <copyright file="Helpers.cs" company="SCI Software">
//     Copyright (c) SCI Software 2015. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos
{
    public static class Helpers
    {
        public static MvcHtmlString DropDownListForKnockoutJs<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("select");

            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("name", ((MemberExpression) expression.Body).Member.Name, true);
            tagBuilder.GenerateId(((MemberExpression) expression.Body).Member.Name);

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            ModelState modelState;

            if (helper.ViewData.ModelState.TryGetValue(((MemberExpression) expression.Body).Member.Name, out modelState) && modelState.Errors.Count > 0)
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            tagBuilder.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(((MemberExpression) expression.Body).Member.Name, metadata));

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}