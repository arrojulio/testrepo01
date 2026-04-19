using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.Bancos;

namespace Bladex.Garantias.Infrastructure.Repositories.Bancos
{
    public class BancosSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.Bancos>, IBancosRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public BancosSqlRepository()
            : this(null)
        {
        }

        public BancosSqlRepository(IUnitOfWork unitOfWork)
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
            DomainModel.DomainBase.Bancos bancos = item as DomainModel.DomainBase.Bancos;
            if (bancos != null)
            {
                return this.PersistNewItem(bancos);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.Bancos bancos = item as DomainModel.DomainBase.Bancos;
            if (bancos != null)
            {
                return this.PersistUpdatedItem(bancos);
            }
            // TODO: We should throw some exception here if the cast is not valid.
            return item;
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.Bancos bancos = item as DomainModel.DomainBase.Bancos;
            if (bancos != null)
            {
                this.PersistDeletedItem(bancos);
            }
            // TODO: We should throw some exception here if the cast is not valid.
        }

        protected override DomainModel.DomainBase.Bancos PersistNewItem(DomainModel.DomainBase.Bancos item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format("INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                BancosFactory.FieldNames.BancosId,
                BancosFactory.FieldNames.BancosName,
                BancosFactory.FieldNames.BancosCategoria));

            builder.Append(string.Format("VALUES ({0},{1},{2})",
                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Nombre),
                DataHelper.GetSqlValue(item.Categoria)
                ));
            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override DomainModel.DomainBase.Bancos PersistUpdatedItem(DomainModel.DomainBase.Bancos item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}",
                BancosFactory.FieldNames.BancosName,
                DataHelper.GetSqlValue(item.Nombre)));

            builder.Append(string.Format(",{0} = {1}",
                BancosFactory.FieldNames.BancosCategoria,
                DataHelper.GetSqlValue(item.Categoria)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.Bancos item)
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
            return "Bancos";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return BancosFactory.FieldNames.BancosId;
        }

        #region Implementation of IBancosRepository

        public DomainModel.DomainBase.Bancos FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return default(Bladex.Garantias.DomainModel.DomainBase.Bancos);
            }
            StringBuilder builder = this.GetBaseQueryBuilder();
            builder.Append(string.Format(" WHERE {0} LIKE @{1};", BancosFactory.FieldNames.BancosName, BancosFactory.FieldNames.BancosName)).ToString();
            using (DbCommand cmd = this.Database.GetSqlStringCommand(builder.ToString()))
            {
                this.Database.AddInParameter(cmd, BancosFactory.FieldNames.BancosName, System.Data.DbType.String);
                this.Database.SetParameterValue(cmd, BancosFactory.FieldNames.BancosName, name);
                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    if(reader.Read())
                        return this.BuildEntityFromReader(reader);
                }
            }
            // Si no tengo coincidencias..
            return default(Bladex.Garantias.DomainModel.DomainBase.Bancos);

        }

        #endregion
    }
}
