using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaInmueble
{
    public class GarantiaInmuebleSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaInmueble>, IGarantiaInmuebleRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaInmuebleSqlRepository()
            : this(null)
        {
        }

        public GarantiaInmuebleSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            //this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseClienteId, this.AppendCliente);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
            this.ChildCallbacks.Add(GarantiaInmuebleFactory.FieldNames.AseguradorSuperId, delegate(Bladex.Garantias.DomainModel.DomainBase.GarantiaInmueble garantia, object value) {
                garantia.AseguradorSuper = RepositoryFactory.GetRepository<IAvaluadorasRepository, DomainModel.DomainBase.Avaluadoras>().FindBy(value);
            
            });
        }

        #endregion


        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaInmueble garantia = item as DomainModel.DomainBase.GarantiaInmueble;
            if (garantia != null)
            {
                return this.PersistNewItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaInmueble");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaInmueble garantia = item as DomainModel.DomainBase.GarantiaInmueble;
            if (garantia != null)
            {
                return this.PersistUpdatedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaInmueble");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaInmueble garantia = item as DomainModel.DomainBase.GarantiaInmueble;
            if (garantia != null)
            {
                this.PersistDeletedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaInmueble");
        }

        protected override DomainModel.DomainBase.GarantiaInmueble PersistNewItem(DomainModel.DomainBase.GarantiaInmueble item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);
            item.Key = result.Key;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2},{3},{4},{5},{6},{7},{8},{9}) ",
                this.GetEntityName(),
                GarantiaInmuebleFactory.FieldNames.Id,
                GarantiaInmuebleFactory.FieldNames.AseguradorSuperId,
                GarantiaInmuebleFactory.FieldNames.ValorEvaluacionVentaRapida,
                GarantiaInmuebleFactory.FieldNames.ValorAvaluo,
                GarantiaInmuebleFactory.FieldNames.InscripcionRegistroPublico,
                GarantiaInmuebleFactory.FieldNames.NumeroDeFinca,
                GarantiaInmuebleFactory.FieldNames.FechaInicialAvaluo,
                GarantiaInmuebleFactory.FieldNames.FechaVencimientoAvaluo,
                GarantiaInmuebleFactory.FieldNames.ValorTotalAvaluo));

            builder.Append(string.Format(@"VALUES ({0},{1},{2},{3},{4},{5}, {6}, {7},{8})",

                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.AseguradorSuper),
                DataHelper.GetSqlValue(item.ValorEvaluacionVentaRapida),
                DataHelper.GetSqlValue(item.ValorAvaluo),
                DataHelper.GetSqlValue(item.InscripcionRegistroPublico),
                DataHelper.GetSqlValue(item.NumeroDeFinca),
                DataHelper.GetSqlValue(item.FechaInicialAvaluo),
                DataHelper.GetSqlValue(item.FechaVencimientoAvaluo),
                DataHelper.GetSqlValue(item.ValorTotalAvaluo)
                ));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;

        }

        protected override DomainModel.DomainBase.GarantiaInmueble PersistUpdatedItem(DomainModel.DomainBase.GarantiaInmueble item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.AseguradorSuperId,
            DataHelper.GetSqlValue(item.AseguradorSuper)));
            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.ValorEvaluacionVentaRapida,
            DataHelper.GetSqlValue(item.ValorEvaluacionVentaRapida)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.ValorAvaluo,
            DataHelper.GetSqlValue(item.ValorAvaluo)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.InscripcionRegistroPublico,
            DataHelper.GetSqlValue(item.InscripcionRegistroPublico)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.NumeroDeFinca,
            DataHelper.GetSqlValue(item.NumeroDeFinca)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.FechaInicialAvaluo,
            DataHelper.GetSqlValue(item.FechaInicialAvaluo)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaInmuebleFactory.FieldNames.FechaVencimientoAvaluo,
            DataHelper.GetSqlValue(item.FechaVencimientoAvaluo)));
            
            builder.Append(string.Format("{0} = {1} ",
            GarantiaInmuebleFactory.FieldNames.ValorTotalAvaluo,
            DataHelper.GetSqlValue(item.ValorTotalAvaluo)));

            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaInmueble item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
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
            return " WHERE [ID] = {0};";
        }

        /// <summary>
        /// Returns the entity table name.
        /// </summary>
        /// <returns>Entity Table Nombre</returns>
        protected override string GetEntityName()
        {
            return "GarantiaInmueble";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaInmuebleFactory.FieldNames.Id;
        }

        #region Implementation of IGarantiaBaseRepository<GarantiaInmueble>

        public bool ChangeType(DomainModel.DomainBase.GarantiaInmueble garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
        {
            IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase> repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository<DomainModel.DomainBase.GarantiaBase>, DomainModel.DomainBase.GarantiaBase>();
            bool result = repository.ChangeType(garantia, newCategoriaSuper);
            if (result)
            {
                base.PersistDeletedItem(garantia);
            }
            return result;
        }

        public void WriteRecord(DomainModel.DomainBase.GarantiaInmueble garantia)
        {
            if (garantia.AseguradorSuper == null)
                garantia.AseguradorSuper = AvaluadoraService.GetEmpty();
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion
    }
}
