using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoPoliza
{
    public class TipoPolizaSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.TipoPoliza>, ITipoPolizaRepository
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleSqlRepository"/> class.
        /// </summary>
        public TipoPolizaSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public TipoPolizaSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region BuildChildCallbacks

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {

        }

        #endregion

        #region Unit of Work Implementation

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistNewItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.TipoPoliza;
            return entity != null ? this.PersistNewItem(entity) : item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.Role;
            return entity != null ? this.PersistUpdatedItem(entity) : item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        public override void PersistDeletedItem(EntityBase item)
        {
            var entity = item as DomainModel.DomainBase.TipoPoliza;
            if (entity != null)
            {
                this.PersistDeletedItem(entity);
            }
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.TipoPoliza PersistNewItem(DomainModel.DomainBase.TipoPoliza item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2}) ",
                this.GetEntityName(),
                TipoPolizaFactory.FieldNames.TipoPolizaId,
                TipoPolizaFactory.FieldNames.TipoPolizaName
                ));
            builder.Append(string.Format("VALUES ({0},{1})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.TipoPoliza PersistUpdatedItem(DomainModel.DomainBase.TipoPoliza item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                TipoPolizaFactory.FieldNames.TipoPolizaName,
                DataHelper.GetSqlValue(item.Nombre)));
            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Role"/></param>
        protected override void PersistDeletedItem(DomainModel.DomainBase.TipoPoliza item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected override string GetBaseQuery()
        {
            return string.Format("SELECT * FROM [{0}] P ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return " WHERE P.[" + TipoPolizaFactory.FieldNames.TipoPolizaId + "] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return TipoPolizaFactory.TableName;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return TipoPolizaFactory.FieldNames.TipoPolizaId;
        }
       #endregion
    }
}
