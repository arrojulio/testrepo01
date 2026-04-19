using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Components.Attributes;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.DomainModel.DomainBase.Summary;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia otra controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaOtraController : GarantiaBaseController
    {
        protected override void PopulateModelWithLists(GarantiaBaseModel garantia)
        {
            base.PopulateModelWithLists(garantia);
            if (garantia is GarantiaOtraModel)
            {
                if (((GarantiaOtraModel)garantia).Emisor == null) ((GarantiaOtraModel)garantia).Emisor = new ActorViewModel();
                //if(((GarantiaOtraModel)garantia).Emisor.List == null)
                ((GarantiaOtraModel)garantia).Emisor.List = ServiceFacade.Instance.ActorService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                if (((GarantiaOtraModel)garantia).Emisor.Pais == null)
                    ((GarantiaOtraModel)garantia).Emisor.Pais = new PaisViewModel();
                if (((GarantiaOtraModel)garantia).Emisor.Pais.List == null)
                    ((GarantiaOtraModel)garantia).Emisor.Pais.List = ServiceFacade.Instance.PaisService.GetAll().OrderBy(o => o.Nombre).Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();

                if (((GarantiaOtraModel)garantia).AvalComponent == null) ((GarantiaOtraModel)garantia).AvalComponent = new AvalManagerViewModel();                
                ((GarantiaOtraModel)garantia).AvalComponent.PaisCatalog.List = ServiceFacade.Instance.PaisService.GetAll().Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                ((GarantiaOtraModel)garantia).AvalComponent.TipoAvalCatalog.List = ServiceFacade.Instance.TipoAvalService.GetAll().Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList();
                
                
             


                
            }
        }

        /// <summary>
        /// Indexes the specified categoria super id.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Index(string categoriaSuperId)
        {
            ViewData["CategoriaSuper"] = ServiceFacade.Instance.CategoriaSuperService.GetById(categoriaSuperId);
            //TODO :Revisar esto por que usaba GetAllAndNotFianzasAvalesNoBancarios en vez del getAll
            //List<GarantiaOtra> list = ServiceFacade.Instance.GarantiaOtraService.GetAllAndNotFianzasAvalesNoBancarios().ToList();
            //#Ticket1320 : Optimizacion
            //List<GarantiaOtra> list = ServiceFacade.Instance.GarantiaOtraService.GetAll(null).Where(o => o.InternalStatus.ToString() != InternalStatus.EXPIRED_ID).ToList();

            List<GarantiaOtraSummary> list = ServiceFacade.Instance.GarantiaOtraService.GetAllGarantiasOtrasSQL();
            ViewData.Model = list;
            return View(list);
        }

        /// <summary>
        /// Indexes the avales.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult IndexAvales(string categoriaSuperId)
        {
            ViewData["CategoriaSuper"] = ServiceFacade.Instance.CategoriaSuperService.GetById(categoriaSuperId);
            List<GarantiaOtraSummary> list = ServiceFacade.Instance.GarantiaOtraService.GetAllAndNotFianzasAvalesNoBancarios().ToList();
            ViewData.Model = list;
            return View("Index", list);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            /*
            string values = "{[9,dddd,false,NA,Solidario,0.00 %,false];[6,ccc,false,NA,Solidario,0.00 %,false];[1,bbbb,false,NA,Solidario,0.00 %,true];[2,aaaa,false,NA,Solidario,0.00 %,true];}";
            int? garantiaId=0;
            List<Aval> avales = testAvales(values,garantiaId);
            */
            GarantiaOtraViewModel viewModel = new GarantiaOtraViewModel();
            GarantiaOtra garantiaOtra = new GarantiaOtra();
            garantiaOtra.FiduciariaBladex = "NA";
            garantiaOtra.FiduciariaSuper.Key = "NA";
            garantiaOtra.IdentificacionFideicomiso = "NA";
            garantiaOtra.Asegurador.Key = "NA";
            garantiaOtra.NumeroPolizaSeguro = "NA";
            viewModel.Garantia = AutoMapper.Mapper.Map<GarantiaOtra, GarantiaOtraModel>(garantiaOtra);
            if (viewModel.Garantia.AvalComponent == null)
            {
                garantiaOtra.AvalComponent = new DomainModel.DomainBase.Components.AvalManager.AvalManager();
                garantiaOtra.AvalComponent.AvalList = new List<Aval>();
                viewModel.Garantia.AvalComponent = new AvalManagerViewModel();
                viewModel.Garantia.AvalComponent.AvalList = new List<AvalViewModel>();
                
            }
            /******1550*/
            viewModel.Garantia.AvalComponent.AvalList = viewModel.Garantia.AvalList;
            PopulateModelWithLists(viewModel.Garantia);
            ViewData.Model = viewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified garantia otra view model.
        /// </summary>
        /// <param name="garantiaOtraViewModel">The garantia otra view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaOtraViewModel"/></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        [ValidateInput(true)]        
        public ActionResult Create(GarantiaOtraViewModel garantiaOtraViewModel)
        {                    
            Session[this.GetType().Name + "_Create"] = garantiaOtraViewModel;

            if (garantiaOtraViewModel.Garantia.AvalComponent != null && !string.IsNullOrEmpty(garantiaOtraViewModel.Garantia.AvalComponent.hiddenAvales))
            {
                List<AvalViewModel> avales = CreateAvalListFromHidden(garantiaOtraViewModel.Garantia.AvalComponent.hiddenAvales, garantiaOtraViewModel.Garantia.Key);

                
                if (avales != null)
                    garantiaOtraViewModel.Garantia.AvalList = avales;
                 
            }
            if (ModelState.IsValid)
            {               
                try
                {
                    GetDummyCustomers(garantiaOtraViewModel.Garantia);

                    //#1242 Validacion de catalogos
                    var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaOtraViewModel.Garantia as GarantiaOtraModel);
                    ServiceFacade.Instance.GarantiaOtraService.ValidateCatalogs(garantiaOtra);
                    garantiaOtraViewModel.Garantia = AutoMapper.Mapper.Map<GarantiaOtra, GarantiaOtraModel>(garantiaOtra);

                    int operationId = SaveGarantia(garantiaOtraViewModel.Garantia);
                    Session[this.GetType().Name + "_Create"] = null;
                    return RedirectToAction("Index", "GarantiaContrato", new { operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    //return RedirectToAction("Index", "Aval", new { operationId = operationId});
                    //return RedirectToAction("Index", "GarantiaOtra", new { categoriaSuperId = garantiaOtraViewModel.Garantia.CategoriaSuper.Key });
                    
                }
                catch (Exception ex)
                {
                    Logger.Error("Ha ocurrido un error creando una Garantia Otra", ex);
                    garantiaOtraViewModel = Session[this.GetType().Name + "_Create"] as GarantiaOtraViewModel;
                    this.PopulateModelWithLists(garantiaOtraViewModel.Garantia);
                    garantiaOtraViewModel.Garantia.BusinessError = ex.Message;
                    this.ViewData.Model = garantiaOtraViewModel;
                    this.SetErrorModel(ex);
                    Session[this.GetType().Name + "_Create"] = null;
                    return View(this.ViewData.Model);
                }
            }
            else
            {
                garantiaOtraViewModel = Session[this.GetType().Name + "_Create"] as GarantiaOtraViewModel;
                this.PopulateModelWithLists(garantiaOtraViewModel.Garantia);
                this.ViewData.Model = garantiaOtraViewModel;
                Session[this.GetType().Name + "_Create"] = null;
                return View(garantiaOtraViewModel);
            }
            
        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Edit(int? operationId, int? garantiaId, string categoriaSuperId, bool useRepository, bool? isReadOnly)
        {
            //retriveCategoriaSuper(categoriaSuperId);
            try
            {                

                GarantiaOtraViewModel viewModel = new GarantiaOtraViewModel();
                var garantiaOtra = ServiceFacade.Instance.GarantiaOtraService.GetById(operationId, garantiaId, useRepository, this.UserId);
                
                string strNroRef= string.Empty;
                int _grantiaid = 0;
                string strIdDocGarantia = string.Empty;

                if (garantiaOtra != null)
                {
                    if (garantiaOtra.Key != null)
                    {
                        _grantiaid = int.Parse(garantiaOtra.Key.ToString());

                        if (operationId.HasValue)
                        {
                            // Es edit de la garantia por operationid
                            strNroRef = garantiaOtra.NroReferencia;
                            strIdDocGarantia = garantiaOtra.GetIdentificacionDocumentoGarantia();
                        }
                        else
                        {
                            // Es edit de la garantia por id
                            strNroRef = ServiceFacade.Instance.GarantiaOtraService.GetNroReferencia(_grantiaid);
                            strIdDocGarantia = ServiceFacade.Instance.GarantiaOtraService.GetIdentificacionDocumentoGarantia(_grantiaid);
                        }
                    }
                    else {
                        strNroRef = garantiaOtra.NroReferencia;
                    }                   
                    
                }

                //Ticket #1550 : workaround  fecha vencimiento riesgo
                if (!garantiaOtra.FechaVencimientoRiesgo.HasValue && garantiaOtra.FechaVencimientoGarantia.HasValue)
                {
                    DateTime fixFechaVencimiento = garantiaOtra.FechaVencimientoGarantia.Value;
                    garantiaOtra.FechaVencimientoRiesgo = fixFechaVencimiento.AddDays(15);
                }

                if (garantiaOtra.Asegurador.Key == null)
                    garantiaOtra.Asegurador.Key = "NA";

                if (garantiaOtra.FiduciariaSuper.Key == null)
                    garantiaOtra.FiduciariaSuper.Key = "NA";

                var garantiaOtraModel = AutoMapper.Mapper.Map<GarantiaOtra, GarantiaOtraModel>(garantiaOtra);
                
                //Ticket #1550 : Custom mapping without lists
                List<AvalViewModel>  lstAval = new List<AvalViewModel>();
                foreach (Aval item in garantiaOtra.Avales) 
                {

                    lstAval.Add(new AvalViewModel
                    {
                        GarantiaId = item.GarantiaId,
                        Key = (int)item.Key,
                        Nombre = item.Nombre,
                        EsCliente = item.EsCliente,
                        Pais = new PaisViewModel { CodigoSuper = item.Pais.CodigoSuper, Key = item.Pais.Key.ToString(), Nombre = item.Pais.Nombre },
                        PorcentajeCobertura = item.PorcentajeCobertura,
                        TipoAval = new TipoAvalViewModel { Key = item.TipoAval.Key.ToString(), Nombre = item.TipoAval.Nombre }
                    });
                }

                garantiaOtraModel.AvalComponent = new AvalManagerViewModel();
                                
                garantiaOtraModel.AvalComponent.AvalList = lstAval;

                /* Fix para evitar que se produzca el error:
                 * Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.
                 * Elimina la sublista que no se utiliza de cada uno de los avales para que el retorno de la pagina sea mas liviano. */
                foreach (AvalViewModel aval in garantiaOtraModel.AvalList)
                {
                    aval.List = null;
                }

                if (isReadOnly.HasValue)
                    garantiaOtraModel.CategoriaSuper.IsReadOnly = isReadOnly.Value;

                if(operationId.HasValue)
                    garantiaOtraModel.selectedOperationId = operationId.Value;

                if (garantiaId.HasValue)
                {
                    if (ServiceFacade.Instance.GarantiaService.GetInternalStatus(garantiaId) == ServiceFacade.Instance.InternalStatusService.GetBlockedStatus())
                    {
                        garantiaOtraModel.CategoriaSuper.IsReadOnly = true;
                    }
                }


                viewModel.Garantia = garantiaOtraModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                this.ViewBag.UseRepository = useRepository;

                viewModel.Garantia.NroReferencia = strNroRef;
                viewModel.Garantia.IdentificacionDocumentoGarantia = strIdDocGarantia;

                if (operationId.HasValue)
                    this.ViewBag.OperationId = operationId.Value;
                else
                    this.ViewBag.OperationId = 0;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                
                var res = new GarantiaOtraViewModel() { Garantia = new GarantiaOtraModel() };
                PopulateModelWithLists(res.Garantia);
                this.ViewData.Model = res;
                this.SetErrorModel(ex);
                return View(this.ViewData.Model);
            }
        }

        /// <summary>
        /// Edits the specified garantia otra view model.
        /// </summary>
        /// <param name="garantiaOtraViewModel">The garantia otra view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaOtraViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(GarantiaOtraViewModel garantiaOtraViewModel, bool? useRepository)
        {
            Session[this.GetType().Name + "_Edit"] = garantiaOtraViewModel;
            this.ViewBag.UseRepository = useRepository.Value;

            if (garantiaOtraViewModel.Garantia.AvalComponent != null && !string.IsNullOrEmpty(garantiaOtraViewModel.Garantia.AvalComponent.hiddenAvales))
            {
                List<AvalViewModel> avales = CreateAvalListFromHidden(garantiaOtraViewModel.Garantia.AvalComponent.hiddenAvales, garantiaOtraViewModel.Garantia.Key);

                if (avales != null)                    
                    garantiaOtraViewModel.Garantia.AvalList = avales;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    GetDummyCustomers(garantiaOtraViewModel.Garantia);
                    
                    //#1242 Validacion de catalogos
                    var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaOtraViewModel.Garantia as GarantiaOtraModel);
                    ServiceFacade.Instance.GarantiaOtraService.ValidateCatalogs(garantiaOtra);
                    garantiaOtraViewModel.Garantia = AutoMapper.Mapper.Map<GarantiaOtra, GarantiaOtraModel>(garantiaOtra);

                    // Obtiene el InternalStatus original, si el internal status actual es Unknown.
                    if (garantiaOtraViewModel.Garantia.InternalStatus.Key == InternalStatus.UNKNOWN_ID)
                    {
                        garantiaOtraViewModel.Garantia.InternalStatus.Key = ServiceFacade.Instance.GarantiaService.GetOriginalInternalStatus(garantiaOtraViewModel.Garantia.Key.ToString());
                    }

                    int operationId = SaveGarantia(garantiaOtraViewModel.Garantia);
                    
                    //Ticket 1174: luego de generar el operation se cambia el useRepository a false para que trabaje con el json generado
                    return RedirectToAction("Index", "GarantiaContrato", new { garantiaId = garantiaOtraViewModel.Garantia.Key, operationId = operationId, categoriaSuperId = this.ViewData["categoriaSuperId"].ToString(), useRepository = false });
                    
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    garantiaOtraViewModel = Session[this.GetType().Name + "_Edit"] as GarantiaOtraViewModel;
                    garantiaOtraViewModel.Garantia.BusinessError = ex.Message;
                    PopulateModelWithLists(garantiaOtraViewModel.Garantia);

                    this.ViewData.Model = garantiaOtraViewModel;
                    this.SetErrorModel(ex);
                    Session[this.GetType().Name + "_Edit"] = null;

                    return View(this.ViewData.Model);
                }
            }
            else
            {
                garantiaOtraViewModel = Session[this.GetType().Name + "_Edit"] as GarantiaOtraViewModel;
                PopulateModelWithLists(garantiaOtraViewModel.Garantia);
                this.ViewData.Model = garantiaOtraViewModel;
                Session[this.GetType().Name + "_Edit"] = null;
                return View(garantiaOtraViewModel);
            }

        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Delete(int id, string categoriaSuperId)
        {
            //retriveCategoriaSuper(categoriaSuperId);
            try
            {
                var viewModel = new GarantiaOtraViewModel();
                var garantiaOtra = ServiceFacade.Instance.GarantiaOtraService.GetById(id);
                var garantiaOtraModel = AutoMapper.Mapper.Map<GarantiaOtra, GarantiaOtraModel>(garantiaOtra);
                viewModel.Garantia = garantiaOtraModel;
                PopulateModelWithLists(viewModel.Garantia);
                this.ViewData.Model = viewModel;
                this.ViewData["Key"] = id;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                return View();
            }
        }

        /// <summary>
        /// Deletes the specified garantia otra view model.
        /// </summary>
        /// <param name="garantiaOtraViewModel">The garantia otra view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaOtraViewModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Delete(GarantiaOtraViewModel garantiaOtraViewModel)
        {
            try
            {
                var garantiaOtra = AutoMapper.Mapper.Map<GarantiaOtraModel, GarantiaOtra>(garantiaOtraViewModel.Garantia);
                ServiceFacade.Instance.GarantiaOtraService.Delete(garantiaOtra, UserId);
                return RedirectToAction("Index", new { categoriaSuperId = this.ViewData["categoriaSuperId"].ToString() });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.SetErrorModel(ex);
                PopulateModelWithLists(garantiaOtraViewModel.Garantia);
                return View(garantiaOtraViewModel);
            }
        }

        public List<AvalViewModel> CreateAvalListFromHidden(string strAvales, int? garantiaId)
        {
            List<AvalViewModel> result = new List<AvalViewModel>();

            if (!string.IsNullOrEmpty(strAvales) && strAvales.Length > 2)
            {

                string Avales = strAvales.Substring(1, strAvales.Length - 2);
                string row;
                var arrAvales = Avales.Split(';');
                double fixCobertura = 0;
                bool fixEsCliente;

                PaisViewModel PaisVM;
                TipoAvalViewModel TipoAvalVM;
                Pais objPais;
                TipoAval objTipoAval;

                foreach (string item in arrAvales)
                {

                    objPais = new Pais();
                    objTipoAval = new TipoAval();
                    PaisVM = new PaisViewModel();
                    TipoAvalVM = new TipoAvalViewModel();

                    if (!string.IsNullOrEmpty(item))
                    {
                        row = "";
                        row = item.Substring(1, item.Length - 2);
                        var values = row.Split(new string[] { "','" }, StringSplitOptions.None);

                        //Pais
                        if (!string.IsNullOrEmpty(values[3]))
                        {
                            //objPais = ServiceFacade.Instance.PaisService.GetAll().Where(o => o.Nombre == values[3].ToString().Trim()).FirstOrDefault();

                            objPais = ServiceFacade.Instance.PaisService.GetById(values[3].ToString().Trim());

                            if (objPais != null)
                            {
                                PaisVM.CodigoSuper = objPais.CodigoSuper;
                                PaisVM.Key = objPais.Key.ToString();
                                PaisVM.Nombre = objPais.Nombre.Trim();
                            }
                            else
                            {
                                PaisVM.CodigoSuper = "N/A";
                                PaisVM.Key = "N/A";
                                PaisVM.Nombre = "N/A";
                            }
                        }

                        //Tipo Aval
                        if (!string.IsNullOrEmpty(values[4]))
                        {
                            //objTipoAval = ServiceFacade.Instance.TipoAvalService.GetAll().Where(o => o.Nombre == values[4].ToString().Trim()).FirstOrDefault();
                            objTipoAval = ServiceFacade.Instance.TipoAvalService.GetById(values[4].ToString().Trim());

                            if (objTipoAval != null)
                            {
                                TipoAvalVM.Key = objTipoAval.Key.ToString().Trim();
                                TipoAvalVM.Nombre = objTipoAval.Nombre.Trim();
                            }
                        }

                        // % Cobertura
                        Double.TryParse(values[5].Substring(0, values[5].Length - 1), out fixCobertura);
                        fixEsCliente = values[2].ToString().ToUpper().Trim()=="SI"?true:false;
                        result.Add(new AvalViewModel
                        {
                            //EsCliente = bool.Parse(values[2]),
                            EsCliente = fixEsCliente,
                            GarantiaId = garantiaId.HasValue ? garantiaId.Value : -1,
                            //IsDirty = bool.Parse(values[6]),
                            Key = int.Parse(values[0].ToString().Replace("'", "")),
                            //KeyAutoGenerated = true,
                            Nombre = values[1].Trim(),
                            Pais = PaisVM,
                            PorcentajeCobertura = fixCobertura,
                            TipoAval = TipoAvalVM
                        });
                    }
                }
            }

            return result;

        }
    }
}