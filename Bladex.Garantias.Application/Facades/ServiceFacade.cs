using Bladex.Garantias.DomainModel.Services.Components.MakerChecker;

namespace Bladex.Garantias.Application.Facades
{
    using DomainModel.Services;

    /// <summary>
    /// Singleton Service Facade used to access to all application services.
    /// </summary>
    public class ServiceFacade
    {
        /// <summary>
        /// ServiceFacade Instance 
        /// </summary>
        private static volatile ServiceFacade instance;

        /// <summary>
        ///   <see cref="System.Object"/> used to synchronize the access.
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceFacade"/> class from being created.
        /// </summary>
        private ServiceFacade()
        {
            this.CategoriaRiesgoGarantiaService = new CategoriaRiesgoGarantiaService();
            this.TipoGarantiaSuperService = new TipoGarantiaSuperService();
            this.TipoGarantiaBladexService = new TipoGarantiaBladexService();
            this.TipoAvalService = new TipoAvalService();
            this.InternalStatusService = new InternalStatusService();
            this.MonedasService = new MonedasService();
            this.InstrumentoFinancieroService = new InstrumentoFinancieroService();
            this.GrupoRiesgoGarantiaService = new GrupoRiesgoGarantiaService();
            this.GarantiaService = new GarantiaService();
            this.GarantiaPrendaService = new GarantiaPrendaService();
            this.GarantiaOtraService = new GarantiaOtraService();
            this.GarantiaMuebleService = new GarantiaMuebleService();
            this.GarantiaInmuebleService = new GarantiaInmuebleService();
            this.ClienteService = new ClienteService();            
            this.ActorService = new ActorService();
            this.AvalService = new AvalService();
            this.PaisService = new PaisService();
            this.RegionService = new RegionService();
            this.AseguradorasService = new AseguradorasService();
            this.AvaluadoraService = new AvaluadoraService();
            this.BancosService = new BancosService();
            this.CalificacionesRiesgoService = new CalificacionesRiesgoService();
            this.CategoriaSuperService = new CategoriaSuperService();
            this.FiduciariasService = new FiduciariasService();
            this.FrecuenciasService = new FrecuenciasService();
            this.GarantiaDepositoOtroBancoService = new GarantiaDepositoOtroBancoService();
            this.GarantiaDepositoService = new GarantiaDepositoService();
            this.IMPORT_TH_ATOMO_GARANTIASService = new IMPORT_TH_ATOMO_GARANTIASService();
            this.SyncLogService = new SyncLogService();
            this.GarantiaContratoService = new GarantiaContratoService();
            this.LimitInformationService = new LimitInformationService();
            this.TooltipService = new TooltipService();
            this.MakerCheckerService = new MakerCheckerService();
            this.StatusService = new StatusService();
            this.UserService = new UserService();
            this.RoleService = new RoleService();
            this.AutocompleteValueService = new AutocompleteValueService();
            this.TipoPolizaService = new TipoPolizaService();
        }

        /// <summary>
        /// Gets Propiedad para acceder a la instancia
        /// </summary>
        public static ServiceFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (null == instance)
                        {
                            instance = new ServiceFacade();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the autocomplete value service.
        /// </summary>
        public AutocompleteValueService AutocompleteValueService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaContratoService"/> Gets GarantiaContratoService
        /// </summary>
        public GarantiaContratoService GarantiaContratoService { get; private set; }

        /// <summary>
        ///   <see cref="ClienteService"/> Gets ClienteService
        /// </summary>
        public ClienteService ClienteService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaService"/> Gets GarantiaService
        /// </summary>
        public GarantiaService GarantiaService { get; private set; }

        /// <summary>
        ///   <see cref="ActorService"/> Gets ActorService
        /// </summary>
        public ActorService ActorService { get; private set; }

        /// <summary>
        ///   <see cref="AvalService"/> Gets AvalService
        /// </summary>
        public AvalService AvalService { get; private set; }

        /// <summary>
        ///   <see cref="PaisService"/> Gets PaisService
        /// </summary>
        public PaisService PaisService { get; private set; }

        /// <summary>
        ///   <see cref="RegionService"/> Gets RegionService
        /// </summary>
        public RegionService RegionService { get; private set; }

        /// <summary>
        ///   <see cref="TipoPolizaService"/> Gets TipoPolizaService
        /// </summary>
        public TipoPolizaService TipoPolizaService { get; private set; }

        /// <summary>
        ///   <see cref="AseguradorasService"/> Gets AseguradorasService
        /// </summary>
        public AseguradorasService AseguradorasService { get; private set; }

        /// <summary>
        ///   <see cref="AvaluadoraService"/> Gets AvaluadoraService
        /// </summary>
        public AvaluadoraService AvaluadoraService { get; private set; }

        /// <summary>
        ///   <see cref="BancosService"/> Gets BancosService
        /// </summary>
        public BancosService BancosService { get; private set; }

        /// <summary>
        ///   <see cref="CalificacionesRiesgoService"/> Gets CalificacionesRiesgoService
        /// </summary>
        public CalificacionesRiesgoService CalificacionesRiesgoService { get; private set; }

        /// <summary>
        ///   <see cref="CategoriaSuperService"/> Gets CategoriaSuperService
        /// </summary>
        public CategoriaSuperService CategoriaSuperService { get; private set; }

        /// <summary>
        ///   <see cref="FiduciariasService"/> Gets FiduciariasService
        /// </summary>
        public FiduciariasService FiduciariasService { get; private set; }

        /// <summary>
        ///   <see cref="FrecuenciasService"/> Gets FrecuenciasService
        /// </summary>
        public FrecuenciasService FrecuenciasService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaDepositoOtroBancoService"/> Gets GarantiaDepositoOtroBancoService
        /// </summary>
        public GarantiaDepositoOtroBancoService GarantiaDepositoOtroBancoService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaDepositoService"/> Gets GarantiaDepositoService
        /// </summary>
        public GarantiaDepositoService GarantiaDepositoService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaInmuebleService"/> Gets GarantiaInmuebleService
        /// </summary>
        public GarantiaInmuebleService GarantiaInmuebleService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaMuebleService"/> Gets GarantiaMuebleService
        /// </summary>
        public GarantiaMuebleService GarantiaMuebleService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaOtraService"/> Gets GarantiaOtraService
        /// </summary>
        public GarantiaOtraService GarantiaOtraService { get; private set; }

        /// <summary>
        ///   <see cref="GarantiaPrendaService"/> Gets GarantiaPrendaService
        /// </summary>
        public GarantiaPrendaService GarantiaPrendaService { get; private set; }           

        /// <summary>
        ///   <see cref="GrupoRiesgoGarantiaService"/> Gets GrupoRiesgoGarantiaService
        /// </summary>
        public GrupoRiesgoGarantiaService GrupoRiesgoGarantiaService { get; private set; }

        /// <summary>
        ///   <see cref="InstrumentoFinancieroService"/> Gets InstrumentoFinancieroService
        /// </summary>
        public InstrumentoFinancieroService InstrumentoFinancieroService { get; private set; }

        /// <summary>
        ///   <see cref="MonedasService"/> Gets MonedasService
        /// </summary>
        public MonedasService MonedasService { get; private set; }

        /// <summary>
        ///   <see cref="StatusService"/> Gets StatusService
        /// </summary>
        public InternalStatusService InternalStatusService { get; private set; }

        /// <summary>
        ///   <see cref="StatusService"/> Gets StatusService
        /// </summary>
        public StatusService StatusService { get; private set; }

        /// <summary>
        ///   <see cref="TipoAvalService"/> Gets TipoAvalService
        /// </summary>
        public TipoAvalService TipoAvalService { get; private set; }

        /// <summary>
        ///   <see cref="TipoGarantiaBladexService"/> Gets TipoGarantiaBladexService
        /// </summary>
        public TipoGarantiaBladexService TipoGarantiaBladexService { get; private set; }

        /// <summary>
        ///   <see cref="TipoGarantiaSuperService"/> Gets TipoGarantiaSuperService
        /// </summary>
        public TipoGarantiaSuperService TipoGarantiaSuperService { get; private set; }

        /// <summary>
        ///   <see cref="CategoriaRiesgoGarantiaService"/> Gets CategoriaRiesgoGarantiaService
        /// </summary>
        public CategoriaRiesgoGarantiaService CategoriaRiesgoGarantiaService { get; private set; }

        /// <summary>
        ///   <see cref="IMPORT_TH_ATOMO_GARANTIASService"/> Gets IMPORT_TH_ATOMO_GARANTIASService
        /// </summary>
        public IMPORT_TH_ATOMO_GARANTIASService IMPORT_TH_ATOMO_GARANTIASService { get; private set; }

        /// <summary>
        ///   <see cref="SyncLogService"/> Gets SyncLogService
        /// </summary>
        public SyncLogService SyncLogService { get; private set; }

        /// <summary>
        ///   <see cref="LimitInformationService"/> Gets LimitInformationService
        /// </summary>
        public LimitInformationService LimitInformationService { get; private set; }

        /// <summary>
        ///   <see cref="TooltipService"/> Gets TooltipService
        /// </summary>
        public TooltipService TooltipService { get; private set; }

        /// <summary>
        /// Gets the maker checker service.
        /// </summary>
        public MakerCheckerService MakerCheckerService { get; private set; }

        /// <summary>
        /// Gets the user service.
        /// </summary>
        public UserService UserService { get; private set; }

        /// <summary>
        /// Gets the role service.
        /// </summary>
        public RoleService RoleService { get; private set; }
    }
}