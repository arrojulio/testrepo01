using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.LimitInformation;

namespace Bladex.Garantias.Infrastructure.Repositories.LimitInformation
{
    public class LimitInformationSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.LimitInformation>, ILimitInformationRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public LimitInformationSqlRepository()
            : this(null)
        {
        }

        public LimitInformationSqlRepository(IUnitOfWork unitOfWork)
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
            DomainModel.DomainBase.LimitInformation LimitInformation = item as DomainModel.DomainBase.LimitInformation;
            if (LimitInformation != null)
            {
                return this.PersistNewItem(LimitInformation);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.LimitInformation LimitInformation = item as DomainModel.DomainBase.LimitInformation;
            if (LimitInformation != null)
            {
                return this.PersistUpdatedItem(LimitInformation);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.LimitInformation LimitInformation = item as DomainModel.DomainBase.LimitInformation;
            if (LimitInformation != null)
            {
                this.PersistDeletedItem(LimitInformation);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.LimitInformation PersistNewItem(DomainModel.DomainBase.LimitInformation item)
        {
            throw new NotImplementedException();
        }

        protected override DomainModel.DomainBase.LimitInformation PersistUpdatedItem(DomainModel.DomainBase.LimitInformation item)
        {
            throw new NotImplementedException();
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.LimitInformation item)
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
            return "LimitInformation";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return LimitInformationFactory.FieldNames.CustomerId;
        }

        #region Implementation of ILimitInformationRepository

        /// <summary>
        /// Retorna la informacion del limite definido por <see cref="definitionId"/> para el cliente <see cref="customerId"/>
        /// </summary>
        /// <param name="definitionId">ID de la definicion del limite</param>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>Retorna la informacion del limite definido por <see cref="definitionId"/> para el cliente <see cref="customerId"/>. <see cref="LimitInformation"/></returns>
        public DomainModel.DomainBase.LimitInformation FindBy(int definitionId, string customerId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT * FROM {0}", this.GetEntityName());
            strBuilder.AppendLine(string.Format(" WHERE {0} = {1} AND {2} = {3} ;", "@" + LimitInformation.LimitInformationFactory.FieldNames.DefinitionId, LimitInformation.LimitInformationFactory.FieldNames.DefinitionId, "@" + LimitInformation.LimitInformationFactory.FieldNames.CustomerId, LimitInformation.LimitInformationFactory.FieldNames.CustomerId));
            using (DbCommand command = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {
                this.Database.AddInParameter(command, "@" + LimitInformationFactory.FieldNames.DefinitionId, System.Data.DbType.Int32,definitionId);
                this.Database.AddInParameter(command, "@" + LimitInformationFactory.FieldNames.CustomerId, System.Data.DbType.String, customerId);
                return this.BuildEntitiesFromSql(command).FirstOrDefault();
            }
        }

        #endregion
    }
}

