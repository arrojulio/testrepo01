using System;
using System.Collections.Generic;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.SqlMappers;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using System.Data;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Cliente
{
    public class ClienteSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Cliente>, IClienteRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public ClienteSqlRepository()
            : this(null)
        {
        }

        public ClienteSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(ClienteFactory.FieldNames.CodigoPaisId, this.AppendPais);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Cliente cliente = item as DomainModel.DomainBase.Cliente;
            if (cliente != null)
            {
                return this.PersistNewItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Cliente cliente = item as DomainModel.DomainBase.Cliente;
            if (cliente != null)
            {
                return this.PersistUpdatedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Cliente cliente = item as DomainModel.DomainBase.Cliente;
            if (cliente != null)
            {
                this.PersistDeletedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        protected override DomainModel.DomainBase.Cliente PersistNewItem(DomainModel.DomainBase.Cliente item)
        {
            StringBuilder builder = new StringBuilder();
            // Generate Key 
            if(item.IsInternal || item.Key==null || item.Key.ToString()=="-1")
            {
                item.Key = UniqueKeyGenerator.Generate(10).ToUpper();
                this._logger.Debug(string.Format("Generando nuevo ID para el cliente {0}. Id Generado: {1}", item.Nombre, item.GetKeyAs<string>()));
                
            }
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}) ",
                this.GetEntityName(),
                ClienteFactory.FieldNames.ClienteId,
                ClienteFactory.FieldNames.Nombre,
                ClienteFactory.FieldNames.CodigoPaisId,
                ClienteFactory.FieldNames.ClienteRating,
                ClienteFactory.FieldNames.GrupoEconomico,
                ClienteFactory.FieldNames.NationalId,
                ClienteFactory.FieldNames.BusinessLineDescription,
                ClienteFactory.FieldNames.AccountOfficer,
                ClienteFactory.FieldNames.GlobalLineDescription,
                ClienteFactory.FieldNames.LimitExpDate,
                ClienteFactory.FieldNames.RecordStart,
                ClienteFactory.FieldNames.AuthStat,
                ClienteFactory.FieldNames.IsActive,
                ClienteFactory.FieldNames.Internal
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})",
                DataHelper.GetSqlValue(item.GetKeyAs<string>()),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.Pais.Key),
                DataHelper.GetSqlValue(item.Rating),
                DataHelper.GetSqlValue(item.GrupoEconomico),
                DataHelper.GetSqlValue(item.NationalId),
                DataHelper.GetSqlValue(item.BusinessLineDescription),
                DataHelper.GetSqlValue(item.AccountOfficer),
                DataHelper.GetSqlValue(item.GlobalLineDescription),
                DataHelper.GetSqlValue(item.LimitExpDate),
                DataHelper.GetSqlValue(item.RecordStat),
                DataHelper.GetSqlValue(item.AuthStat),
                DataHelper.GetSqlValue(item.IsActive),
                DataHelper.GetSqlValue(item.IsInternal)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Cliente PersistUpdatedItem(DomainModel.DomainBase.Cliente item)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                ClienteFactory.FieldNames.ClienteRating,
                DataHelper.GetSqlValue(item.Rating)));

            builder.Append(string.Format(",{0} = {1}",
                ClienteFactory.FieldNames.CodigoPaisId,
                DataHelper.GetSqlValue(item.Pais.Key)));

            builder.Append(string.Format(",{0} = {1}",
                ClienteFactory.FieldNames.GrupoEconomico,
                DataHelper.GetSqlValue(item.GrupoEconomico)));

            builder.Append(string.Format(",{0} = {1}",
                ClienteFactory.FieldNames.Nombre,
                DataHelper.GetSqlValue(item.Nombre)));

            // TODO: Completar update de columnas en repositorio de clientes.

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Cliente item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendPais(DomainModel.DomainBase.Cliente cliente, object CodigoPais)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, DomainModel.DomainBase.Pais>();
            cliente.Pais = repository.FindBy(CodigoPais);
        }

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

        #region Implementation of IClienteRepository

        public IList<DomainModel.DomainBase.Cliente> FindByName(string Name)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE C.[Nombre] = N'{0}';", Name)).ToString());
        }

        public IList<DomainModel.DomainBase.Cliente> FindByCodigoPais(string CodigoPais)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE C.[CountryID] = N'{0}';", CodigoPais)).ToString());
        }

        public IList<DomainModel.DomainBase.Cliente> FindByGrupoEconomico(string GrupoEconomico)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE C.[EconomicGroup] = N'{0}';", GrupoEconomico)).ToString());
        }

        #endregion

        public override DomainModel.DomainBase.Cliente FindBy(object key)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            builder.Append(string.Format( "WHERE C.[ID] = '{0}'", key));
            return this.BuildEntityFromSql(builder.ToString());
        }

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
            return "Customer";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return ClienteFactory.FieldNames.ClienteId;
        }
    }
}
