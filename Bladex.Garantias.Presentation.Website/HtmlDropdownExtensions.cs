using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bladex.Garantias.Presentation.Website
{
    /// <summary>
    /// HTML Helpers for enum types.
    /// </summary>
    public static class HtmlDropdownExtensions
    {
        /// <summary>
        /// Enums the drop down list.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="htmlHelper">The HTML helper of type <see cref="System.Web.Mvc.HtmlHelper"/></param>
        /// <param name="name">The name of type <see cref="System.String"/></param>
        /// <param name="selectedValue">The selected value of type <see cref="TEnum"/></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = value.ToString(),
                    Value = ((int)Enum.Parse(value.GetType(), value.ToString())).ToString(),
                    Selected = (value.Equals(selectedValue))
                };

            return new MvcHtmlString(Telerik.Web.Mvc.UI.HtmlHelperExtension.Telerik(htmlHelper).DropDownList().Name(name).BindTo(items).ToHtmlString());

            //return htmlHelper.DropDownList(
            //    name,
            //    items
            //    );
        }

        /// <summary>
        /// Empty dummy item.
        /// </summary>
        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "Not Assigned", Value = "" } };

        /// <summary>
        /// Method for the check of nullable enums
        /// </summary>
        /// <param name="modelMetadata">The model metadata of type <see cref="System.Web.Mvc.ModelMetadata"/></param>
        /// <returns></returns>
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        /// <summary>
        /// Enums the drop down list for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="htmlHelper">The HTML helper of type <see cref="System.Web.Mvc.HtmlHelper&lt;TModel&gt;"/></param>
        /// <param name="expression">The expression of type <see cref="Func{T,TResult}"/></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = converter.ConvertToString(value),
                    Value = ((int)Enum.Parse(value.GetType(), value.ToString())).ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }
            return new MvcHtmlString(Telerik.Web.Mvc.UI.HtmlHelperExtension.Telerik(htmlHelper).DropDownListFor(expression).BindTo(items).ClientEvents(
                e => 
                { 
                    if(enumType == typeof(Bladex.Garantias.DomainModel.DomainBase.IndicadorAtomoEnum))
                    {
                        e.OnChange("indicadorAtomoDropDownChange");                        
                    }
                    else if(enumType == typeof(Bladex.Garantias.DomainModel.DomainBase.GarantiaSourceEnum))
                    {
                        //TODO: Add Method to handle this
                    }
                } ).ToHtmlString());
            //return htmlHelper.DropDownListFor(
            //    expression,
            //    items
            //    );
        }
    }
}