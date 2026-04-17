using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Services;
using Bladex.Garantias.Infrastructure.Logging;
using Bladex.Garantias.Presentation.Website.ViewModels;

namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The aval controller class.
    /// </summary>
    public class AvalController : Controller
    {

        /// <summary>
        /// Counts the specified garantia id.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <returns></returns>
        public int Count(int? garantiaId)
        {
            if (garantiaId.HasValue)
            {
                List<Aval> avalList = ServiceFacade.Instance.AvalService.GetByGarantiaId(garantiaId.Value).ToList();
                return avalList.Count;
            }
            return 0;
        }

        /// <summary>
        /// Indexes the specified garantia id.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        public ActionResult Index(int? garantiaId, int? operationId)
        {
            ViewBag.GarantiaId = garantiaId;
            ViewBag.OperationId = operationId;
            List<AvalViewModel> avalListViewModel = null;
            if (garantiaId.HasValue && operationId.HasValue)
            {
                var categoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetByGarantiaId(garantiaId.Value);
                if (categoriaSuper != null)
                    this.ViewBag.CategoriaSuperId = categoriaSuper.Key.ToString();

                List<Aval> avalList = ServiceFacade.Instance.AvalService.GetByGarantiaId(garantiaId.Value).ToList();
                var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId.Value);
                MakerCheckerObject<GarantiaOtra> objMakerChecker = operation.GetMakerCheckerObject();
                var mcAvales = new List<Aval>(objMakerChecker.Object.Avales);
                mcAvales.ForEach(o => o.GarantiaId = operationId.Value);
                avalList.AddRange(mcAvales);
                avalListViewModel = AutoMapper.Mapper.Map<List<Aval>, List<AvalViewModel>>(avalList);
                
            }
            else if (garantiaId.HasValue)
            {
                List<Aval> avalList = ServiceFacade.Instance.AvalService.GetByGarantiaId(garantiaId.Value).ToList();
                avalListViewModel = AutoMapper.Mapper.Map<List<Aval>, List<AvalViewModel>>(avalList);
                avalListViewModel.ForEach(o => o.GarantiaId = garantiaId);
                this.ViewBag.GarantiaId = garantiaId;
                var categoriaSuper = ServiceFacade.Instance.CategoriaSuperService.GetByGarantiaId(garantiaId.Value);
                if (categoriaSuper != null)
                    this.ViewBag.CategoriaSuperId = categoriaSuper.Key.ToString();
                return View(avalListViewModel);
            }
            else if (operationId.HasValue)
            {
                var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId.Value);
                MakerCheckerObject<GarantiaOtra> objMakerChecker = operation.GetMakerCheckerObject();
                List<Aval> avalList = new List<Aval>(objMakerChecker.Object.Avales);
                avalListViewModel = AutoMapper.Mapper.Map<List<Aval>, List<AvalViewModel>>(avalList);
                // we set the operation id instead of the garantia id.
                avalListViewModel.ForEach(o => o.GarantiaId = operationId.Value);
                this.ViewBag.CategoriaSuperId = Request.QueryString["categoriaSuperId"];
            }
            else
            {
                avalListViewModel = new List<AvalViewModel>();
            }
            return View(avalListViewModel);
        }

        //
        // GET: /Aval/Create

        /// <summary>
        /// Creates the specified garantia id.
        /// </summary>
        /// <param name="garantiaId">The garantia id of type <see cref="System.Int32"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        public ActionResult Create(int? garantiaId, int? operationId)
        {
            this.ViewData["CREATE_SUCESSFULL"] = false;
            this.ViewBag.GarantiaId = garantiaId;
            this.ViewBag.OperationId = operationId;
            Aval aval = AvalService.GetEmpty();
            AvalViewModel avalViewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(aval);
            avalViewModel.GarantiaId = garantiaId;
            return View(avalViewModel);
        } 

        //
        // POST: /Aval/Create

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(AvalViewModel viewModel, int? garantiaId, int? operationId)
        {
            ViewBag.OperationId = operationId;
            ViewBag.GarantiaId = garantiaId;
            try
            {
                if (ModelState.IsValid)
                {

                    Aval avalEntity = AutoMapper.Mapper.Map<AvalViewModel, Aval>(viewModel);
                    if (avalEntity.Pais != null)
                        avalEntity.Pais = ServiceFacade.Instance.PaisService.GetById(avalEntity.Pais.Key.ToString());
                    if (avalEntity.TipoAval != null)
                        avalEntity.TipoAval = ServiceFacade.Instance.TipoAvalService.GetById(avalEntity.TipoAval.Key.ToString());
                    if (operationId.HasValue)
                        avalEntity.GarantiaId = operationId.Value;
                    var result = ServiceFacade.Instance.AvalService.Save(avalEntity, User.Identity.Name);
                    viewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(result);
                    this.ViewData["CREATE_SUCESSFULL"] = true;
                    return View(viewModel);
                }
                else
                {
                    this.ViewData["CREATE_SUCESSFULL"] = false;
                    viewModel.TipoAval.List = new SelectList(ServiceFacade.Instance.TipoAvalService.GetAll().OrderBy(o=>o.Nombre), "Key", "Nombre");
                    viewModel.Pais.List = new SelectList(ServiceFacade.Instance.PaisService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                    viewModel.List = new SelectList(ServiceFacade.Instance.AvalService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");

                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Error(string.Format("Ha ocurrido un error creando un Aval para la garantia {0} con operation Id {1}", garantiaId, operationId), ex);
                this.ViewData["CREATE_SUCESSFULL"] = false;
                ModelState.AddModelError("", ex);
                viewModel.TipoAval.List = new SelectList(ServiceFacade.Instance.TipoAvalService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                viewModel.Pais.List = new SelectList(ServiceFacade.Instance.PaisService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                viewModel.List = new SelectList(ServiceFacade.Instance.AvalService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                return View(viewModel);
            }
        }
        
        //
        // GET: /Aval/Edit/5

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        public ActionResult Edit(int id, int? garantiaId, int? operationId)
        {
            this.ViewBag.GarantiaId = garantiaId;
            this.ViewBag.OperationId = operationId;
            this.ViewData["EDIT_SUCESSFULL"] = false;
            Aval aval = ServiceFacade.Instance.AvalService.GetById(id);
            AvalViewModel avalViewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(aval);
            return View(avalViewModel);
        }

        //
        // POST: /Aval/Edit/5

        /// <summary>
        /// Edits the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AvalViewModel viewModel, int? garantiaId, int? operationId)
        {
            ViewBag.GarantiaId = garantiaId;
            ViewBag.OperationId = operationId;
            try
            {
                if (ModelState.IsValid)
                {
                    Aval avalEntity = AutoMapper.Mapper.Map<AvalViewModel, Aval>(viewModel);
                    if (operationId.HasValue)
                        avalEntity.GarantiaId = operationId.Value;
                    else if (garantiaId.HasValue)
                        avalEntity.GarantiaId = garantiaId.Value;
                    if (operationId.HasValue)
                        avalEntity.GarantiaId = operationId.Value;
                    ServiceFacade.Instance.AvalService.Save(avalEntity, User.Identity.Name);
                    this.ViewData["EDIT_SUCESSFULL"] = true;
                    return View(viewModel);
                }
                else
                {
                    this.ViewData["EDIT_SUCESSFULL"] = false;
                    AvalViewModel newViewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(ServiceFacade.Instance.AvalService.GetById(viewModel.Key.Value));
                    return View(newViewModel);
                }
                
            }
            catch(Exception ex)
            {
                ApplicationLogger.Instance.Error(string.Format("Ha ocurrido un error actualizando un Aval con identificador {0}", viewModel.Key), ex);
                ModelState.AddModelError("", ex);
                this.ViewData["EDIT_SUCESSFULL"] = false;
                AvalViewModel newViewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(ServiceFacade.Instance.AvalService.GetById(viewModel.Key.Value));
                return View(newViewModel);
            }
        }

        //
        // GET: /Aval/Delete/5

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        public ActionResult Delete(int id, int? garantiaId, int? operationId)
        {
            ViewBag.GarantiaId = garantiaId;
            ViewBag.OperationId = operationId;
            this.ViewData["DELETE_SUCESSFULL"] = false;
            Aval aval = ServiceFacade.Instance.AvalService.GetById(id);
            AvalViewModel avalViewModel = AutoMapper.Mapper.Map<Aval, AvalViewModel>(aval);
            return View(avalViewModel);
        }

        //
        // POST: /Aval/Delete/5

        /// <summary>
        /// Deletes the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model of type <see cref="Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel"/></param>
        /// <param name="garantiaId">The garantia id of type <see cref="int"/></param>
        /// <param name="operationId">The operation id of type <see cref="int"/></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(AvalViewModel viewModel, int? garantiaId, int? operationId)
        {
            ViewBag.GarantiaId = garantiaId;
            ViewBag.OperationId = operationId;
            try
            {
                Aval avalEntity = AutoMapper.Mapper.Map<AvalViewModel, Aval>(viewModel);
                ServiceFacade.Instance.AvalService.Remove(avalEntity, User.Identity.Name);
                this.ViewData["DELETE_SUCESSFULL"] = true;
                return RedirectToAction("Index", "Aval", new { garantiaId = garantiaId, operationId = operationId });
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Error(string.Format("Ha ocurrido un error eliminando un Aval con identificador {0}", viewModel.Key), ex);
                viewModel.TipoAval.List = new SelectList(ServiceFacade.Instance.TipoAvalService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                viewModel.Pais.List = new SelectList(ServiceFacade.Instance.PaisService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                viewModel.List = new SelectList(ServiceFacade.Instance.AvalService.GetAll().OrderBy(o => o.Nombre), "Key", "Nombre");
                this.ViewData["DELETE_SUCESSFULL"] = false;
                return View(viewModel);
            }
        }

        
    }
}
