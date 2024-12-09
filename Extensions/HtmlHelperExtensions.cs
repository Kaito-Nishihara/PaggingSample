using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaggingSample.Models;


namespace PaggingSample.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent PagingDropDownFor<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, PagingDropDown>> expression,
            object htmlAttributes = null)
        {
            // モデルのプロパティにアクセス
            var modelExplorer = htmlHelper.ViewData.ModelExplorer;
            var pagingDropDown = expression.Compile()(htmlHelper.ViewData.Model);

            if (pagingDropDown == null)
            {
                throw new ArgumentException("モデルに有効な PagingDropDown が含まれていません。");
            }

            // ドロップダウンを生成
            return htmlHelper.DropDownList(
                $"{GetExpressionText(expression)}.SelectedPageSize",
                pagingDropDown.PageSizeOptions,
                "",
                htmlAttributes);
        }

        private static string GetExpressionText<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            throw new ArgumentException("Invalid expression");
        }
    }
}
