using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.TipoGarantiaSuper;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Tooltip
{
    public class TooltipSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Tooltip>, ITooltipRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public TooltipSqlRepository()
            : this(null)
        {
        }

        public TooltipSqlRepository(IUnitOfWork unitOfWork)
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
            DomainModel.DomainBase.Tooltip tooltip = item as DomainModel.DomainBase.Tooltip;
            if (tooltip != null)
            {
                return this.PersistNewItem(tooltip);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Tooltip tooltip = item as DomainModel.DomainBase.Tooltip;
            if (tooltip != null)
            {
                return this.PersistUpdatedItem(tooltip);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Tooltip tooltip = item as DomainModel.DomainBase.Tooltip;
            if (tooltip != null)
            {
                this.PersistDeletedItem(tooltip);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.Tooltip PersistNewItem(DomainModel.DomainBase.Tooltip item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                TooltipFactory.FieldNames.TooltipHtmlControlId,
                TooltipFactory.FieldNames.TooltipTooltipName,
                TooltipFactory.FieldNames.TooltipTooltipHtmlText));

            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.HtmlControlId),
                DataHelper.GetSqlValue(item.TooltipName),
                DataHelper.GetSqlValue(item.TooltipHtmlText)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Tooltip PersistUpdatedItem(DomainModel.DomainBase.Tooltip item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                TooltipFactory.FieldNames.TooltipTooltipName,
                DataHelper.GetSqlValue(item.TooltipName)));
            builder.Append(string.Format(", {0} = {1}",
                TooltipFactory.FieldNames.TooltipTooltipHtmlText,
                DataHelper.GetSqlValue(item.TooltipHtmlText)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Tooltip item)
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
            return string.Format("SELECT * FROM {0} ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return string.Concat(string.Format(" WHERE {0} ", this.GetKeyFieldName()), " = {0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "Tooltip";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return TooltipFactory.FieldNames.TooltipHtmlControlId;
        }
    }
}
