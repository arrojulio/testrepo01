using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoGarantiaSuper
{
    public class TipoGarantiaSuperSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.TipoGarantiaSuper>, ITipoGarantiaSuperRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public TipoGarantiaSuperSqlRepository()
            : this(null)
        {
        }

        public TipoGarantiaSuperSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperCategoriaId, this.AppendCategoria);
        }

        private void AppendCategoria(DomainModel.DomainBase.TipoGarantiaSuper actor, object CodigoCategoria)
        {
            ICategoriaSuperRepository repository = RepositoryFactory.GetRepository<ICategoriaSuperRepository, DomainModel.DomainBase.CategoriaSuper>();
            actor.Categoria = repository.FindBy(CodigoCategoria);
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.TipoGarantiaSuper tipoGarantiaSuper = item as DomainModel.DomainBase.TipoGarantiaSuper;
            if (tipoGarantiaSuper != null)
            {
                return this.PersistNewItem(tipoGarantiaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.TipoGarantiaSuper tipoGarantiaSuper = item as DomainModel.DomainBase.TipoGarantiaSuper;
            if (tipoGarantiaSuper != null)
            {
                return this.PersistUpdatedItem(tipoGarantiaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.TipoGarantiaSuper tipoGarantiaSuper = item as DomainModel.DomainBase.TipoGarantiaSuper;
            if (tipoGarantiaSuper != null)
            {
                this.PersistDeletedItem(tipoGarantiaSuper);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.TipoGarantiaSuper PersistNewItem(DomainModel.DomainBase.TipoGarantiaSuper item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4}) ",
                this.GetEntityName(),
                TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperId,
                TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperName,
                TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperCategoriaId,
                TipoGarantiaSuperFactory.FieldNames.IsActive));

            builder.Append(string.Format("VALUES ({0},{1},{2},{3})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.Categoria.Key.ToString()),
                DataHelper.GetSqlValue(item.IsActive)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.TipoGarantiaSuper PersistUpdatedItem(DomainModel.DomainBase.TipoGarantiaSuper item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperName,
                DataHelper.GetSqlValue(item.Nombre)));
            builder.Append(string.Format(", {0} = {1}",
                TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperCategoriaId,
                DataHelper.GetSqlValue(item.Categoria.Key.ToString())));
            builder.Append(string.Format("{0} = {1}",
                TipoGarantiaSuperFactory.FieldNames.IsActive,
                DataHelper.GetSqlValue(item.IsActive)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.TipoGarantiaSuper item)
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
            return "TipoGarantiaSuper";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return TipoGarantiaSuperFactory.FieldNames.TipoGarantiaSuperId;
        }
    }
}
