using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

namespace Bladex.Garantias.Presentation.Website.Components.Attributes
{
    /// <summary>
    /// Atributo que indica si la propiedad poseera tooltips.
    /// </summary>
    public class IsTooltipAttribute : MetadataAttribute
    {
        /// <summary>
        /// Constructor de <see cref="IsTooltipAttribute"/>.
        /// </summary>
        /// <param name="isTooltip"><see cref="Boolean"/> indica si posee tooltip o no.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public IsTooltipAttribute(bool isTooltip)
        {
            this.isTooltip = isTooltip;
        }

        /// <summary>
        /// Constructor de <see cref="IsTooltipAttribute"/>
        /// </summary>
        /// <param name="isTooltip"><see cref="Boolean"/> indica si posee tooltip o no.</param>
        /// <param name="imageHtmlAttributes">Indica los atributos del tag IMG representando una imagen para utilizar como icono de ayuda en los tooltips. Ex "src:t.gif", "alt:test", "class:someCssClass".</param>
        public IsTooltipAttribute(bool isTooltip, params string[] imageHtmlAttributes)
        {
            this.isTooltip = isTooltip;
            this.imageHtmlAttributes = imageHtmlAttributes.ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);
        }

        // Fields
        /// <summary>
        ///   <see cref="IsTooltipAttribute"/>
        /// </summary>
        public static readonly IsTooltipAttribute Default = No;
        /// <summary>
        ///   <see cref="System.Boolean"/>
        /// </summary>
        private bool isTooltip;
        private Dictionary<string, string> imageHtmlAttributes;
        public static readonly IsTooltipAttribute No = new IsTooltipAttribute(false);
        public static readonly IsTooltipAttribute Yes = new IsTooltipAttribute(true);
        public const string IISTOOLTIP_KEY = "IsTooltip";
        public const string TOOLTIP_IMAGE_HTML_ATTRIBUTES = "ImageHtmlAttributes";

        // Methods

        public override bool Equals(object value)
        {
            if (this == value)
            {
                return true;
            }
            IsTooltipAttribute attribute = value as IsTooltipAttribute;
            return ((attribute != null) && (attribute.IsTooltip == this.IsTooltip));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return (this.IsTooltip == Default.IsTooltip);
        }

        // Properties
        /// <summary>
        /// Gets a value indicating whether this instance is tooltip.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is tooltip; otherwise, <c>false</c>.
        /// </value>
        public bool IsTooltip
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.isTooltip;
            }
        }

        public Dictionary<string,string> ImageHtmlAttributes
        {
            get 
            {
                return this.imageHtmlAttributes;
            }
        }


        /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public override void Process(System.Web.Mvc.ModelMetadata modelMetaData)
        {
            
            modelMetaData.AdditionalValues.Add(IISTOOLTIP_KEY, this.isTooltip);
            
            modelMetaData.AdditionalValues.Add(TOOLTIP_IMAGE_HTML_ATTRIBUTES, this.imageHtmlAttributes);
        }
    }
}