using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.Infrastructure.DomainBase;
using System.Data.Common;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaOtra
{
    public class GarantiaOtraSqlRepository :  SqlRepositoryBase<DomainModel.DomainBase.GarantiaOtra>, IGarantiaOtraRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaOtraSqlRepository()
            : this(null)
        {
        }

        public GarantiaOtraSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaOtraFactory.FieldNames.EmisorId, this.AppendEmisor);
            this.ChildCallbacks.Add(GarantiaOtraFactory.FieldNames.Id, this.AppendAval);
            
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion


        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaOtra garantia = item as DomainModel.DomainBase.GarantiaOtra;
            if (garantia != null)
            {
                return this.PersistNewItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaOtra");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaOtra garantia = item as DomainModel.DomainBase.GarantiaOtra;
            if (garantia != null)
            {
                return this.PersistUpdatedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaOtra");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaOtra garantia = item as DomainModel.DomainBase.GarantiaOtra;
            if (garantia != null)
            {
                this.PersistDeletedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaOtra");
        }

        protected override DomainModel.DomainBase.GarantiaOtra PersistNewItem(DomainModel.DomainBase.GarantiaOtra item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);

            DomainModel.DomainBase.GarantiaOtra result = (DomainModel.DomainBase.GarantiaOtra)garantiaBaseRepo.Add(item as DomainModel.DomainBase.GarantiaBase);
            item.Key = result.Key;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2},{3}) ",
                this.GetEntityName(),
                GarantiaOtraFactory.FieldNames.Id,
                GarantiaOtraFactory.FieldNames.EmisorId,
                GarantiaOtraFactory.FieldNames.NroReferencia));

            builder.Append(string.Format(@"VALUES ({0},{1},{2})",

                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Emisor),
                DataHelper.GetSqlValue(item.NroReferencia)));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaOtra PersistUpdatedItem(DomainModel.DomainBase.GarantiaOtra item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add((DomainModel.DomainBase.GarantiaBase)item) as DomainModel.DomainBase.GarantiaOtra;
            
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());
            
            builder.Append(string.Format("{0} = {1}", GarantiaOtraFactory.FieldNames.EmisorId, DataHelper.GetSqlValue(item.Emisor)));
            builder.Append(string.Format(", {0} = {1}", GarantiaOtraFactory.FieldNames.NroReferencia, DataHelper.GetSqlValue(item.NroReferencia)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaOtra item)
        {
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendAval(DomainModel.DomainBase.GarantiaOtra garantia, object garantiaId)
        {
            IAvalRepository repository = RepositoryFactory.GetRepository<IAvalRepository, DomainModel.DomainBase.Aval>();
            garantia.Avales = repository.GetByGarantiaId((int)garantiaId).ToList();
        }

        private void AppendEmisor(DomainModel.DomainBase.GarantiaOtra garantia, object CodigoEmisor)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, DomainModel.DomainBase.Actor>();
            garantia.Emisor = repository.FindBy(CodigoEmisor);
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
            return "GarantiaOtra";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaOtraFactory.FieldNames.Id;
        }

        #region IGarantiaBaseRepository<GarantiaOtra> Members

        public bool ChangeType(DomainModel.DomainBase.GarantiaOtra garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
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

        #region IGarantiaBaseRepository<GarantiaOtra> Members


        public void WriteRecord(DomainModel.DomainBase.GarantiaOtra garantia)
        {
            if(garantia.Emisor == null)
                garantia.Emisor = ActorService.GetEmpty();
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion

        public List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllGarantiasOtrasSQL() 
        {
            List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> list = new List<DomainModel.DomainBase.Summary.GarantiaOtraSummary>();
            
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT G.Id As [Key],ISNULL(getIdentificacionDocumentoGarantia,'(vacio)') AS [IdentificacionDocumentoGarantia],C.Nombre AS [Cliente],G.CategoriaSuperId AS [CategoriaSuper],GS.Nombre AS [TipoGarantiaSuper],G.ValorPolizaSeguro AS ValorPolizaSeguro,G.ValorInicial AS [ValorInicial],G.InternalStatus AS [InternalStatus],G.getValorGarantiaSuperIntendencia AS [ValorGarantiaSuperIntendencia],CS.IsReadOnly AS [IsReadOnly]");
            strBuilder.AppendFormat(" FROM GarantiaBase G ");
            strBuilder.AppendFormat(" INNER JOIN  GarantiaOtra O ON G.ID=O.ID");
            strBuilder.AppendFormat(" INNER JOIN Customer C ON G.Cliente=C.ID");
            strBuilder.AppendFormat(" INNER JOIN TipoGarantiaSuper GS ON G.TipoGarantiaSuper=GS.ID");
            strBuilder.AppendFormat(" INNER JOIN CategoriaSuper CS ON G.CategoriaSuperId=CS.ID");            
            strBuilder.AppendFormat(" WHERE InternalStatus not in (2)");
             
            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new DomainModel.DomainBase.Summary.GarantiaOtraSummary
                        {
                            Key = DataHelper.GetInteger(reader["Key"]),
                            Cliente = DataHelper.GetString(reader["Cliente"]),
                            CategoriaSuper = DataHelper.GetString(reader["CategoriaSuper"]),
                            IdentificacionDocumentoGarantia  = DataHelper.GetString(reader["IdentificacionDocumentoGarantia"]),
                            TipoGarantiaSuper = DataHelper.GetString(reader["TipoGarantiaSuper"]),
                            ValorInicial = DataHelper.GetDecimal(reader["ValorInicial"]),
                            ValorPolizaSeguro = DataHelper.GetDecimal(reader["ValorPolizaSeguro"]),
                            InternalStatus = DataHelper.GetInteger(reader["InternalStatus"]),
                            ValorGarantiaSuperIntendencia = DataHelper.GetDecimal(reader["ValorGarantiaSuperIntendencia"]),
                            IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])
                        });
                    }
                }

            }
                        
            return list;
        
        }

        public List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllByFianzasAvalesNoBancariosSQL(string fianzaAvalID) 
        {
            List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> list = new List<DomainModel.DomainBase.Summary.GarantiaOtraSummary>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT G.Id As [Key],ISNULL(getIdentificacionDocumentoGarantia,'(vacio)') AS [IdentificacionDocumentoGarantia],C.Nombre AS [Cliente],G.CategoriaSuperId AS [CategoriaSuper],GS.Nombre AS [TipoGarantiaSuper],G.ValorPolizaSeguro AS ValorPolizaSeguro,G.ValorInicial AS [ValorInicial],G.InternalStatus AS [InternalStatus],G.getValorGarantiaSuperIntendencia AS [ValorGarantiaSuperIntendencia],CS.IsReadOnly AS [IsReadOnly]");
            strBuilder.AppendFormat(" FROM GarantiaBase G ");
            strBuilder.AppendFormat(" INNER JOIN  GarantiaOtra O ON G.ID=O.ID");
            strBuilder.AppendFormat(" INNER JOIN Customer C ON G.Cliente=C.ID");
            strBuilder.AppendFormat(" INNER JOIN TipoGarantiaSuper GS ON G.TipoGarantiaSuper=GS.ID");
            strBuilder.AppendFormat(" INNER JOIN CategoriaSuper CS ON G.CategoriaSuperId=CS.ID");
            strBuilder.AppendFormat(" WHERE InternalStatus not in (2) AND GS.Id='{0}'",fianzaAvalID);

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new DomainModel.DomainBase.Summary.GarantiaOtraSummary
                        {
                            Key = DataHelper.GetInteger(reader["Key"]),
                            Cliente = DataHelper.GetString(reader["Cliente"]),
                            CategoriaSuper = DataHelper.GetString(reader["CategoriaSuper"]),
                            IdentificacionDocumentoGarantia = DataHelper.GetString(reader["IdentificacionDocumentoGarantia"]),
                            TipoGarantiaSuper = DataHelper.GetString(reader["TipoGarantiaSuper"]),
                            ValorInicial = DataHelper.GetDecimal(reader["ValorInicial"]),
                            ValorPolizaSeguro = DataHelper.GetDecimal(reader["ValorPolizaSeguro"]),
                            InternalStatus = DataHelper.GetInteger(reader["InternalStatus"]),
                            ValorGarantiaSuperIntendencia = DataHelper.GetDecimal(reader["ValorGarantiaSuperIntendencia"]),
                            IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])
                        });
                    }
                }

            }

            return list;
        }


        public List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> GetAllAndNotFianzasAvalesNoBancariosSQL(string fianzaAvalID) 
        {
            List<DomainModel.DomainBase.Summary.GarantiaOtraSummary> list = new List<DomainModel.DomainBase.Summary.GarantiaOtraSummary>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT G.Id As [Key],ISNULL(getIdentificacionDocumentoGarantia,'(vacio)') AS [IdentificacionDocumentoGarantia],C.Nombre AS [Cliente],G.CategoriaSuperId AS [CategoriaSuper],GS.Nombre AS [TipoGarantiaSuper],G.ValorPolizaSeguro AS ValorPolizaSeguro,G.ValorInicial AS [ValorInicial],G.InternalStatus AS [InternalStatus],G.getValorGarantiaSuperIntendencia AS [ValorGarantiaSuperIntendencia],CS.IsReadOnly AS [IsReadOnly]");
            strBuilder.AppendFormat(" FROM GarantiaBase G ");
            strBuilder.AppendFormat(" INNER JOIN  GarantiaOtra O ON G.ID=O.ID");
            strBuilder.AppendFormat(" INNER JOIN Customer C ON G.Cliente=C.ID");
            strBuilder.AppendFormat(" INNER JOIN TipoGarantiaSuper GS ON G.TipoGarantiaSuper=GS.ID");
            strBuilder.AppendFormat(" INNER JOIN CategoriaSuper CS ON G.CategoriaSuperId=CS.ID");
            strBuilder.AppendFormat(" WHERE InternalStatus not in (2) AND GS.Id <>'{0}'", fianzaAvalID);

            using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
            {

                using (IDataReader reader = this.Database.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(new DomainModel.DomainBase.Summary.GarantiaOtraSummary
                        {
                            Key = DataHelper.GetInteger(reader["Key"]),
                            Cliente = DataHelper.GetString(reader["Cliente"]),
                            CategoriaSuper = DataHelper.GetString(reader["CategoriaSuper"]),
                            IdentificacionDocumentoGarantia = DataHelper.GetString(reader["IdentificacionDocumentoGarantia"]),
                            TipoGarantiaSuper = DataHelper.GetString(reader["TipoGarantiaSuper"]),
                            ValorInicial = DataHelper.GetDecimal(reader["ValorInicial"]),
                            ValorPolizaSeguro = DataHelper.GetDecimal(reader["ValorPolizaSeguro"]),
                            InternalStatus = DataHelper.GetInteger(reader["InternalStatus"]),
                            ValorGarantiaSuperIntendencia = DataHelper.GetDecimal(reader["ValorGarantiaSuperIntendencia"]),
                            IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])
                        });
                    }
                }

            }

            return list;
        }

        public string GetNroReferencia(int garantiaId) 
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT TOP 1 NroReferencia FROM GarantiaOtra WHERE ID={0}",garantiaId);
            object objRes=this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(strBuilder.ToString()));

            if (objRes != null)
                return objRes.ToString();
            else
                return string.Empty;           
            
        }

        public string GetIdentificacionDocumentoGarantia(int garantiaId)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("SELECT TOP 1 GetIdentificacionDocumentoGarantia FROM GarantiaBase WHERE ID={0}", garantiaId);
            object objRes = this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(strBuilder.ToString()));

            if (objRes != null)
                return objRes.ToString();
            else
                return string.Empty;

        }

        public void UpdateIdentificacionDocumentoGarantia(int garantiaId, string valor)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("update GarantiaBase set GetIdentificacionDocumentoGarantia = '{0}' where ID={1}", valor, garantiaId);
            object objRes = this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(strBuilder.ToString()));
        }
    }
}
