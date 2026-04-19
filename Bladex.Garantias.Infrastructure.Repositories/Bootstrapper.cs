using Bladex.Garantias.DomainModel.Components.MakerChecker;

namespace Bladex.Garantias.Infrastructure.Repositories
{
    using Cliente;

    /// <summary>
    /// The Bootstrapper class
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Setups this instance.
        /// </summary>
        public static void Setup()
        {
            SetupAutomapper();
        }

        /// <summary>
        /// Setups the automapper.
        /// </summary>
        private static void SetupAutomapper()
        {
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaOtra>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaOtra>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaPrenda>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaPrenda>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaMueble>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaMueble>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaInmueble>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaInmueble>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaDeposito>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaDeposito>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaDepositoOtroBanco>, MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>>();
            AutoMapper.Mapper.CreateMap<MakerCheckerObject<DomainModel.DomainBase.GarantiaBase>, MakerCheckerObject<DomainModel.DomainBase.GarantiaDepositoOtroBanco>>();

            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.IMPORT_TH_ATOMO_GARANTIAS>();

            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Cliente>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src[ClienteFactory.FieldNames.ClienteId]))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src[ClienteFactory.FieldNames.Nombre]))
                .ForMember(dest => dest.GrupoEconomico, opt => opt.MapFrom(src => src[ClienteFactory.FieldNames.GrupoEconomico]))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src[ClienteFactory.FieldNames.ClienteRating]))
                .ForMember(dest => dest.Pais, opt => opt.ResolveUsing<PaisResolver>());
            
            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.Cliente, SqlMappers.SqlQueryBuilder>()
                .ForMember(dest => dest.SelectQuery, opt => opt.MapFrom(src => "SELECT * FROM {0}"))
                .ForMember(dest => dest.UpdateQuery, opt => opt.MapFrom(src => string.Concat("UPDATE {0} SET ", string.Format("{0} = {1}, {2} = {3}, {4} = {5}, {6} = {7}", "CountryID", DataHelper.GetSqlValue(src.Pais.Key), "EconomicGroup", DataHelper.GetSqlValue(src.GrupoEconomico), "Nombre", DataHelper.GetSqlValue(src.Nombre), "Rating", DataHelper.GetSqlValue(src.Rating)))))
                .ForMember(dest => dest.InsertQuery, opt => opt.MapFrom(src => string.Concat("INSERT INTO {0} ", string.Format("({0},{1},{2},{3},{4})", "ID", "Nombre", "CountryID", "Rating", "EconomicGroup"), string.Format(" VALUES ({0},{1},{2},{3},{4})", DataHelper.GetSqlValue(src.Key), DataHelper.GetSqlValue(src.Nombre), DataHelper.GetSqlValue(src.Pais.Key), DataHelper.GetSqlValue(src.Rating), DataHelper.GetSqlValue(src.GrupoEconomico)))))
                .ForMember(dest => dest.DeleteQuery, opt => opt.MapFrom(src => "DELETE FROM {0}"));

            // Mapeo de garantia base al resto de garantias.
            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaDeposito>()
                .ForMember(src => src.BancoLocalSuper, opt => opt.Ignore())
                .ForMember(src => src.selectedOperationId ,opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaDepositoOtroBanco>()
            .ForMember(src => src.BancoSuper, opt => opt.Ignore())
            .ForMember(src => src.selectedOperationId, opt => opt.Ignore());
            

            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaInmueble>()
                .ForMember(src => src.AseguradorSuper, opt => opt.Ignore())
                .ForMember(src => src.InscripcionRegistroPublico, opt => opt.Ignore())
                .ForMember(src => src.ValorEvaluacionVentaRapida, opt => opt.Ignore())
                .ForMember(src => src.FechaInicialAvaluo, opt => opt.Ignore())
                .ForMember(src => src.FechaVencimientoAvaluo, opt => opt.Ignore())
                .ForMember(src => src.ValorTotalAvaluo, opt => opt.Ignore())
                .ForMember(src => src.ValorAvaluo, opt => opt.Ignore())
                .ForMember(src => src.selectedOperationId, opt => opt.Ignore())
                .ForMember(src => src.NumeroDeFinca, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaMueble>()
                .ForMember(src => src.selectedOperationId, opt => opt.Ignore())
                .ForMember(src => src.AseguradorSuper, opt => opt.Ignore())
                .ForMember(src => src.FechaInicialAvaluo, opt => opt.Ignore())
                .ForMember(src => src.FechaVencimientoAvaluo, opt => opt.Ignore())
                .ForMember(src => src.ValorTotalAvaluo, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaOtra>()
                .ForMember(src => src.Emisor, opt => opt.Ignore())
                .ForMember(src => src.selectedOperationId, opt => opt.Ignore())
                .ForMember(src => src.AvalComponent, opt => opt.Ignore())
                .ForMember(src => src.NroReferencia, opt => opt.Ignore())
                .ForMember(src => src.Avales, opt => opt.Ignore());
                

            AutoMapper.Mapper.CreateMap<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaPrenda>()
                .ForMember(src => src.Emisor, opt => opt.Ignore())
                .ForMember(src => src.TipoInstrumentoFinanciero, opt => opt.Ignore())
                .ForMember(src => src.CalificacionEmision, opt => opt.Ignore())
                .ForMember(src => src.PaisEmision, opt => opt.Ignore())
                .ForMember(src => src.IdentificadorPrenda, opt => opt.Ignore())
                .ForMember(src => src.selectedOperationId, opt => opt.Ignore())
                .ForMember(src => src.CalificacionEmisor, opt => opt.Ignore())
                .ForMember(src => src.FechaInicialAvaluo, opt => opt.Ignore())
                .ForMember(src => src.FechaVencimientoAvaluo, opt => opt.Ignore())
                .ForMember(src => src.ValorTotalAvaluo, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.SyncLog>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Pais>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Region>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.TipoPoliza>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Actor>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Aseguradoras>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Aval>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Avaluadoras>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Bancos>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.CalificacionesRiesgo>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.CategoriaRiesgoGarantia>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.CategoriaSuper>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Fiduciarias>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Frecuencias>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaDeposito>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaDepositoOtroBanco>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaInmueble>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaMueble>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaOtra>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaPrenda>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GrupoRiesgoGarantia>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.InstrumentoFinanciero>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.Monedas>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.InternalStatus>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.TipoAval>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.TipoGarantiaBladex>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.TipoGarantiaSuper>();
            AutoMapper.Mapper.CreateMap<System.Data.IDataReader, DomainModel.DomainBase.GarantiaContrato>();

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
