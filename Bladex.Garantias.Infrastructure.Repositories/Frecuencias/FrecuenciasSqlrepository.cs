using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.Frecuencias;

namespace Bladex.Garantias.Infrastructure.Repositories.Frecuencias
{
    public class FrecuenciasSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Frecuencias>, IFrecuenciasRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public FrecuenciasSqlRepository()
            : this(null)
        {
        }

        public FrecuenciasSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.CodigoPaisId, this.AppendPais);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Frecuencias frecuencias = item as DomainModel.DomainBase.Frecuencias;
            if (frecuencias != null)
            {
                return this.PersistNewItem(frecuencias);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Frecuencias frecuencias = item as DomainModel.DomainBase.Frecuencias;
            if (frecuencias != null)
            {
                return this.PersistUpdatedItem(frecuencias);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Frecuencias frecuencias = item as DomainModel.DomainBase.Frecuencias;
            if (frecuencias != null)
            {
                this.PersistDeletedItem(frecuencias);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.Frecuencias PersistNewItem(DomainModel.DomainBase.Frecuencias item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4}) ",
                this.GetEntityName(),
                FrecuenciasFactory.FieldNames.FrecuenciasId,
                FrecuenciasFactory.FieldNames.FrecuenciasName,
                FrecuenciasFactory.FieldNames.FrecuenciasUnidad,
                FrecuenciasFactory.FieldNames.FrecuenciasValorFrecuencia
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2},{3})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                UnidadFrecuenciaResolver.Resolve(item.Unidad),
                DataHelper.GetSqlValue(item.ValorFrecuencia)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Frecuencias PersistUpdatedItem(DomainModel.DomainBase.Frecuencias item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                FrecuenciasFactory.FieldNames.FrecuenciasName,
                DataHelper.GetSqlValue(item.Nombre)));
            
            builder.Append(string.Format("{0} = {1}",
                FrecuenciasFactory.FieldNames.FrecuenciasUnidad,
                UnidadFrecuenciaResolver.Resolve(item.Unidad)
                ));

            builder.Append(string.Format("{0} = {1}",
                FrecuenciasFactory.FieldNames.FrecuenciasValorFrecuencia,
                DataHelper.GetSqlValue(item.Unidad)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Frecuencias item)
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
            return "Frecuencias";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return FrecuenciasFactory.FieldNames.FrecuenciasId;
        }
    }
}