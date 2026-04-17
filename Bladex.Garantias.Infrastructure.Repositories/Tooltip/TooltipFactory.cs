using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Tooltip
{
    internal class TooltipFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Tooltip>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string TooltipHtmlControlId = "HtmlControlId";
            public const string TooltipTooltipName = "TooltipName";
            public const string TooltipTooltipHtmlText = "TooltipHtmlText";
        }

        #endregion

        #region IEntityFactory<Tooltip> Members

        public DomainModel.DomainBase.Tooltip BuildEntity(System.Data.IDataReader reader)
        {
            var tooltip = new DomainModel.DomainBase.Tooltip {Key = reader[FieldNames.TooltipHtmlControlId].ToString(), TooltipName = reader[FieldNames.TooltipTooltipName].ToString(), TooltipHtmlText = reader[FieldNames.TooltipTooltipHtmlText].ToString()};

            return tooltip;
        }

        #endregion
    }
}
