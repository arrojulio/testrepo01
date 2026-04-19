using System;
using System.Collections.Generic;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.Aval;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.AutocompleteValue
{
    public class AutocompleteValueSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue>, IAutocompleteValueRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public AutocompleteValueSqlRepository()
            : this(null)
        {
        }

        public AutocompleteValueSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue entity = item as DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue;
            if (entity != null)
            {
                return this.PersistNewItem(entity);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue entity = item as DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue;
            if (entity != null)
            {
                return this.PersistUpdatedItem(entity);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue entity = item as DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue;
            if (entity != null)
            {
                this.PersistDeletedItem(entity);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue");
        }

        protected override DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue PersistNewItem(DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2}) ",
                this.GetEntityName(),
                AutocompleteValueFactory.FieldNames._HTML_CONTROL_ID,
                AutocompleteValueFactory.FieldNames._VALUE
                ));

            builder.Append(string.Format("VALUES ({0},{1})",
                DataHelper.GetSqlValue(item.HtmlControlId),
                DataHelper.GetSqlValue(item.Value)
                ));
            builder.AppendLine(" SELECT @@IDENTITY");
            item.Key = Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(builder.ToString())));
            return item;
        }

        protected override DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue PersistUpdatedItem(DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1},",
                AutocompleteValueFactory.FieldNames._HTML_CONTROL_ID,
                DataHelper.GetSqlValue(item.HtmlControlId)));

            builder.Append(string.Format("{0} = {1},",
                AutocompleteValueFactory.FieldNames._VALUE,
                DataHelper.GetNullableDecimal(item.Value)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        #endregion

        

        /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected override string GetBaseQuery()
        {
            return string.Format("SELECT * FROM {0} C ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return string.Concat(string.Format(" WHERE [{0}] ", AutocompleteValueFactory.FieldNames._ROW_ID), "= {0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "Autocomplete_Values";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return AutocompleteValueFactory.FieldNames._ROW_ID;
        }

        #region Implementation of IAutocompleteValueRepository

        /// <summary>
        /// Gets the autocomplete values grouped by the html control id
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public IList<DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue> GetByHtmlControlId(string htmlControlId)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();

            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE [{0}] = '{1}';", AutocompleteValueFactory.FieldNames._HTML_CONTROL_ID, htmlControlId)).ToString());
        }

        /// <summary>
        /// Gets the autocomplete values grouped by the html control id
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <param name="value">The value of type <see cref="System.String"/></param>
        /// <returns></returns>
        public DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue GetByHtmlControlIdAndValue(string htmlControlId, string value)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();

            return this.BuildEntityFromSql(builder.Append(string.Format(" WHERE [{0}] = '{1}' AND [{2}] = '{3}';", AutocompleteValueFactory.FieldNames._HTML_CONTROL_ID, htmlControlId, AutocompleteValueFactory.FieldNames._VALUE, value)).ToString());
        }

        #endregion
    }
}
