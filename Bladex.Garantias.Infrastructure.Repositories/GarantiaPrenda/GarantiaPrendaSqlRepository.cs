using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaPrenda
{
    public class GarantiaPrendaSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaPrenda>, IGarantiaPrendaRepository
    {
         #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaPrendaSqlRepository()
            : this(null)
        {
        }

        public GarantiaPrendaSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaPrendaFactory.FieldNames.EmisorId, this.AppendEmisor);
            this.ChildCallbacks.Add(GarantiaPrendaFactory.FieldNames.TipoInstrumentoFinancieroId, this.AppendInstrumentoFinanciero);
            this.ChildCallbacks.Add(GarantiaPrendaFactory.FieldNames.CalificacionEmisionId, this.AppendCalificacionEmision);
            this.ChildCallbacks.Add(GarantiaPrendaFactory.FieldNames.CalificacionEmisorId, this.AppendCalificacionEmisor);
            this.ChildCallbacks.Add(GarantiaPrendaFactory.FieldNames.PaisEmision, this.AppendPaisEmision);
        }

        #endregion


        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaPrenda garantia = item as DomainModel.DomainBase.GarantiaPrenda;
            if (garantia != null)
            {
                return this.PersistNewItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaPrenda");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaPrenda garantia = item as DomainModel.DomainBase.GarantiaPrenda;
            if (garantia != null)
            {
                return this.PersistUpdatedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaPrenda");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaPrenda garantia = item as DomainModel.DomainBase.GarantiaPrenda;
            if (garantia != null)
            {
                this.PersistDeletedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaPrenda");
        }

        protected override DomainModel.DomainBase.GarantiaPrenda PersistNewItem(DomainModel.DomainBase.GarantiaPrenda item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);
            item.Key = result.Key;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7}, {8}, {9}, {10}) ",
                this.GetEntityName(),
                GarantiaPrendaFactory.FieldNames.Id,
                GarantiaPrendaFactory.FieldNames.EmisorId,
                GarantiaPrendaFactory.FieldNames.TipoInstrumentoFinancieroId,
                GarantiaPrendaFactory.FieldNames.CalificacionEmisionId,
                GarantiaPrendaFactory.FieldNames.CalificacionEmisorId,
                GarantiaPrendaFactory.FieldNames.PaisEmision,
                GarantiaPrendaFactory.FieldNames.IdentificadorPrenda,
                GarantiaPrendaFactory.FieldNames.FechaInicialAvaluo,
                GarantiaPrendaFactory.FieldNames.FechaVencimientoAvaluo,
                GarantiaPrendaFactory.FieldNames.ValorTotalAvaluo
                ));

            builder.Append(string.Format(@"VALUES ({0},{1},{2},{3},{4},{5},{6}, {7}, {8}, {9})",

                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.Emisor),
                DataHelper.GetSqlValue(item.TipoInstrumentoFinanciero),
                DataHelper.GetSqlValue(item.CalificacionEmision),
                DataHelper.GetSqlValue(item.CalificacionEmisor),
                DataHelper.GetSqlValue(item.PaisEmision),
                DataHelper.GetSqlValue(item.IdentificadorPrenda),
                DataHelper.GetSqlValue(item.FechaInicialAvaluo),
                DataHelper.GetSqlValue(item.FechaVencimientoAvaluo),
                DataHelper.GetSqlValue(item.ValorTotalAvaluo)
                ));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaPrenda PersistUpdatedItem(DomainModel.DomainBase.GarantiaPrenda item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.AppendLine(string.Format("{0} = {1}",
            GarantiaPrendaFactory.FieldNames.EmisorId,
            DataHelper.GetSqlValue(item.Emisor)));
            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.TipoInstrumentoFinancieroId,
            DataHelper.GetSqlValue(item.TipoInstrumentoFinanciero)));
            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.CalificacionEmisionId,
            DataHelper.GetSqlValue(item.CalificacionEmision)));
            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.CalificacionEmisorId,
            DataHelper.GetSqlValue(item.CalificacionEmisor)));
            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.PaisEmision,
            DataHelper.GetSqlValue(item.PaisEmision)));

            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.IdentificadorPrenda,
            DataHelper.GetSqlValue(item.IdentificadorPrenda)));

            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.FechaInicialAvaluo,
            DataHelper.GetSqlValue(item.FechaInicialAvaluo)));

            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.FechaVencimientoAvaluo,
            DataHelper.GetSqlValue(item.FechaVencimientoAvaluo)));

            builder.AppendLine(string.Format(", {0} = {1}",
            GarantiaPrendaFactory.FieldNames.ValorTotalAvaluo,
            DataHelper.GetSqlValue(item.ValorTotalAvaluo)));


            builder.AppendLine(" ");
            builder.AppendLine(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaPrenda item)
        {
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendPaisEmision(DomainModel.DomainBase.GarantiaPrenda garantia, object PaisEmision)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, DomainModel.DomainBase.Pais>();
            garantia.PaisEmision = repository.FindBy(PaisEmision);
        }

        private void AppendEmisor(DomainModel.DomainBase.GarantiaPrenda garantia, object CodigoEmisor)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, DomainModel.DomainBase.Actor>();
            garantia.Emisor = repository.FindBy(CodigoEmisor);
        }

        private void AppendInstrumentoFinanciero(DomainModel.DomainBase.GarantiaPrenda garantia, object CodigoInstrumentoFinanciero)
        {
            IInstrumentoFinancieroRepository repository = RepositoryFactory.GetRepository<IInstrumentoFinancieroRepository, DomainModel.DomainBase.InstrumentoFinanciero>();
            garantia.TipoInstrumentoFinanciero = repository.FindBy(CodigoInstrumentoFinanciero);
        }

        private void AppendCalificacionEmision(DomainModel.DomainBase.GarantiaPrenda garantia, object CodigoCalificacionEmision)
        {
            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, DomainModel.DomainBase.CalificacionesRiesgo>();
            garantia.CalificacionEmision = repository.FindBy(CodigoCalificacionEmision);
        }

        private void AppendCalificacionEmisor(DomainModel.DomainBase.GarantiaPrenda garantia, object CodigoCalificacionEmisor)
        {
            ICalificacionesRiesgoRepository repository = RepositoryFactory.GetRepository<ICalificacionesRiesgoRepository, DomainModel.DomainBase.CalificacionesRiesgo>();
            garantia.CalificacionEmisor = repository.FindBy(CodigoCalificacionEmisor);
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
            return "GarantiaPrenda";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaPrendaFactory.FieldNames.Id;
        }

        #region IGarantiaBaseRepository<GarantiaPrenda> Members

        public bool ChangeType(DomainModel.DomainBase.GarantiaPrenda garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
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

        #region IGarantiaBaseRepository<GarantiaPrenda> Members


        public void WriteRecord(DomainModel.DomainBase.GarantiaPrenda garantia)
        {
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion
    }
}
