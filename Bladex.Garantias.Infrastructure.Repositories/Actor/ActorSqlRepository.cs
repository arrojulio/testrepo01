using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Actor
{
    public class ActorSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Actor>, IActorRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public ActorSqlRepository()
            : this(null)
        {
        }

        public ActorSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(ActorFactory.FieldNames.PaisId, this.AppendPais);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Actor actor = item as DomainModel.DomainBase.Actor;
            if (actor != null)
            {
                return this.PersistNewItem(actor);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Actor actor = item as DomainModel.DomainBase.Actor;
            if (actor != null)
            {
                return this.PersistUpdatedItem(actor);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Actor actor = item as DomainModel.DomainBase.Actor;
            if (actor != null)
            {
                this.PersistDeletedItem(actor);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.Actor PersistNewItem(DomainModel.DomainBase.Actor item)
        {
            item.Key = UniqueKeyGenerator.Generate(10);
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                ActorFactory.FieldNames.ActorId,
                ActorFactory.FieldNames.ActorNombre,
                ActorFactory.FieldNames.PaisId
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.Pais.Key)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Actor PersistUpdatedItem(DomainModel.DomainBase.Actor item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                ActorFactory.FieldNames.ActorNombre,
                DataHelper.GetSqlValue(item.Nombre)));

            builder.Append(string.Format(",{0} = {1}",
                ActorFactory.FieldNames.PaisId,
                DataHelper.GetSqlValue(item.Pais.Key)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Actor item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendPais(DomainModel.DomainBase.Actor actor, object CodigoPais)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, DomainModel.DomainBase.Pais>();
            actor.Pais = repository.FindBy(CodigoPais);
        }

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
            return "Actor";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return ActorFactory.FieldNames.ActorId;
        }

        #region IActorRepository Members

        public DomainModel.DomainBase.Actor GetByName(string name)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE [{0}] = {1} ", ActorFactory.FieldNames.ActorNombre, DataHelper.GetSqlValue(name))).ToString()).FirstOrDefault();
        }

        #endregion
    }
}
