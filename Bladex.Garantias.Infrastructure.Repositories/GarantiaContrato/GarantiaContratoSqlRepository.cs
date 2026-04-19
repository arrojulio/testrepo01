using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaContrato
{
    public class GarantiaContratoSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaContrato>, IGarantiaContratoRepository
    {
        private static string _GARANTIA_CONTRATO_COBERTURA_TABLE = "GarantiaContrato_Cobertura";
        #region Private Members
        protected ILogger _logger = Bladex.Garantias.Infrastructure.Logging.ApplicationLogger.Instance;
        #endregion

        #region Public Constructors

        public GarantiaContratoSqlRepository()
            : this(null)
        {
        }

        public GarantiaContratoSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            
        }

        #endregion

        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaContrato cliente = item as DomainModel.DomainBase.GarantiaContrato;
            if (cliente != null)
            {
                return this.PersistNewItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaContrato cliente = item as DomainModel.DomainBase.GarantiaContrato;
            if (cliente != null)
            {
                return this.PersistUpdatedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaContrato cliente = item as DomainModel.DomainBase.GarantiaContrato;
            if (cliente != null)
            {
                this.PersistDeletedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.Cliente");
        }

        /// <summary>
        /// Verifica si la garantia contrato existe
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        protected bool CheckAlreadyInserted(DomainModel.DomainBase.GarantiaContrato item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = {2} AND {3} = {4}",
                this.GetEntityName(),   //table
                GarantiaContratoFactory.FieldNames.DealReference,   //DealReference column
                DataHelper.GetSqlValue(item.DealReference),
                GarantiaContratoFactory.FieldNames.GarantiaId,      //campo garantiaId
                DataHelper.GetSqlValue(item.GarantiaId)));

            int cnt = Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName()))));

            return (cnt > 0);

        }

        protected override DomainModel.DomainBase.GarantiaContrato PersistNewItem(DomainModel.DomainBase.GarantiaContrato item)
        {

            if (CheckAlreadyInserted(item))
                return this.GetByGarantiaIdDealReference(item.GarantiaId.Value, item.DealReference);

            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7}) ",
                this.GetEntityName(),
                GarantiaContratoFactory.FieldNames.DealReference,
                GarantiaContratoFactory.FieldNames.PorcUtilization,
                GarantiaContratoFactory.FieldNames.GarantiaId,
                GarantiaContratoFactory.FieldNames.FechaRegistroInicial,
                GarantiaContratoFactory.FieldNames.FechaVencimientoGarantia,
                GarantiaContratoFactory.FieldNames.FechaVencimientoRiesgo,
                GarantiaContratoFactory.FieldNames.NetBalancePrincipal
                ));
            builder.Append(string.Format("VALUES ({0},{1},{2},{3},{4},{5},{6}) SELECT @@IDENTITY",
                DataHelper.GetSqlValue(item.DealReference),
                DataHelper.GetSqlValue(item.PorcUtilization),
                DataHelper.GetSqlValue(item.GarantiaId),
                DataHelper.GetSqlValue(item.FechaRegistroInicial),
                DataHelper.GetSqlValue(item.FechaVencimientoGarantia),
                DataHelper.GetSqlValue(item.FechaVencimientoRiesgo),
                DataHelper.GetSqlValue(item.NetBalancePrincipal)
                ));
            item.Key = Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName()))));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaContrato PersistUpdatedItem(DomainModel.DomainBase.GarantiaContrato item)
        {
            
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                GarantiaContratoFactory.FieldNames.DealReference,
                DataHelper.GetSqlValue(item.DealReference)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.PorcUtilization,
                DataHelper.GetSqlValue(item.PorcUtilization)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.GarantiaId,
                DataHelper.GetSqlValue(item.GarantiaId)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.FechaRegistroInicial,
                DataHelper.GetSqlValue(item.FechaRegistroInicial)));
            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.FechaVencimientoGarantia,
                DataHelper.GetSqlValue(item.FechaVencimientoGarantia)));
            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.FechaVencimientoRiesgo,
                DataHelper.GetSqlValue(item.FechaVencimientoRiesgo)));
            builder.Append(string.Format(",{0} = {1}",
                GarantiaContratoFactory.FieldNames.NetBalancePrincipal,
                DataHelper.GetSqlValue(item.NetBalancePrincipal)));


            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaContrato item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods

       
        #region Examples of use
        
       
        #endregion
       
        #endregion

        #region Implementation of IClienteRepository

        public DomainModel.DomainBase.GarantiaContrato GetByGarantiaIdDealReference(int GarantiaId, string DealReference)
        {
            StringBuilder query = new StringBuilder(String.Format("SELECT TOP 1 * FROM [{0}] WHERE [{1}] = {2} AND [{3}] = {4}", this.GetEntityName(), GarantiaContratoFactory.FieldNames.GarantiaId, DataHelper.GetSqlValue(GarantiaId), GarantiaContratoFactory.FieldNames.DealReference, DataHelper.GetSqlValue(DealReference)));
            return this.BuildEntityFromSql(query.ToString());

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
            return GarantiaContratoFactory.TableNames.GarantiaContratoTable;
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaContratoFactory.FieldNames.Id;
        }

        #region IGarantiaContratoRepository Members


        public IList<DomainModel.DomainBase.GarantiaContrato> GetByGarantiaId(int GarantiaId)
        {
            StringBuilder query = new StringBuilder(String.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} ", this.GetEntityName(), GarantiaContratoFactory.FieldNames.GarantiaId, DataHelper.GetSqlValue(GarantiaId)));
            return this.BuildEntitiesFromSql(query.ToString());
        }

        public void RefreshContratosIntradiarios()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("EXEC SP_Garantias_RefreshContratosIntradiarios");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {

                    this.Database.ExecuteNonQuery(cmd);
                    
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        #endregion

        // Ticket #78343321 - Garantias - % de cobertura

        /// <summary>
        /// Determina si la asignacion Garantia - Contrato provisto posee el valor de cobertura establecido externamente
        /// </summary>
        /// <param name="GarantiaId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool IsPorcUtilizationImported(int GarantiaId, string DealReference)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("SELECT COUNT(*) FROM {0} WHERE {1} = {2} AND {3} = {4}",
                _GARANTIA_CONTRATO_COBERTURA_TABLE,   //table
                GarantiaContratoFactory.FieldNames.DealReference,   //DealReference column
                DataHelper.GetSqlValue(DealReference),
                GarantiaContratoFactory.FieldNames.GarantiaId,      //campo garantiaId
                DataHelper.GetSqlValue(GarantiaId)));

            int cnt = Convert.ToInt32(this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName()))));

            return (cnt > 0);
        }
    }
}
