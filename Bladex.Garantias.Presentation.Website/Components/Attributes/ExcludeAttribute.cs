namespace Bladex.Garantias.Presentation.Website.Components.Attributes
{
    /// <summary>
    /// The exclude attribute class. Used to exclude properties from being displayed into the maker and checker operations form using reflection.
    /// </summary>
    public class ExcludeAttribute : MetadataAttribute
    {
        public override void Process(System.Web.Mvc.ModelMetadata modelMetaData)
        {
            
        }
    }
}