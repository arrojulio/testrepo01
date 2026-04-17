using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaDepositoOtroBanco
{
    public class GarantiaDepositoOtroBancoSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaDepositoOtroBanco>, IGarantiaDepositoOtroBancoRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaDepositoOtroBancoSqlRepository()
            : this(null)
        {
        }

        public GarantiaDepositoOtroBancoSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaDepositoOtroBancoFactory.FieldNames.BancoSuperId, this.AppendBancoSuper);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion


        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia = item as DomainModel.DomainBase.GarantiaDepositoOtroBanco;
            if (garantia != null)
            {
                return this.PersistNewItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDepositoOtroBanco");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia = item as DomainModel.DomainBase.GarantiaDepositoOtroBanco;
            if (garantia != null)
            {
                return this.PersistUpdatedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDepositoOtroBanco");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia = item as DomainModel.DomainBase.GarantiaDepositoOtroBanco;
            if (garantia != null)
            {
                this.PersistDeletedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDepositoOtroBanco");
        }

        protected override DomainModel.DomainBase.GarantiaDepositoOtroBanco PersistNewItem(DomainModel.DomainBase.GarantiaDepositoOtroBanco item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);
            item.Key = result.Key;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2}) ",
                this.GetEntityName(),
                GarantiaDepositoOtroBancoFactory.FieldNames.Id,
                GarantiaDepositoOtroBancoFactory.FieldNames.BancoSuperId));
                
            builder.Append(string.Format(@"VALUES ({0},{1})",
                
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.BancoSuper)));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaDepositoOtroBanco PersistUpdatedItem(DomainModel.DomainBase.GarantiaDepositoOtroBanco item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
            GarantiaDepositoOtroBancoFactory.FieldNames.BancoSuperId,
            DataHelper.GetSqlValue(item.BancoSuper)));
            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaDepositoOtroBanco item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
        }

        #endregion

       #region Private Callback and Helper Methods

        private void AppendBancoSuper(DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia, object codigoBancoSuper)
        {
            IBancosRepository repository = RepositoryFactory.GetRepository<IBancosRepository, DomainModel.DomainBase.Bancos>();
            garantia.BancoSuper = repository.FindBy(codigoBancoSuper);
        }
       
        #endregion

         /// <summary>
        /// Get the base query to retrieve entities.
        /// </summary>
        /// <returns>SQL Select Query to retrieve entities</returns>
        protected override string GetBaseQuery()
        {
            return string.Format("SELECT * FROM {0} C ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return " WHERE [ID] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "GarantiaDepositoOtroBanco";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaDepositoOtroBancoFactory.FieldNames.Id;
        }

        #region Implementation of IGarantiaBaseRepository<GarantiaDepositoOtroBanco>

        public bool ChangeType(DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
        {
            IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase> repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase>, DomainModel.DomainBase.GarantiaBase>();
            bool result = repository.ChangeType(garantia, newCategoriaSuper);
            if (result)
            {
                base.PersistDeletedItem(garantia);
            }
            return result;
        }

        public void WriteRecord(DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia)
        {
            if (garantia.BancoSuper == null)
                garantia.BancoSuper = BancosService.GetEmpty();
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion
    }
}
