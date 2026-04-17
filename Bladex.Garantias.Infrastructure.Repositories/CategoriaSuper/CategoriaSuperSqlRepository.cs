using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.CategoriaSuper;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CategoriaSuper
{
    public class CategoriaSuperSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.CategoriaSuper>, ICategoriaSuperRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public CategoriaSuperSqlRepository()
            : this(null)
        {
        }

        public CategoriaSuperSqlRepository(IUnitOfWork unitOfWork)
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
            DomainModel.DomainBase.CategoriaSuper categoriaSuper = item as DomainModel.DomainBase.CategoriaSuper;
            if (categoriaSuper != null)
            {
                return this.PersistNewItem(categoriaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.CategoriaSuper categoriaSuper = item as DomainModel.DomainBase.CategoriaSuper;
            if (categoriaSuper != null)
            {
                return this.PersistUpdatedItem(categoriaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.CategoriaSuper categoriaSuper = item as DomainModel.DomainBase.CategoriaSuper;
            if (categoriaSuper != null)
            {
                this.PersistDeletedItem(categoriaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.CategoriaSuper PersistNewItem(DomainModel.DomainBase.CategoriaSuper item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                CategoriaSuperFactory.FieldNames.CategoriaSuperId,
                CategoriaSuperFactory.FieldNames.CategoriaSuperName,
                CategoriaSuperFactory.FieldNames.CategoriaSuperIsReadOnly));

            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.IsReadOnly)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.CategoriaSuper PersistUpdatedItem(DomainModel.DomainBase.CategoriaSuper item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",CategoriaSuperFactory.FieldNames.CategoriaSuperName,DataHelper.GetSqlValue(item.Nombre)));
            builder.Append(string.Format(",{0} = {1}", CategoriaSuperFactory.FieldNames.CategoriaSuperIsReadOnly, DataHelper.GetSqlValue(item.IsReadOnly)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.CategoriaSuper item)
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
            return "CategoriaSuper";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return CategoriaSuperFactory.FieldNames.CategoriaSuperId;
        }

        #region Implementation of ICategoriaSuperRepository

        public DomainModel.DomainBase.CategoriaSuper FindByGarantiaId(int categoriaId)
        {
            IGarantiaBaseRepository gRepo = RepositoryFactory.GetRepository<IGarantiaBaseRepository, Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>();
            Bladex.Garantias.DomainModel.DomainBase.GarantiaBase gBase = gRepo.FindBy(categoriaId);
            if (gBase != null)
                return gBase.CategoriaSuper;
            return null;

        }

        #endregion
    }
}


