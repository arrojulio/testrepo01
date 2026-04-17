using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.GarantiaBase;
using Bladex.Garantias.DomainModel.Services;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaDeposito
{
    public class GarantiaDepositoSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaDeposito>, IGarantiaDepositoRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaDepositoSqlRepository()
            : this(null)
        {
        }

        public GarantiaDepositoSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaDepositoFactory.FieldNames.GarantiaDepositoBancoLocalSuperId, this.AppendBancoLocalSuper);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDeposito cliente = item as DomainModel.DomainBase.GarantiaDeposito;
            if (cliente != null)
            {
                return this.PersistNewItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDeposito");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDeposito cliente = item as DomainModel.DomainBase.GarantiaDeposito;
            if (cliente != null)
            {
                return this.PersistUpdatedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDeposito");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaDeposito cliente = item as DomainModel.DomainBase.GarantiaDeposito;
            if (cliente != null)
            {
                this.PersistDeletedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaDeposito");
        }

        protected override DomainModel.DomainBase.GarantiaDeposito PersistNewItem(DomainModel.DomainBase.GarantiaDeposito item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);
            item.Key = result.Key;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2}) ",
                this.GetEntityName(),
                GarantiaDepositoFactory.FieldNames.GarantiaDepositoId,
                GarantiaDepositoFactory.FieldNames.GarantiaDepositoBancoLocalSuperId));
                
            builder.Append(string.Format(@"VALUES ({0},{1})",
                
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.BancoLocalSuper)));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaDeposito PersistUpdatedItem(DomainModel.DomainBase.GarantiaDeposito item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
            GarantiaDepositoFactory.FieldNames.GarantiaDepositoBancoLocalSuperId,
            DataHelper.GetSqlValue(item.BancoLocalSuper)));
            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaDeposito item)
        {
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
            
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendBancoLocalSuper(DomainModel.DomainBase.GarantiaDeposito garantia, object codigoBancoLocalSuper)
        {
            IBancosRepository repository = RepositoryFactory.GetRepository<IBancosRepository, DomainModel.DomainBase.Bancos>();
            garantia.BancoLocalSuper = repository.FindBy(codigoBancoLocalSuper);
        }

        //private void AppendCliente(DomainModel.DomainBase.GarantiaBase garantia, object clienteId)
        //{
        //    IClienteRepository _repository = RepositoryFactory.GetRepository<IClienteRepository, DomainModel.DomainBase.Cliente>();
        //    garantia.Cliente = _repository.FindBy(clienteId);
        //}

        #region Examples of use
        
        //private void AppendOwner(Project project, object ownerCompanyId)
        //{
        //    ICompanyRepository _repository = RepositoryFactory.GetRepository<ICompanyRepository, Company>();
        //    project.Owner = _repository.FindBy(ownerCompanyId);
        //}

        //private void AppendProjectAllowances(DomainModel.DomainBase.Cliente project)
        //{
        //    string sql = string.Format("SELECT * FROM ProjectAllowance WHERE ProjectID = '{0}'", project.Key.ToString());
        //    using (IDataReader reader = this.ExecuteReader(sql))
        //    {
        //        while (reader.Read())
        //        {
        //            project.Allowances.Add(ClienteFactory.BuildAllowance(reader));
        //        }
        //    }
        //}
        #endregion
       
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
            return "GarantiaDeposito";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaDepositoFactory.FieldNames.GarantiaDepositoId;
        }

        //#region IRepository<GarantiaDeposito> Members

        //public new DomainModel.DomainBase.GarantiaDeposito FindBy(object key)
        //{
        //    return (DomainModel.DomainBase.GarantiaDeposito) base.FindBy(key);
        //}

        //public DomainModel.DomainBase.GarantiaDeposito Add(DomainModel.DomainBase.GarantiaDeposito item)
        //{
        //    return base.Add(item) as DomainModel.DomainBase.GarantiaDeposito;
        //}

        //public new DomainModel.DomainBase.GarantiaDeposito this[object key]
        //{
        //    get
        //    {
        //        return base[key] as DomainModel.DomainBase.GarantiaDeposito;
        //    }
        //    set
        //    {
        //        base[key] = value;
        //    }
        //}

        //public void Remove(DomainModel.DomainBase.GarantiaDeposito item)
        //{
        //    base.Remove(item);
        //}

        //public new IList<DomainModel.DomainBase.GarantiaDeposito> GetAll()
        //{
        //    return (IList<DomainModel.DomainBase.GarantiaDeposito>) base.GetAll();
        //}

        //#endregion

        #region Implementation of IGarantiaBaseRepository<GarantiaDeposito>

        public bool ChangeType(DomainModel.DomainBase.GarantiaDeposito garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
        {
            IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase> repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase>, DomainModel.DomainBase.GarantiaBase>();
            bool result = repository.ChangeType(garantia, newCategoriaSuper);
            if (result)
            {
                base.PersistDeletedItem(garantia);
            }
            return result;
        }

        #endregion

        #region IGarantiaBaseRepository<GarantiaDeposito> Members


        public void WriteRecord(DomainModel.DomainBase.GarantiaDeposito garantia)
        {
            if (garantia.BancoLocalSuper == null)
                garantia.BancoLocalSuper = BancosService.GetEmpty();
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion
    }
}
