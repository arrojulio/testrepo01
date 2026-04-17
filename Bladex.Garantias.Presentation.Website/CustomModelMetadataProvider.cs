using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Presentation.Website.Components.Attributes;

namespace Bladex.Garantias.Presentation.Website
{

    /// <summary>
    /// Custom class used to allow the process of custom attributes in models.
    /// </summary>
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The metadata for the property.
        /// </returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            // Create the base metadata, default behavior
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            // Process my custom attributes
            attributes.OfType<MetadataAttribute>().ToList().ForEach(x => x.Process(modelMetadata));
            return modelMetadata;
        }

    }
}