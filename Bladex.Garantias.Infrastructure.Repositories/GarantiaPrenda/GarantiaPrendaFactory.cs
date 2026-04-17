using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaPrenda
{
    internal class GarantiaPrendaFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaPrenda> 
    {
        #region Field Names

        internal static class FieldNames
        {

            public const string Id = "ID"; 
            public const string EmisorId = "Emisor";
            public const string TipoInstrumentoFinancieroId = "TipoInstrumentoFinanciero";
            public const string CalificacionEmisionId = "CalificacionEmision";
            public const string CalificacionEmisorId = "CalificacionEmisor";
            public const string PaisEmision = "PaisEmision";
            public const string IdentificadorPrenda = "IdentificadorPrenda";
            public const string FechaInicialAvaluo = "FechaInicialAvaluo";
            public const string FechaVencimientoAvaluo = "FechaVencimientoAvaluo";
            public const string ValorTotalAvaluo = "ValorTotalAvaluo";
        }

        

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.GarantiaPrenda BuildEntity(System.Data.IDataReader reader)
        {
            IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>();

            DomainModel.DomainBase.GarantiaBase garantiaBase = repository.FindBy(reader[FieldNames.Id].ToString());
            if (garantiaBase == null)
            {
                throw new Exception("Cannot find garantia Base id: " + reader[FieldNames.Id].ToString());
            }


            DomainModel.DomainBase.GarantiaPrenda garantia = AutoMapper.Mapper.Map<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaPrenda>(garantiaBase);

            garantia.IdentificadorPrenda = reader[FieldNames.IdentificadorPrenda].ToString();
            garantia.FechaInicialAvaluo = DataHelper.GetDateTime(reader[FieldNames.FechaInicialAvaluo]);
            garantia.FechaVencimientoAvaluo = DataHelper.GetDateTime(reader[FieldNames.FechaVencimientoAvaluo]);
            garantia.ValorTotalAvaluo = DataHelper.GetDecimal(reader[FieldNames.ValorTotalAvaluo]);

            return garantia;

        }

        #endregion
    }
}
