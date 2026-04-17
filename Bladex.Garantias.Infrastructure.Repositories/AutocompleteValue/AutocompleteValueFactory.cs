using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.AutocompleteValue
{
    internal class AutocompleteValueFactory : IEntityFactory<DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string _ROW_ID = "RowId";
            public const string _HTML_CONTROL_ID = "HtmlControlId";
            public const string _VALUE = "Value";
        }

        #endregion

        #region IEntityFactory<AutocompleteValue> Members

        public DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue BuildEntity(System.Data.IDataReader reader)
        {
            var entity = new DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue {RowId = DataHelper.GetInteger(reader[FieldNames._ROW_ID]), HtmlControlId = DataHelper.GetString(reader[FieldNames._HTML_CONTROL_ID]), Value = DataHelper.GetString(reader[FieldNames._VALUE])};
            return entity;
        }

        #endregion
    }
}
