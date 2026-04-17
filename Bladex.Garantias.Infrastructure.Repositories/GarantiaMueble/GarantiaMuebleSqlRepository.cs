using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaMueble
{
    public class GarantiaMuebleSqlRepository : SqlRepositoryBase<DomainModel.DomainBase.GarantiaMueble>, IGarantiaMuebleRepository
    {
        #region Private Members

        #endregion

        #region Public Constructors

        public GarantiaMuebleSqlRepository()
            : this(null)
        {
        }

        public GarantiaMuebleSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion


        #region BuildChildCallbacks

        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaMuebleFactory.FieldNames.AseguradorSuperId, this.AppendAseguradoraSuper);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
            //this.ChildCallbacks.Add(ClienteFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);
            //this.ChildCallbacks.Add("allowances", delegate(Project project, object childKeyName) { this.AppendProjectAllowances(project); });
        }

        #endregion


        #region Unit of Work Implementation

        public override EntityBase PersistNewItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaMueble garantia = item as DomainModel.DomainBase.GarantiaMueble;
            if (garantia != null)
            {
                return this.PersistNewItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaMueble");
        }

        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaMueble garantia = item as DomainModel.DomainBase.GarantiaMueble;
            if (garantia != null)
            {
                return this.PersistUpdatedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaMueble");
        }

        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaMueble garantia = item as DomainModel.DomainBase.GarantiaMueble;
            if (garantia != null)
            {
                this.PersistDeletedItem(garantia);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaMueble");
        }

        protected override DomainModel.DomainBase.GarantiaMueble PersistNewItem(DomainModel.DomainBase.GarantiaMueble item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            var result = garantiaBaseRepo.Add(item);
            item.Key = result.Key;
            if (item.AseguradorSuper == null || item.AseguradorSuper.Key == null)
            {
                item.AseguradorSuper = AseguradorasService.GetEmpty();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0} ({1},{2}, {3}, {4}, {5}) ",
                this.GetEntityName(),
                GarantiaMuebleFactory.FieldNames.Id,
                GarantiaMuebleFactory.FieldNames.AseguradorSuperId,
                GarantiaMuebleFactory.FieldNames.FechaInicialAvaluo,
                GarantiaMuebleFactory.FieldNames.FechaVencimientoAvaluo,
                GarantiaMuebleFactory.FieldNames.ValorTotalAvaluo
                ));

            builder.Append(string.Format(@"VALUES ({0},{1}, {2}, {3}, {4})",

                DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.AseguradorSuper),
                DataHelper.GetSqlValue(item.FechaInicialAvaluo),
                DataHelper.GetSqlValue(item.FechaVencimientoAvaluo),
                DataHelper.GetSqlValue(item.ValorTotalAvaluo)
                ));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), this.GetEntityName())));
            return item;
        }

        protected override DomainModel.DomainBase.GarantiaMueble PersistUpdatedItem(DomainModel.DomainBase.GarantiaMueble item)
        {
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);

            if (item.AseguradorSuper == null || item.AseguradorSuper.Key == null)
            {
                item.AseguradorSuper = AseguradorasService.GetEmpty();
            }

            var result = garantiaBaseRepo.Add(item);

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaMuebleFactory.FieldNames.AseguradorSuperId,
            DataHelper.GetSqlValue(item.AseguradorSuper)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaMuebleFactory.FieldNames.FechaInicialAvaluo,
            DataHelper.GetSqlValue(item.FechaInicialAvaluo)));

            builder.Append(string.Format("{0} = {1}, ",
            GarantiaMuebleFactory.FieldNames.FechaVencimientoAvaluo,
            DataHelper.GetSqlValue(item.FechaVencimientoAvaluo)));

            builder.Append(string.Format("{0} = {1} ",
            GarantiaMuebleFactory.FieldNames.ValorTotalAvaluo,
            DataHelper.GetSqlValue(item.ValorTotalAvaluo)));


            builder.Append(" ");
            builder.Append(this.BuildBaseWhereClause(item.Key));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaMueble item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
            // We could delete related objects here, and then, call the base method to delete the entity.
            IGarantiaBaseRepository garantiaBaseRepo = RepositoryFramework.RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>(this.UnitOfWork);
            garantiaBaseRepo.Remove(item);
        }

        #endregion

        #region Private Callback and Helper Methods

        private void AppendAseguradoraSuper(DomainModel.DomainBase.GarantiaMueble garantia, object CodigoAseguradoraSuper)
        {
            IAseguradorasRepository repository = RepositoryFactory.GetRepository<IAseguradorasRepository, DomainModel.DomainBase.Aseguradoras>();
            garantia.AseguradorSuper = repository.FindBy(CodigoAseguradoraSuper);
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
            return "GarantiaMueble";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaMuebleFactory.FieldNames.Id;
        }


        #region IGarantiaBaseRepository<GarantiaMueble> Members

        public bool ChangeType(DomainModel.DomainBase.GarantiaMueble garantia, DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
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

        #region IGarantiaBaseRepository<GarantiaMueble> Members


        public void WriteRecord(DomainModel.DomainBase.GarantiaMueble garantia)
        {
            if (garantia.AseguradorSuper == null)
                garantia.AseguradorSuper = AseguradorasService.GetEmpty();
            garantia.TipoGarantiaSuper = TipoGarantiaSuperService.GetEmpty(garantia.CategoriaSuper);
            if (this.FindBy(garantia.Key) != null)
                this.PersistUpdatedItem(garantia);
            else
                this.PersistNewItem(garantia);
        }

        #endregion
    }
}
