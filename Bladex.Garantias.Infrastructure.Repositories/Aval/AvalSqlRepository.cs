using System;
using System.Collections.Generic;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.SqlMappers;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using System.Data;
using System.Linq;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Aval
{
    public class AvalSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Aval>, IAvalRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public AvalSqlRepository()
            : this(null)
        {
        }

        public AvalSqlRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(AvalFactory.FieldNames.AvalPaisId, this.AppendPais);
            
            this.ChildCallbacks.Add(AvalFactory.FieldNames.AvalTipoAvalId, this.AppendTipoAval);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.Aval aval = item as DomainModel.DomainBase.Aval;
            if (aval != null)
            {
                return this.PersistNewItem(aval);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Aval");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Aval aval = item as DomainModel.DomainBase.Aval;
            if (aval != null)
            {
                return this.PersistUpdatedItem(aval);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Aval");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Aval aval = item as DomainModel.DomainBase.Aval;
            if (aval != null)
            {
                this.PersistDeletedItem(aval);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Aval");
        }

        protected override DomainModel.DomainBase.Aval PersistNewItem(DomainModel.DomainBase.Aval item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6}) ",
                this.GetEntityName(),
                AvalFactory.FieldNames.AvalGarantiaId,
                AvalFactory.FieldNames.AvalNombre,
                AvalFactory.FieldNames.AvalPorcentajeCobertura,
                AvalFactory.FieldNames.AvalPaisId,
                AvalFactory.FieldNames.AvalEsCliente,
                AvalFactory.FieldNames.AvalTipoAvalId
                ));

            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5})",
                DataHelper.GetSqlValue(item.GarantiaId),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.PorcentajeCobertura),
                DataHelper.GetSqlValue(item.Pais.Key),
                DataHelper.GetSqlValueEsCliente(item.EsCliente),
                DataHelper.GetSqlValue(item.TipoAval.Key)
                ));
            builder.AppendLine(" SELECT @@IDENTITY");
            item.Key = Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(builder.ToString())));
            return item;
        }

        protected override DomainModel.DomainBase.Aval PersistUpdatedItem(DomainModel.DomainBase.Aval item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1},",
                AvalFactory.FieldNames.AvalNombre,
                DataHelper.GetSqlValue(item.Nombre)));

            builder.Append(string.Format("{0} = {1},",
                AvalFactory.FieldNames.AvalPorcentajeCobertura,
                DataHelper.GetNullableDecimal(item.PorcentajeCobertura)));

            builder.Append(string.Format("{0} = {1},",
                AvalFactory.FieldNames.AvalGarantiaId,
                DataHelper.GetInteger(item.GarantiaId)));

            builder.Append(string.Format("{0} = {1},",
                AvalFactory.FieldNames.AvalPaisId,
                DataHelper.GetSqlValue(item.Pais.Key)));

            builder.Append(string.Format("{0} = {1},",
                AvalFactory.FieldNames.AvalEsCliente,
                DataHelper.GetSqlValueEsCliente(item.EsCliente)));

            builder.Append(string.Format("{0} = {1}",
                AvalFactory.FieldNames.AvalTipoAvalId,
                DataHelper.GetSqlValue(item.TipoAval.Key)));
            

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Aval item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendPais(DomainModel.DomainBase.Aval aval, object CodigoPais)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, DomainModel.DomainBase.Pais>();
            aval.Pais = repository.FindBy(CodigoPais);
        }

        private void AppendTipoAval(DomainModel.DomainBase.Aval aval, object CodigoTipoAval)
        {
            ITipoAvalRepository repository = RepositoryFactory.GetRepository<ITipoAvalRepository, DomainModel.DomainBase.TipoAval>();
            aval.TipoAval = repository.FindBy(CodigoTipoAval);
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
            return string.Concat(string.Format(" WHERE [{0}] ", AvalFactory.FieldNames.AvalId), "= {0};");
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "Aval";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return AvalFactory.FieldNames.AvalId;
        }

        /// <summary>
        /// Returns an Aval collection based on a Garantia entity.
        /// </summary>
        /// <param name="garantiaId">ID of the garantia</param>
        /// <returns>Aval Collection.</returns>
        public IList<DomainModel.DomainBase.Aval> GetByGarantiaId(int garantiaId)
        {
            StringBuilder builder = this.GetBaseQueryBuilder();
            
            return this.BuildEntitiesFromSql(builder.Append(string.Format(" WHERE [{0}] = {1};", AvalFactory.FieldNames.AvalGarantiaId, garantiaId)).ToString());
        }
    }
}
