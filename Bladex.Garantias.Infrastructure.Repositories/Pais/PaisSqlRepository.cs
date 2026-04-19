using System;
using System.Collections.Generic;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using System.Data;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Pais
{
    public class PaisSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Pais>, IPaisRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public PaisSqlRepository()
            : this(null)
        {
        }

        public PaisSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        #endregion

        #region IRepository Members

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Pais pais = item as DomainModel.DomainBase.Pais;
            if (pais != null)
            {
                return this.PersistNewItem(pais);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Pais pais = item as DomainModel.DomainBase.Pais;
            if (pais != null)
            {
                return this.PersistUpdatedItem(pais);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Pais pais = item as DomainModel.DomainBase.Pais;
            if (pais != null)
            {
                this.PersistDeletedItem(pais);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.Pais PersistNewItem(DomainModel.DomainBase.Pais item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                PaisFactory.FieldNames.PaisId,
                PaisFactory.FieldNames.PaisNombre,
                PaisFactory.FieldNames.CodigoSuper
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.CodigoSuper)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Pais PersistUpdatedItem(DomainModel.DomainBase.Pais item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                PaisFactory.FieldNames.PaisNombre,
                DataHelper.GetSqlValue(item.Nombre)));
            builder.Append(string.Format(", {0} = {1}",
                PaisFactory.FieldNames.CodigoSuper,
                DataHelper.GetSqlValue(item.CodigoSuper)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Pais item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods
       
        #endregion

        #region Implementation of IPaisRepository

        public IList<DomainModel.DomainBase.Pais> FindByName(string Name)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE P.[Nombre] = N'{0}';", Name)).ToString());
        }

        #endregion


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
            return " WHERE P.[ID] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "Pais"; 
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return PaisFactory.FieldNames.PaisId;
        }
    }
}
