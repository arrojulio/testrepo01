using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.DomainBase;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Bladex.Garantias.Infrastructure.Repositories.IMPORT_TH_ATOMO_GARANTIAS
{
    public class IMPORT_TH_ATOMO_GARANTIASSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS>, IIMPORT_TH_ATOMO_GARANTIASRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public IMPORT_TH_ATOMO_GARANTIASSqlRepository()
            : this(null)
        {
        }

        public IMPORT_TH_ATOMO_GARANTIASSqlRepository(IUnitOfWork unitOfWork)
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
            throw new Exception("PersistNewItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            throw new Exception("PersistUpdatedItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            throw new Exception("PersistDeletedItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
        }

        protected override DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS PersistNewItem(DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS item)
        {
            throw new Exception("PersistNewItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
        }

        protected override DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS PersistUpdatedItem(DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS item)
        {
            throw new Exception("PersistUpdatedItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS item)
        {
            throw new Exception("PersistDeletedItem not supported for IMPORT_TH_ATOMO_GARANTIAS");
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
            return string.Format("SELECT * FROM {0} C ", this.GetEntityName());
        }

        /// <summary>
        /// Get the where clause used to retrieve one entity.
        /// </summary>
        /// <returns>SQL Where Clases to retrieve one entity.</returns>
        protected override string GetBaseWhereClause()
        {
            return " WHERE [ID] = '{0}';";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "IMPORT_TH_ATOMO_GARANTIAS";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return IMPORT_TH_ATOMO_GARANTIASFactory.FieldNames.ID;
        }

        #region Interface Implementation

        public IList<DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS> GetByFechaCorte(DateTime fechaCorte)
        {
            StringBuilder query = new StringBuilder(this.GetBaseQuery());
            query = query.Append(" WHERE Fecha_Corte = @FechaCorte");
            SqlCommand command = new SqlCommand(query.ToString());
            command.Parameters.AddWithValue("@FechaCorte", fechaCorte);
            return this.BuildEntitiesFromSql(command);

        }
        public DateTime GetLastFechaCorte()
        {
            string query = String.Format("SELECT MAX([{0}]) FechaCorte FROM [{1}]", IMPORT_TH_ATOMO_GARANTIASFactory.FieldNames.Fecha_corte, this.GetEntityName());
            using (IDataReader reader = this.ExecuteReader(query))
            {
                if (reader == null) throw new Exception("There is no FechaCorte");

                if (reader.Read())
                {
                    DateTime? result = DataHelper.GetNullableDateTime(reader["FechaCorte"]);
                    if (result.HasValue)
                        return result.Value;

                }
            }
            throw new Exception("There is no FechaCorte");
        }

        #endregion



    }
}

