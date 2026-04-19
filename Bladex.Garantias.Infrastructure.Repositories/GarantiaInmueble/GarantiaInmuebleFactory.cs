using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaInmueble
{
    internal class GarantiaInmuebleFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaInmueble> 
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string ValorEvaluacionVentaRapida = "ValorEvaluacionVentaRapida";
            public const string AseguradorSuperId = "AseguradorSuper";
            public const string InscripcionRegistroPublico = "InscripcionRegistroPublico";
            public const string ValorAvaluo = "ValorAvaluo";
            public const string NumeroDeFinca = "NumeroDeFinca";
            public const string Id = "ID";
            public const string FechaInicialAvaluo = "FechaInicialAvaluo";
            public const string FechaVencimientoAvaluo = "FechaVencimientoAvaluo";
            public const string ValorTotalAvaluo = "ValorTotalAvaluo";
        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.GarantiaInmueble BuildEntity(System.Data.IDataReader reader)
        {

            IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>();

            DomainModel.DomainBase.GarantiaBase garantiaBase = repository.FindBy(reader[FieldNames.Id].ToString());
            if (garantiaBase == null)
            {
                throw new Exception("Cannot find garantia Base id: " + reader[FieldNames.Id].ToString());
            }

            DomainModel.DomainBase.GarantiaInmueble garantia = AutoMapper.Mapper.Map<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaInmueble>(garantiaBase);

            garantia.InscripcionRegistroPublico = reader[FieldNames.InscripcionRegistroPublico].ToString();
            garantia.NumeroDeFinca = reader[FieldNames.NumeroDeFinca].ToString();
            garantia.ValorAvaluo = DataHelper.GetDecimal(reader[FieldNames.ValorAvaluo]);
            garantia.ValorEvaluacionVentaRapida = DataHelper.GetDecimal(reader[FieldNames.ValorEvaluacionVentaRapida]);
            garantia.FechaInicialAvaluo = DataHelper.GetDateTime(reader[FieldNames.FechaInicialAvaluo]);
            garantia.FechaVencimientoAvaluo = DataHelper.GetDateTime(reader[FieldNames.FechaVencimientoAvaluo]);
            garantia.ValorTotalAvaluo = DataHelper.GetDecimal(reader[FieldNames.ValorTotalAvaluo]);

            return garantia;
        }

        #endregion
    }
}
