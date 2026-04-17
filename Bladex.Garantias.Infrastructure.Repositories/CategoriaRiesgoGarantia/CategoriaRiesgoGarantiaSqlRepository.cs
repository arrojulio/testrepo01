using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CategoriaRiesgoGarantia
{
    public class CategoriaRiesgoGarantiaSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.CategoriaRiesgoGarantia>, ICategoriaRiesgoGarantiaRepository
    {
         #region Private Members

        #endregion

        #region Public Constructors

        public CategoriaRiesgoGarantiaSqlRepository()
            : this(null)
        {
        }

        public CategoriaRiesgoGarantiaSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            //TODO: Agregar Grupo
            this.ChildCallbacks.Add(CategoriaRiesgoGarantiaFactory.FieldNames.Grupo, this.AppendCategoria);

            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.CodigoPaisId, this.AppendPais);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        private void AppendCategoria(DomainModel.DomainBase.CategoriaRiesgoGarantia actor, object GrupoId)
        {
            IGrupoRiesgoGarantiaRepository repository = RepositoryFactory.GetRepository<IGrupoRiesgoGarantiaRepository, DomainModel.DomainBase.GrupoRiesgoGarantia>();
            actor.Grupo = repository.FindBy(GrupoId);
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.CategoriaRiesgoGarantia categoriaRiesgoGarantia = item as DomainModel.DomainBase.CategoriaRiesgoGarantia;
            if (categoriaRiesgoGarantia != null)
            {
                return this.PersistNewItem(categoriaRiesgoGarantia);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.CategoriaRiesgoGarantia categoriaRiesgoGarantia = item as DomainModel.DomainBase.CategoriaRiesgoGarantia;
            if (categoriaRiesgoGarantia != null)
            {
                return this.PersistUpdatedItem(categoriaRiesgoGarantia);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.CategoriaRiesgoGarantia categoriaRiesgoGarantia = item as DomainModel.DomainBase.CategoriaRiesgoGarantia;
            if (categoriaRiesgoGarantia != null)
            {
                this.PersistDeletedItem(categoriaRiesgoGarantia);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.CategoriaRiesgoGarantia PersistNewItem(DomainModel.DomainBase.CategoriaRiesgoGarantia item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                CategoriaRiesgoGarantiaFactory.FieldNames.Id,
                CategoriaRiesgoGarantiaFactory.FieldNames.Nombre,
                CategoriaRiesgoGarantiaFactory.FieldNames.Grupo
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.Grupo.Key)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.CategoriaRiesgoGarantia PersistUpdatedItem(DomainModel.DomainBase.CategoriaRiesgoGarantia item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                CategoriaRiesgoGarantiaFactory.FieldNames.Nombre,
                DataHelper.GetSqlValue(item.Nombre)));

            builder.Append(string.Format(",{0} = {1}",
                CategoriaRiesgoGarantiaFactory.FieldNames.Grupo,
                DataHelper.GetSqlValue(item.Grupo.Key)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.CategoriaRiesgoGarantia item)
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
            return "CategoriaRiesgoGarantia";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return CategoriaRiesgoGarantiaFactory.FieldNames.Id;
        }
    }
}
