using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;

namespace Bladex.Garantias.Presentation.Website
{
    /// <summary>
    /// The HTML label extensions class.
    /// </summary>
    public static class HtmlLabelExtensions
    {
        /// <summary>
        /// Labels the tooltip for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper of type <see cref="System.Web.Mvc.HtmlHelper&lt;TModel&gt;"/></param>
        /// <param name="expression">The expression of type <see cref="Func{T,TResult}"/></param>
        /// <returns></returns>
        public static MvcHtmlString LabelTooltipFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.LabelTooltipFor<TModel, TValue>(expression, null);
        }
        /// <summary>
        /// Labels the tooltip for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper of type <see cref="System.Web.Mvc.HtmlHelper&lt;TModel&gt;"/></param>
        /// <param name="expression">The expression of type <see cref="Func{T,TResult}"/></param>
        /// <param name="labelText">The label text of type <see cref="System.String"/></param>
        /// <returns></returns>
        public static MvcHtmlString LabelTooltipFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, System.Linq.Expressions.Expression<Func<TModel, TValue>> expression, string labelText)
        {
            return LabelHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), labelText);
        }

        /// <summary>
        /// Labels the helper.
        /// </summary>
        /// <param name="html">The HTML of type <see cref="System.Web.Mvc.HtmlHelper"/></param>
        /// <param name="metadata">The metadata of type <see cref="System.Web.Mvc.ModelMetadata"/></param>
        /// <param name="htmlFieldName">Name of the HTML field.</param>
        /// <param name="labelText">The label text of type <see cref="System.String"/></param>
        /// <returns></returns>
        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText = "")
        {
            string str = labelText ?? (metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>()));
            if (string.IsNullOrEmpty(str))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tagBuilder = new TagBuilder("label");
            tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            // Append (*) if the property is required.
            tagBuilder.SetInnerText(metadata.IsRequired ? string.Format("{0} (*)", str) : str);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.Normal));
            if (metadata.AdditionalValues.ContainsKey(IsTooltipAttribute.IISTOOLTIP_KEY) && (bool)metadata.AdditionalValues[IsTooltipAttribute.IISTOOLTIP_KEY])
            {
                TagBuilder imgTagBuilder = new TagBuilder("img");
                imgTagBuilder.Attributes.Add("id", string.Concat("Tooltip_", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName))));
                imgTagBuilder.Attributes.Add("title", "");
                if (metadata.AdditionalValues.ContainsKey(IsTooltipAttribute.TOOLTIP_IMAGE_HTML_ATTRIBUTES))
                {
                    Dictionary<string, string> htmlAttr = metadata.AdditionalValues[IsTooltipAttribute.TOOLTIP_IMAGE_HTML_ATTRIBUTES] as Dictionary<string, string>;
                    
                    foreach (KeyValuePair<string, string> kv in htmlAttr)
                    {
                        imgTagBuilder.Attributes.Add(kv.Key, kv.Value);
                    }
                }
                else
                {
                    // Default Url
                    imgTagBuilder.Attributes.Add("src", "");
                    imgTagBuilder.Attributes.Add("alt", "There is no information defined for tooltip.");
                }
                strBuilder.AppendLine(imgTagBuilder.ToString(TagRenderMode.Normal));
            }
            return new MvcHtmlString(strBuilder.ToString());
        }


    }


}