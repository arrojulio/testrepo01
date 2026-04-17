using System;
using System.Linq;
using System.Collections.Generic;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaContratoDealReference
{
    /// <summary>
    /// The garantia contrato deal reference SQL repository class.
    /// </summary>
    public class GarantiaContratoDealReferenceSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaContratoDealReference>, IGarantiaContratoDealReferenceRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaContratoDealReferenceSqlRepository"/> class.
        /// </summary>
        public GarantiaContratoDealReferenceSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaContratoDealReferenceSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public GarantiaContratoDealReferenceSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.GarantiaContratoDealReference PersistNewItem(DomainModel.DomainBase.GarantiaContratoDealReference item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.GarantiaContratoDealReference PersistUpdatedItem(DomainModel.DomainBase.GarantiaContratoDealReference item)
        {
            throw new NotImplementedException();
        }

        

        #region Unit of Work Implementation

        #endregion

        #region Private Callback and Helper Methods

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaContratoDealReferenceFactory.FieldNames.CustomerId, this.AppendCliente);
        }

        /// <summary>
        /// Appends the cliente.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference"/></param>
        /// <param name="clienteId">The cliente id of type <see cref="System.Object"/></param>
        private void AppendCliente(DomainModel.DomainBase.GarantiaContratoDealReference entity, object clienteId)
        {
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, DomainModel.DomainBase.Cliente>(this.UnitOfWork);
            entity.Customer = repository.FindBy(clienteId);
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
            return string.Concat(string.Format("WHERE [{0}] = ", this.GetKeyFieldName()), "{0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return GarantiaContratoDealReferenceFactory.TableNames.GarantiaContratoDealReferences;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaContratoDealReferenceFactory.FieldNames.DealReference;
        }

        #region IGarantiaContratoDealReferenceRepository Members

        /// <summary>
        /// Gets the garantia contrato deal reference by customer.
        /// </summary>
        /// <param name="customerId">The customer id of type <see cref="System.String"/></param>
        /// <param name="globalLineDescription">The global line description of type <see cref="System.String"/></param>
        /// <returns></returns>
        public IEnumerable<Bladex.Garantias.DomainModel.DomainBase.GarantiaContratoDealReference> GetGarantiaContratoDealReferenceByCustomerOrEconomicGroup(string customerId, string globalLineDescription)
        {
            using(var cmd = this.Database.GetSqlStringCommand(string.Format("SELECT * FROM {0} WHERE ({1} = @{1}) OR (@{2} IS NOT NULL AND {2} = @{2})", GarantiaContratoDealReferenceFactory.TableNames.GarantiaContratoDealReferences, GarantiaContratoDealReferenceFactory.FieldNames.CustomerId, GarantiaContratoDealReference.GarantiaContratoDealReferenceFactory.FieldNames.GlobalLineDescription)))
            {
                this.Database.AddInParameter(cmd, GarantiaContratoDealReferenceFactory.FieldNames.CustomerId, System.Data.DbType.String,customerId);
                this.Database.AddInParameter(cmd, GarantiaContratoDealReferenceFactory.FieldNames.GlobalLineDescription, System.Data.DbType.String, globalLineDescription);
                return this.BuildEntitiesFromSql(cmd);
            }
        }


        #endregion
    }
}
