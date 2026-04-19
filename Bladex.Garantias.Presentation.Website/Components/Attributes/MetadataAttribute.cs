using System;
using System.Web.Mvc;

namespace Bladex.Garantias.Presentation.Website.Components.Attributes
{
    /// <summary>
    /// Base class for custom MetadataAttributes.
    /// </summary>
    public abstract class MetadataAttribute : Attribute
    {
        /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public abstract void Process(ModelMetadata modelMetaData);
    }
}