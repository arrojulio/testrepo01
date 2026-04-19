using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.CalificacionesRiesgo;

namespace Bladex.Garantias.Infrastructure.Repositories.CalificacionesRiesgo
{
    public class CalificacionesRiesgoSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.CalificacionesRiesgo>, ICalificacionesRiesgoRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public CalificacionesRiesgoSqlRepository()
            : this(null)
        {
        }

        public CalificacionesRiesgoSqlRepository(IUnitOfWork unitOfWork)
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
            DomainModel.DomainBase.CalificacionesRiesgo calificacionesRiesgo = item as DomainModel.DomainBase.CalificacionesRiesgo;
            if (calificacionesRiesgo != null)
            {
                return this.PersistNewItem(calificacionesRiesgo);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.CalificacionesRiesgo calificacionesRiesgo = item as DomainModel.DomainBase.CalificacionesRiesgo;
            if (calificacionesRiesgo != null)
            {
                return this.PersistUpdatedItem(calificacionesRiesgo);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.CalificacionesRiesgo calificacionesRiesgo = item as DomainModel.DomainBase.CalificacionesRiesgo;
            if (calificacionesRiesgo != null)
            {
                this.PersistDeletedItem(calificacionesRiesgo);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.CalificacionesRiesgo PersistNewItem(DomainModel.DomainBase.CalificacionesRiesgo item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5}) ",
                this.GetEntityName(),
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoId,
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoFitch,
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoMoodys,
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoOrden,
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoSyP
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Fitch),
                DataHelper.GetSqlValue(item.Moodys),
                DataHelper.GetSqlValue(item.Orden),
                DataHelper.GetSqlValue(item.SnP)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.CalificacionesRiesgo PersistUpdatedItem(DomainModel.DomainBase.CalificacionesRiesgo item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoFitch,
                DataHelper.GetSqlValue(item.Fitch)));

            builder.Append(string.Format(",{0} = {1}",
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoMoodys,
                DataHelper.GetSqlValue(item.Moodys)));

            builder.Append(string.Format(",{0} = {1}",
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoOrden,
                DataHelper.GetSqlValue(item.Orden)));

            builder.Append(string.Format(",{0} = {1}",
                CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoSyP,
                DataHelper.GetSqlValue(item.SnP)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.CalificacionesRiesgo item)
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
            return "CalificacionesRiesgo";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return CalificacionesRiesgoFactory.FieldNames.CalificacionesRiesgoId;
        }
    }
}
