using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Class used to represent the tooltips of the system.
    /// </summary>
    public class Tooltip : EntityBase
    {
        /// <summary>
        /// ID of the Tooltip. It represents the ID of the HTML control where the tooltip should be placed.
        /// </summary>
        public string HtmlControlId
        {
            get
            {
                return this.GetKeyAs<string>();
            }
            set { this.Key = value; }
        }

        /// <summary>
        /// Descriptive name used to identify the tooltip.
        /// </summary>
        public string TooltipName
        { get; set; }

        /// <summary>
        /// The tooltip itself. It could contain html code.
        /// </summary>
        public string TooltipHtmlText
        {
            get;
            set;
        }
    }
}
