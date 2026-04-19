using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using System.Data;
using System.Data.Common;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaBase
{
    /// <summary>
    /// The garantia base SQL repository class.
    /// </summary>
    public class GarantiaBaseSqlRepository : SqlRepositoryBase<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>, IGarantiaBaseRepository<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>, IGarantiaBaseRepository
    {
        #region Private Members

        private const string _UDF_GARANTIAS_VALOR_GARANTIA = "dbo.UDF_Garantias_ValorGarantia";
        private const string _UDF_GARANTIAS_VALIDATION_UNBLOCK_GUARANTEE = "dbo.UDF_Validation_UnblockGuarantee";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaBaseSqlRepository"/> class.
        /// </summary>
        public GarantiaBaseSqlRepository()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaBaseSqlRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work of type <see cref="Bladex.Garantias.Infrastructure.IUnitOfWork"/></param>
        public GarantiaBaseSqlRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        #endregion

        #region BuildChildCallbacks

        /// <summary>
        /// Builds the child callbacks.
        /// </summary>
        protected override void BuildChildCallbacks()
        {
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseClienteId, this.AppendCliente);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseFiduciariaSuperId, this.AppendFiduciarias);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseDepositanteId, this.AppendDepositante);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseEvaluadorId, this.AppendEvaluador);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseAdministradorId, this.AppendAdministrador);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseAseguradorId, this.AppendAsegurador);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseRevisorId, this.AppendRevisor);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBasePaisGarantiaId, this.AppendPais);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaSuperId, this.AppendTipoGarantiaSuper);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaBladexId, this.AppendTipoGarantiaBladex);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseGaranteId, this.AppendGarante);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseFrequenciaRevisionId, this.AppendFrequencias);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaRiesgoGarantiaId, this.AppendCategoriaRiesgoGarantia);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseMonedaId, this.AppendMonedas);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseRatingGaranteId, this.AppendCalificacionesRiesgo);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseInternalStatusId, this.AppendInternalStatus);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseStatusId, this.AppendStatus);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaSuperId, this.AppendCategoriaSuper);
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseValorNecesarioDeGarantia, this.AppendValorNecesarioDeGarantia);
            // (Comentario: Para que en la vista seleccione el valor del DD automaticamente, aca tiene que existir la linea correspondiente a la propiedad)
            // Ticket #1619 - 1
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseRegion, this.AppendRegion);
            // Ticket #1619 - 2
            this.ChildCallbacks.Add(GarantiaBaseFactory.FieldNames.GarantiaBaseTipoPoliza, this.AppendTipoPoliza);
        }

        #endregion

        #region Unit of Work Implementation

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistNewItem(EntityBase item)
        {
            if (item != null && item as DomainModel.DomainBase.GarantiaBase != null)
            {
                var newObj = this.PersistNewItem(item as DomainModel.DomainBase.GarantiaBase);
                if (newObj != null)
                {
                    item.Key = newObj.Key;
                    return item;
                }
                else
                {
                    return item;
                }
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaBase");
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        /// <returns></returns>
        public override EntityBase PersistUpdatedItem(EntityBase item)
        {
            if (item != null && item as DomainModel.DomainBase.GarantiaBase != null)
            {
                return this.PersistUpdatedItem(item as DomainModel.DomainBase.GarantiaBase);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaBase");
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.Infrastructure.DomainBase.EntityBase"/></param>
        public override void PersistDeletedItem(EntityBase item)
        {
            DomainModel.DomainBase.GarantiaBase cliente = item as DomainModel.DomainBase.GarantiaBase;
            if (cliente != null)
            {
                this.PersistDeletedItem(cliente);
            }
            else throw new InvalidCastException("Cannot cast from DomainBase.EntityBase to DomainModel.DomainBase.GarantiaBase");
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.GarantiaBase PersistNewItem(DomainModel.DomainBase.GarantiaBase item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(@"INSERT INTO {0}
                (
                {1},
                {2},
                {3},
                {4},
                {5},
                {6},
                {7},
                {8},
                {9},
                {10},
                {11},
                {12},
                {13},
                {14},
                {15},
                {16},
                {17},
                {18},
                {19},
                {20},
                {21},
                {22},
                {23},
                {24},
                {25},
                {26},
                {27},
                {28},
                {29},
                {30},
                {31},
                {32},
                {33},
                {34},
                {35},
                {36},
                {37},
                {38},
                {39},
                {40},
                {41},
                {42},
                {43},
                {44},
                {45},
                {46},
                {47},
                {48},
                {49},
                {50},
                {51},
                {52}) ",
                GetEntityName(),    //0
                //GarantiaBaseFactory.FieldNames.GarantiaBaseId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseID_Atomo,    //1
                GarantiaBaseFactory.FieldNames.GarantiaBaseFCCReference,    //2
                GarantiaBaseFactory.FieldNames.GarantiaBaseIdentificadorGarantia,   //3
                GarantiaBaseFactory.FieldNames.GarantiaBaseCodigoBanco, //4
                GarantiaBaseFactory.FieldNames.GarantiaBaseClienteId,   //5
                GarantiaBaseFactory.FieldNames.GarantiaBaseBeneficiario,    //6
                GarantiaBaseFactory.FieldNames.GarantiaBaseIdentificacionFideicomiso,   //7
                GarantiaBaseFactory.FieldNames.GarantiaBaseFiduciariaSuperId,   //8
                GarantiaBaseFactory.FieldNames.GarantiaBaseFiduciaria,  //9
                GarantiaBaseFactory.FieldNames.GarantiaBaseDepositanteId,      //...
                GarantiaBaseFactory.FieldNames.GarantiaBaseEvaluadorId, 
                GarantiaBaseFactory.FieldNames.GarantiaBaseAdministradorId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseAseguradorId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseRevisorId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseOrigenGarantia,
                GarantiaBaseFactory.FieldNames.GarantiaBasePaisGarantiaId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaSuperId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaBladexId,
                GarantiaBaseFactory.FieldNames.GarantiaBasegetIdentificacionDocumentoGarantia,
                GarantiaBaseFactory.FieldNames.GarantiaBasegetNombreOrganismo,
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorInicial,
                "GetValorGarantiaSuperIntendencia",
                GarantiaBaseFactory.FieldNames.GarantiaBaseDescripcionDeLaGarantia,
                GarantiaBaseFactory.FieldNames.GarantiaBaseGaranteId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseAttachedToLine,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaRegistroInicial,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaFormalizacion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoRiesgo,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoGarantia,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaUltimaRevisionEvaluacion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaProximaRevisionEvaluacion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFrequenciaRevisionId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoSeguro,
                GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaRiesgoGarantiaId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseReduccionDeRiesgoPorPais,
                GarantiaBaseFactory.FieldNames.GarantiaBaseMonedaId,
                // This is not necessary anymore after WI 1497.
                // Now this column is calculated in sql with the GetUdfGarantiasValorGarantia function
                //GarantiaBaseFactory.FieldNames.GarantiaBaseValorNecesarioDeGarantia,
                "GetRatioCoberturaGarantia",
                GarantiaBaseFactory.FieldNames.GarantiaBasePorcentajeAplicableMitigacionSuperInt,
                GarantiaBaseFactory.FieldNames.GarantiaBaseComentarios,
                GarantiaBaseFactory.FieldNames.GarantiaBaseRatingGaranteId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorPolizaSeguro,
                GarantiaBaseFactory.FieldNames.GarantiaBaseNumeroPolizaSeguro,
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorMercado,
                GarantiaBaseFactory.FieldNames.GarantiaBaseInternalStatusId,                
                GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaSuperId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseSource,
                GarantiaBaseFactory.FieldNames.GarantiaBaseIndAtomo,
                GarantiaBaseFactory.FieldNames.GarantiaBaseStatusId,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaComienzoEjecucion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaCierreEjecucion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseRegion,
                GarantiaBaseFactory.FieldNames.GarantiaBaseTipoPoliza));
            builder.Append(string.Format(@"VALUES (
                {0},
                {1},
                {2},
                {3},
                {4},
                {5},
                {6},
                {7},
                {8},
                {9},
                {10},
                {11},
                {12},
                {13},
                {14},
                {15},
                {16},
                {17},
                {18},
                {19},
                {20},
                {21},
                {22},
                {23},
                {24},
                {25},
                {26},
                {27},
                {28},
                {29},
                {30},
                {31},
                {32},
                {33},
                {34},
                {35},
                {36},
                {37},
                {38},
                {39},
                {40},
                {41},
                {42},
                {43},
                {44},
                {45},
                {46},
                {47},
                {48},
                {49},
                {50},
                {51})",
                //DataHelper.GetSqlValue(item.Key),
                DataHelper.GetSqlValue(item.ID_Atomo),
                DataHelper.GetSqlValue(item.FCCReference),
                DataHelper.GetSqlValue(item.NroIncidenteWorkflow),
                DataHelper.GetSqlValue(item.CodigoBanco),
                DataHelper.GetSqlValue(item.Cliente),
                DataHelper.GetSqlValue(item.Beneficiario),
                DataHelper.GetSqlValue(item.IdentificacionFideicomiso),
                DataHelper.GetSqlValue(item.FiduciariaSuper),
                DataHelper.GetSqlValue(item.FiduciariaBladex),
                DataHelper.GetSqlValue(item.Depositante),
                DataHelper.GetSqlValue(item.Evaluador),
                DataHelper.GetSqlValue(item.Administrador),
                DataHelper.GetSqlValue(item.Asegurador),
                DataHelper.GetSqlValue(item.Revisor),
                DataHelper.GetSqlValue(item.OrigenGarantia),
                DataHelper.GetSqlValue(item.PaisGarantia),
                DataHelper.GetSqlValue(item.TipoGarantiaSuper),
                DataHelper.GetSqlValue(item.TipoGarantiaBladex),
                DataHelper.GetSqlValue(item.GetIdentificacionDocumentoGarantia()),
                DataHelper.GetSqlValue(item.NombreOrganismo),
                DataHelper.GetSqlValue(item.ValorInicial),
                DataHelper.GetSqlValue(item.GetValorGarantiaSuperIntendencia()),
                DataHelper.GetSqlValue(item.DescripcionDeLaGarantia),
                DataHelper.GetSqlValue(item.Garante),
                DataHelper.GetSqlValue(item.AttachedToLine),
                DataHelper.GetSqlValue(item.FechaRegistroInicial),
                DataHelper.GetSqlValue(item.FechaFormalizacion),
                DataHelper.GetSqlValue(item.FechaVencimientoRiesgo),
                DataHelper.GetSqlValue(item.FechaVencimientoGarantia),
                DataHelper.GetSqlValue(item.FechaUltimaRevisionEvaluacion),
                DataHelper.GetSqlValue(item.FechaProximaRevisionEvaluacion),
                DataHelper.GetSqlValue(item.FrecuenciaRevision),
                DataHelper.GetSqlValue(item.FechaVencimientoSeguro),
                DataHelper.GetSqlValue(item.CategoriaRiesgoGarantia),
                DataHelper.GetSqlValue(item.ReduccionDeRiesgoPorPais),
                DataHelper.GetSqlValue(item.Moneda),
                // This is not necessary anymore after WI 1497.
                // Now this column is calculated in sql with the GetUdfGarantiasValorGarantia function
                //DataHelper.GetSqlValue(item.ValorNecesarioDeGarantia),
                DataHelper.GetSqlValue(item.GetRatioCoberturaGarantia()),
                DataHelper.GetSqlValue(item.PorcentajeAplicableMitigacionSuperInt),
                DataHelper.GetSqlValue(item.Comentarios),
                DataHelper.GetSqlValue(item.RatingGarante),
                DataHelper.GetSqlValue(item.ValorPolizaSeguro),
                DataHelper.GetSqlValue(item.NumeroPolizaSeguro),
                DataHelper.GetSqlValue(item.ValorMercado),
                DataHelper.GetSqlValue(item.InternalStatus),
                DataHelper.GetSqlValue(item.CategoriaSuper),
                DataHelper.GetEnumAsIntegerForSql(item.Source),
                DataHelper.GetEnumAsIntegerForSql(item.IndAtomo),
                DataHelper.GetSqlValue(item.Status),
                DataHelper.GetSqlValue(item.FechaComienzoEjecucion),
                DataHelper.GetSqlValue(item.FechaCierreEjecucion),
                DataHelper.GetSqlValue(item.Region),
                DataHelper.GetSqlValue(item.TipoPoliza)
                ));
            builder.Append(" SELECT CONVERT(int, @@IDENTITY) ");
            object newKey = this.Database.ExecuteScalar(this.Database.GetSqlStringCommand(string.Format(builder.ToString(), GetEntityName())));
            item.Key = newKey;
            return item;
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <returns></returns>
        protected override DomainModel.DomainBase.GarantiaBase PersistUpdatedItem(DomainModel.DomainBase.GarantiaBase item)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("UPDATE {0} SET ", GetEntityName());

            builder.Append(string.Format("{0} = {1}",
            GarantiaBaseFactory.FieldNames.GarantiaBaseID_Atomo,
            DataHelper.GetSqlValue(item.ID_Atomo)));

            
            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFCCReference,
                DataHelper.GetSqlValue(item.FCCReference)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseIdentificadorGarantia,
                DataHelper.GetSqlValue(item.NroIncidenteWorkflow)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseCodigoBanco,
                DataHelper.GetSqlValue(item.CodigoBanco)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseClienteId,
                DataHelper.GetSqlValue(item.Cliente)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseBeneficiario,
                DataHelper.GetSqlValue(item.Beneficiario)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseIdentificacionFideicomiso,
                DataHelper.GetSqlValue(item.IdentificacionFideicomiso)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFiduciariaSuperId,
                DataHelper.GetSqlValue(item.FiduciariaSuper)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFiduciaria,
                DataHelper.GetSqlValue(item.FiduciariaBladex)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseDepositanteId,
                DataHelper.GetSqlValue(item.Depositante)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseEvaluadorId,
                DataHelper.GetSqlValue(item.Evaluador)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseAdministradorId,
                DataHelper.GetSqlValue(item.Administrador)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseAseguradorId,
                DataHelper.GetSqlValue(item.Asegurador)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseRevisorId,
                DataHelper.GetSqlValue(item.Revisor)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseOrigenGarantia,
                DataHelper.GetSqlValue(item.OrigenGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBasePaisGarantiaId,
                DataHelper.GetSqlValue(item.PaisGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaSuperId,
                DataHelper.GetSqlValue(item.TipoGarantiaSuper)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseTipoGarantiaBladexId,
                DataHelper.GetSqlValue(item.TipoGarantiaBladex)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBasegetIdentificacionDocumentoGarantia,
                DataHelper.GetSqlValue(item.GetIdentificacionDocumentoGarantia())));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBasegetNombreOrganismo,
                DataHelper.GetSqlValue(item.NombreOrganismo)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorInicial,
                DataHelper.GetSqlValue(item.ValorInicial)));

            builder.Append(string.Format(",{0} = {1}",
                "GetValorGarantiaSuperIntendencia",
                DataHelper.GetSqlValue(item.GetValorGarantiaSuperIntendencia())));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseDescripcionDeLaGarantia,
                DataHelper.GetSqlValue(item.DescripcionDeLaGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseGaranteId,
                DataHelper.GetSqlValue(item.Garante)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseAttachedToLine,
                DataHelper.GetSqlValue(item.AttachedToLine)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaRegistroInicial,
                DataHelper.GetSqlValue(item.FechaRegistroInicial)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaFormalizacion,
                DataHelper.GetSqlValue(item.FechaFormalizacion)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoRiesgo,
                DataHelper.GetSqlValue(item.FechaVencimientoRiesgo)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoGarantia,
                DataHelper.GetSqlValue(item.FechaVencimientoGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaUltimaRevisionEvaluacion,
                DataHelper.GetSqlValue(item.FechaUltimaRevisionEvaluacion)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaProximaRevisionEvaluacion,
                DataHelper.GetSqlValue(item.FechaProximaRevisionEvaluacion)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFrequenciaRevisionId,
                DataHelper.GetSqlValue(item.FrecuenciaRevision)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaVencimientoSeguro,
                DataHelper.GetSqlValue(item.FechaVencimientoSeguro)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaRiesgoGarantiaId,
                DataHelper.GetSqlValue(item.CategoriaRiesgoGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseReduccionDeRiesgoPorPais,
                DataHelper.GetSqlValue(item.ReduccionDeRiesgoPorPais)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseMonedaId,
                DataHelper.GetSqlValue(item.Moneda)));

            // This is not necessary anymore after WI 1497.
            // Now this column is calculated in sql with the GetUdfGarantiasValorGarantia function

            //builder.Append(string.Format(",{0} = {1}",
            //    GarantiaBaseFactory.FieldNames.GarantiaBaseValorNecesarioDeGarantia,
            //    DataHelper.GetSqlValue(item.ValorNecesarioDeGarantia)));

            builder.Append(string.Format(",{0} = {1}",
                "GetRatioCoberturaGarantia",
                DataHelper.GetSqlValue(item.GetRatioCoberturaGarantia())));

            
            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBasePorcentajeAplicableMitigacionSuperInt,
                DataHelper.GetSqlValue(item.PorcentajeAplicableMitigacionSuperInt)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseComentarios,
                DataHelper.GetSqlValue(item.Comentarios)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseRatingGaranteId,
                DataHelper.GetSqlValue(item.RatingGarante)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseNumeroPolizaSeguro,
                DataHelper.GetSqlValue(item.NumeroPolizaSeguro)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorPolizaSeguro,
                DataHelper.GetSqlValue(item.ValorPolizaSeguro)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseValorMercado,
                DataHelper.GetSqlValue(item.ValorMercado)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseInternalStatusId,
                DataHelper.GetSqlValue(item.InternalStatus)));

            builder.Append(string.Format(",{0} = {1}",
            GarantiaBaseFactory.FieldNames.GarantiaBaseSource,
            DataHelper.GetEnumAsIntegerForSql(item.Source)));

            builder.Append(string.Format(",{0} = {1}",
            GarantiaBaseFactory.FieldNames.GarantiaBaseIndAtomo,
            DataHelper.GetEnumAsIntegerForSql(item.IndAtomo)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseStatusId,
                DataHelper.GetSqlValue(item.Status)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaComienzoEjecucion,
                DataHelper.GetSqlValue(item.FechaComienzoEjecucion)));

            builder.Append(string.Format(",{0} = {1}",
                GarantiaBaseFactory.FieldNames.GarantiaBaseFechaCierreEjecucion,
                DataHelper.GetSqlValue(item.FechaCierreEjecucion)));

            // Ticket #1619
            builder.Append(string.Format(",{0} = {1}",
            GarantiaBaseFactory.FieldNames.GarantiaBaseRegion,
            DataHelper.GetSqlValue(item.Region)));

            // Ticket #1619
            builder.Append(string.Format(",{0} = {1}",
            GarantiaBaseFactory.FieldNames.GarantiaBaseTipoPoliza,
            DataHelper.GetSqlValue(item.TipoPoliza)));

            builder.Append(" ");
            string test = item.Key.ToString();
            builder.Append(this.BuildBaseWhereClause(int.Parse(item.Key.ToString())));

            this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
            return item;
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">The item of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        protected override void PersistDeletedItem(DomainModel.DomainBase.GarantiaBase item)
        {
            // We could delete related objects here, and then, call the base method to delete the entity.
            base.PersistDeletedItem(item);
        }

        #endregion

        #region Private Callback and Helper Methods



        /// <summary>
        /// Appends the categoria super.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.Object"/></param>
        private void AppendCategoriaSuper(DomainModel.DomainBase.GarantiaBase garantia, object categoriaSuperId)
        {
        }

        /// <summary>
        /// Appends the status.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="statusId">The status id of type <see cref="System.Object"/></param>
        private void AppendStatus(DomainModel.DomainBase.GarantiaBase garantia, object statusId)
        {
            garantia.Status = new StatusService().GetById(Convert.ToString(statusId))
                ?? new StatusService().GetNormalStatus();
        }

        /// <summary>
        /// Appends the internal status.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="internalStatusId">The internal status id of type <see cref="System.Object"/></param>
        private void AppendInternalStatus(DomainModel.DomainBase.GarantiaBase garantia, object internalStatusId)
        {
            garantia.InternalStatus = new InternalStatusService().GetById(Convert.ToString(internalStatusId))
                ?? new InternalStatusService().GetActiveStatus();
        }

        /// <summary>
        /// Appends the calificaciones riesgo.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="CalificacionesRiesgoId">The calificaciones riesgo id of type <see cref="System.Object"/></param>
        private void AppendCalificacionesRiesgo(DomainModel.DomainBase.GarantiaBase garantia, object CalificacionesRiesgoId)
        {
            garantia.RatingGarante = new CalificacionesRiesgoService().GetById(Convert.ToString(CalificacionesRiesgoId));
        }

        /// <summary>
        /// Appends the monedas.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="MonedasId">The monedas id of type <see cref="System.Object"/></param>
        private void AppendMonedas(DomainModel.DomainBase.GarantiaBase garantia, object MonedasId)
        {
            garantia.Moneda = new MonedasService().GetById(Convert.ToString(MonedasId));
        }

        /// <summary>
        /// Appends the categoria riesgo garantia.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="CategoriaRiesgoGarantiaId">The categoria riesgo garantia id of type <see cref="System.Object"/></param>
        private void AppendCategoriaRiesgoGarantia(DomainModel.DomainBase.GarantiaBase garantia, object CategoriaRiesgoGarantiaId)
        {
            garantia.CategoriaRiesgoGarantia = new CategoriaRiesgoGarantiaService().GetById(Convert.ToString(CategoriaRiesgoGarantiaId));
        }

        /// <summary>
        /// Appends the frequencias.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="FrecuenciasId">The frecuencias id of type <see cref="System.Object"/></param>
        private void AppendFrequencias(DomainModel.DomainBase.GarantiaBase garantia, object FrecuenciasId)
        {
            garantia.FrecuenciaRevision = new FrecuenciasService().GetById(Convert.ToString(FrecuenciasId));
        }

        /// <summary>
        /// Appends the tipo garantia super.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="TipoGarantiaSuperId">The tipo garantia super id of type <see cref="System.Object"/></param>
        private void AppendTipoGarantiaSuper(DomainModel.DomainBase.GarantiaBase garantia, object TipoGarantiaSuperId)
        {
            garantia.TipoGarantiaSuper = new TipoGarantiaSuperService().GetById(Convert.ToString(TipoGarantiaSuperId));
        }

        /// <summary>
        /// Appends the tipo garantia bladex.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="TipoGarantiaBladexId">The tipo garantia bladex id of type <see cref="System.Object"/></param>
        private void AppendTipoGarantiaBladex(DomainModel.DomainBase.GarantiaBase garantia, object TipoGarantiaBladexId)
        {
            garantia.TipoGarantiaBladex = new TipoGarantiaBladexService().GetById(Convert.ToString(TipoGarantiaBladexId));
        }

        /// <summary>
        /// Appends the actor helper.
        /// </summary>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        /// <returns></returns>
        private DomainModel.DomainBase.Actor AppendActorHelper(object actorId)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, DomainModel.DomainBase.Actor>(this.UnitOfWork);
            return repository.FindBy(actorId);
        }

        /// <summary>
        /// Appends the valor necesario de garantia.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Object"/></param>
        private void AppendValorNecesarioDeGarantia(DomainModel.DomainBase.GarantiaBase garantia, object garantiaId)
        {
            // Commented after WI 1497 (Now this column is calculated in sql with the GetUdfGarantiasValorGarantia function
            //garantia.ValorNecesarioDeGarantia = this.GetUdfGarantiasValorGarantia(garantia.GetKeyAs<int>());
        }

        /// <summary>
        /// Appends the depositante.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        private void AppendDepositante(DomainModel.DomainBase.GarantiaBase garantia, object actorId)
        {
            garantia.Depositante = this.AppendActorHelper(actorId);
        }

        /// <summary>
        /// Appends the evaluador.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        private void AppendEvaluador(DomainModel.DomainBase.GarantiaBase garantia, object actorId)
        {
            garantia.Evaluador = this.AppendActorHelper(actorId);
        }

        /// <summary>
        /// Appends the revisor.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        private void AppendRevisor(DomainModel.DomainBase.GarantiaBase garantia, object actorId)
        {
            garantia.Revisor = this.AppendActorHelper(actorId);
        }

        /// <summary>
        /// Appends the asegurador.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        private void AppendAsegurador(DomainModel.DomainBase.GarantiaBase garantia, object actorId)
        {
            garantia.Asegurador = this.AppendActorHelper(actorId);
        }

        /// <summary>
        /// Appends the administrador.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="actorId">The actor id of type <see cref="System.Object"/></param>
        private void AppendAdministrador(DomainModel.DomainBase.GarantiaBase garantia, object actorId)
        {
            garantia.Administrador = this.AppendActorHelper(actorId);
        }

        /// <summary>
        /// Appends the pais.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="CodigoPais">The codigo pais of type <see cref="System.Object"/></param>
        private void AppendPais(DomainModel.DomainBase.GarantiaBase garantia, object CodigoPais)
        {
            garantia.PaisGarantia = new PaisService().GetById(Convert.ToString(CodigoPais));
        }

        /// <summary>
        /// Appends the region.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="CodigoRegion">The codigo Region of type <see cref="System.Object"/></param>
        private void AppendRegion(DomainModel.DomainBase.GarantiaBase garantia, object CodigoRegion)
        {
            garantia.Region = new RegionService().GetById(Convert.ToString(CodigoRegion));
        }

        /// <summary>
        /// Appends the TipoPoliza.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="IdTipoPoliza">The codigo TipoPoliza of type <see cref="System.Object"/></param>
        private void AppendTipoPoliza(DomainModel.DomainBase.GarantiaBase garantia, object IdTipoPoliza)
        {
            garantia.TipoPoliza = new TipoPolizaService().GetById(Convert.ToString(IdTipoPoliza));
        }

        /// <summary>
        /// Appends the garante.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="garanteId">The garante id of type <see cref="System.Object"/></param>
        private void AppendGarante(DomainModel.DomainBase.GarantiaBase garantia, object garanteId)
        {
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, DomainModel.DomainBase.Cliente>(this.UnitOfWork);
            garantia.Garante = repository.FindBy(garanteId);
        }

        /// <summary>
        /// Appends the cliente.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="clienteId">The cliente id of type <see cref="System.Object"/></param>
        private void AppendCliente(DomainModel.DomainBase.GarantiaBase garantia, object clienteId)
        {
            IClienteRepository repository = RepositoryFactory.GetRepository<IClienteRepository, DomainModel.DomainBase.Cliente>(this.UnitOfWork);
            garantia.Cliente = repository.FindBy(clienteId);
        }

        /// <summary>
        /// Appends the fiduciarias.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="fiduciariaSuperId">The fiduciaria super id of type <see cref="System.Object"/></param>
        private void AppendFiduciarias(DomainModel.DomainBase.GarantiaBase garantia, object fiduciariaSuperId)
        {
            IFiduciariasRepository repository = RepositoryFactory.GetRepository<IFiduciariasRepository, Bladex.Garantias.DomainModel.DomainBase.Fiduciarias>(this.UnitOfWork);
            garantia.FiduciariaSuper = repository.FindBy(fiduciariaSuperId);
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
            return string.Format("SELECT * FROM {0} C ", GetEntityName());
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
            return "GarantiaBase";
        }

        /// <summary>
        /// Returns the Key of the entity
        /// </summary>
        /// <returns>Primary Key of the entity.</returns>
        protected override string GetKeyFieldName()
        {
            return GarantiaBaseFactory.FieldNames.GarantiaBaseId;
        }

        /// <summary>
        /// Obtiene la garantia base que corresponde al Id del Atomo de Teinsa asociada
        /// Tener en cuenta que para garantias creadas en el sistema Novaris, no posee dicho ID
        /// </summary>
        /// <param name="IdAtomo"></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.GarantiaBase GetByFccRef(String FCCRef)
        {
            StringBuilder query = new StringBuilder(this.GetBaseQuery());
            query = query.Append(String.Format(" WHERE [{0}] = '{1}'", GarantiaBaseFactory.FieldNames.GarantiaBaseFCCReference, FCCRef));
            return this.BuildEntitiesFromSql(query.ToString()).FirstOrDefault();
        }
        /// <summary>
        /// Trae los TeinsaIds(FCCReference) de Garantia Base 
        /// </summary>
        /// <returns>IDictionary<GarantiaBase.FCCReference, GarantiaBase.ID></returns>
        public IDictionary<String, int> GetFccReferences()
        {
            StringBuilder query = new StringBuilder(String.Format("SELECT DISTINCT [{0}] GarantiaRef, [{1}] IdNovaris FROM [{2}] ", GarantiaBaseFactory.FieldNames.GarantiaBaseFCCReference, GarantiaBaseFactory.FieldNames.GarantiaBaseId, this.GetEntityName()));
            query = query.Append(String.Format(" WHERE NOT [{0}] IS NULL", GarantiaBaseFactory.FieldNames.GarantiaBaseFCCReference));
            using (IDataReader reader = this.ExecuteReader(query.ToString()))
            {
                Dictionary<String, int> ret = new Dictionary<String, int>();
                while (reader.Read())
                {

                    ret.Add(DataHelper.GetString(reader["GarantiaRef"]), DataHelper.GetInteger(reader["IdNovaris"]));
                }

                return ret;
            }
        }

        public decimal GetUdfGarantiasValorGarantia(int garantiaId)
        {
            string query = string.Format("SELECT {0}({1}) as ValorGarantia", _UDF_GARANTIAS_VALOR_GARANTIA, garantiaId);
            object result = this.Database.ExecuteScalar(CommandType.Text, query);
            if (result == null || result == DBNull.Value)
                return default(decimal);
            else
                return (decimal)result;
        }

        public Boolean GetUdfValidationUnblockGuarantee(int garantiaId)
        {
            string query = string.Format("SELECT {0}({1}) as isValid", _UDF_GARANTIAS_VALIDATION_UNBLOCK_GUARANTEE, garantiaId);
            object result = this.Database.ExecuteScalar(CommandType.Text, query);
            if (result == null || result == DBNull.Value)
                return false;
            else
                return (bool)result;
        }

        public List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> SearchGarantias(string SearchText)
        {
            List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> listado = new List<DomainModel.DomainBase.GarantiaBaseRow>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("EXEC SP_SearchGarantias @SearchText");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, "@SearchText", DbType.String, SearchText);                    

                    using (IDataReader reader = this.Database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {

                            Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow item = new DomainModel.DomainBase.GarantiaBaseRow
                            {
                                Key = DataHelper.GetInteger(reader["Key"]),
                                CategoriaGarantia = DataHelper.GetString(reader["Categoria"]),
                                CategoriaGarantiaId = DataHelper.GetString(reader["CategoriaId"]),                                
                                Cliente = DataHelper.GetString(reader["Cliente"]),
                                IdentificadorGarantia = DataHelper.GetString(reader["IdentificadorGarantia"]),
                                TipoGarantia = DataHelper.GetString(reader["TipoGarantia"]),
                                ValorInicial = DataHelper.GetNullableDouble(reader["ValorInicial"]).Value,
                                ValorMercado = DataHelper.GetNullableDouble(reader["ValorMercado"]).Value,
                                IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])                                 
                            };

                            listado.Add(item);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error en SearchGarantias.", ex);
            }

            return listado;
        }

        public List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> SearchGarantiasPaged(string SearchText, int Page, int RecsPerPage)
        {
            List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> listado = new List<DomainModel.DomainBase.GarantiaBaseRow>();
            
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("EXEC SP_SearchGarantias_Paged @SearchText,@Page,@RecsPerPage");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, "@SearchText", DbType.String, SearchText);
                    this.Database.AddInParameter(cmd, "@Page", DbType.Int32, Page);
                    this.Database.AddInParameter(cmd, "@RecsPerPage", DbType.Int32, RecsPerPage);

                    using (IDataReader reader = this.Database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {

                            Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow item = new DomainModel.DomainBase.GarantiaBaseRow
                            {
                                Key = DataHelper.GetInteger(reader["Key"]),
                                CategoriaGarantia = DataHelper.GetString(reader["Categoria"]),
                                Cliente = DataHelper.GetString(reader["Cliente"]),
                                IdentificadorGarantia = DataHelper.GetString(reader["IdentificadorGarantia"]),
                                TipoGarantia = DataHelper.GetString(reader["TipoGarantia"]),
                                ValorInicial = DataHelper.GetNullableDouble(reader["ValorInicial"]).Value,
                                ValorMercado = DataHelper.GetNullableDouble(reader["ValorMercado"]).Value,
                                IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])                                 
                            };

                            listado.Add(item);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error en SearchGarantiasPaged.", ex);
            }

            return listado;
        }

        #region IGarantiaBaseRepository<GarantiaBase> Members

        /// <summary>
        /// Gets all deleted.
        /// </summary>
        /// <returns></returns>
        public IList<DomainModel.DomainBase.GarantiaBase> GetAllDeleted()
        {
            StringBuilder query = new StringBuilder(this.GetBaseQuery());
            query = query.Append(string.Format(" WHERE [{0}] = {1}", GarantiaBaseFactory.FieldNames.GarantiaBaseInternalStatusId, new InternalStatusService().GetDeletedStatus().Key));
            return this.BuildEntitiesFromSql(query.ToString());
        }

        public List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> GetAllUnknown(string SearchText)
        {
            List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow> listado = new List<DomainModel.DomainBase.GarantiaBaseRow>();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("EXEC SP_SearchGarantias_Unknown @SearchText");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, "@SearchText", DbType.String, SearchText);

                    using (IDataReader reader = this.Database.ExecuteReader(cmd))
                    {
                        while (reader.Read())
                        {

                            Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow item = new DomainModel.DomainBase.GarantiaBaseRow
                            {
                                Key = DataHelper.GetInteger(reader["Key"]),
                                CategoriaGarantia = DataHelper.GetString(reader["Categoria"]),
                                CategoriaGarantiaId = DataHelper.GetString(reader["CategoriaId"]),
                                Cliente = DataHelper.GetString(reader["Cliente"]),
                                IdentificadorGarantia = DataHelper.GetString(reader["IdentificadorGarantia"]),
                                TipoGarantia = DataHelper.GetString(reader["TipoGarantia"]),
                                ValorInicial = DataHelper.GetNullableDouble(reader["ValorInicial"]).Value,
                                ValorMercado = DataHelper.GetNullableDouble(reader["ValorMercado"]).Value,
                                IsReadOnly = DataHelper.GetBoolean(reader["IsReadOnly"])
                            };

                            listado.Add(item);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error en GetAllUnknown.", ex);
            }

            return listado;
        }

        public string GetOriginalInternalStatus(string GarantiaId)
        {
            string result = string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("select [dbo].[UDF_Garantias_GetOriginal_InternalStatus] (@GarantiaId)");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, "@GarantiaId", DbType.String, GarantiaId);

                    result = Convert.ToString(this.Database.ExecuteScalar(cmd));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error en GetOriginalInternalStatus para GarantiaId={0}.", GarantiaId), ex);
            }

            return result;
        }

        public string DisableGuaranteeType(string UserId, string TipoGarantiaSuperId)
        {
            string result = string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("EXEC SP_Garantias_DisableGuaranteeType @UserId, @TipoGarantiaSuperId");

            try
            {
                using (DbCommand cmd = this.Database.GetSqlStringCommand(strBuilder.ToString()))
                {
                    this.Database.AddInParameter(cmd, "@UserId", DbType.String, UserId);
                    this.Database.AddInParameter(cmd, "@TipoGarantiaSuperId", DbType.String, TipoGarantiaSuperId);

                    result = Convert.ToString(this.Database.ExecuteScalar(cmd));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error en DisableGuaranteeType para TipoGarantiaSuperId={0}.", TipoGarantiaSuperId), ex);
            }

            return result;
        }

        /// <summary>
        /// Changes the type.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <param name="newCategoriaSuper">The new categoria super of type <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/></param>
        /// <returns></returns>
        public bool ChangeType(DomainModel.DomainBase.GarantiaBase garantia, Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper newCategoriaSuper)
        {
            bool result = true;
            try
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

                builder.Append(string.Format("{0} = {1}",
                                             GarantiaBaseFactory.FieldNames.GarantiaBaseCategoriaSuperId,
                                             DataHelper.GetSqlValue(newCategoriaSuper.Key)));

                builder.Append(" ");
                builder.Append(this.BuildBaseWhereClause(garantia.Key));

                this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
                
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error intentando cambiar el tipo de garantia a la garantia con identificador {0}.", garantia.GetKeyAs<int>()), ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Actualiza el internal Status
        /// </summary>
        /// <param name="garantia"></param>
        /// <param name="internalStatus"></param>
        public void SetInternalStatus(int garantiaId, Bladex.Garantias.DomainModel.DomainBase.InternalStatus internalStatus)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendFormat("UPDATE {0} SET ", this.GetEntityName());

                builder.Append(string.Format("{0} = {1}",
                                             GarantiaBaseFactory.FieldNames.GarantiaBaseInternalStatusId,
                                             DataHelper.GetSqlValue(internalStatus.Key)));

                builder.Append(" ");
                builder.Append(" WHERE ");
                builder.Append(GarantiaBaseFactory.FieldNames.GarantiaBaseId + " = " + DataHelper.GetSqlValue(garantiaId));

                this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));

            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error intentando cambiar el InternalStatus de la garantia con identificador {0}.", garantiaId), ex);
                
            }
        }

        /// <summary>
        /// Writes the record.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        [Obsolete("Inherited method. Obsolete.")]
        public void WriteRecord(DomainModel.DomainBase.GarantiaBase garantia)
        {
            
        }

        #endregion
    }
}
