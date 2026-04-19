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
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc;
using System.Collections;


namespace Bladex.Garantias.Presentation.Website.Controllers
{
    /// <summary>
    /// The garantia controller class.
    /// </summary>
    [HandleErrorWithElmah]
    public class GarantiaController : BaseController
    {
        /// <summary>
        ///   <see cref="Bladex.Garantias.Presentation.Website.ViewModels.GarantiaViewModel"/>
        /// </summary>
        public static GarantiaViewModel GarantiaModel;// = SetupController();

        /// <summary>
        /// Initializes a new instance of the <see cref="GarantiaController"/> class.
        /// </summary>
        public GarantiaController()
        {
        }

        //
        // GET: /Garantia/
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CategoriaSuper> list = ServiceFacade.Instance.CategoriaSuperService.GetAll().ToList();
            GarantiaIndexViewModel givm = new GarantiaIndexViewModel {CategoriaSuperList = list};

            return View(givm);
        }

        /// <summary>
        /// Lists the deleted guarantees.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListDeleted()
        {
            List<GarantiaBase> list = ServiceFacade.Instance.GarantiaService.GetAllGarantiasDeleted().ToList();
            return View(list);
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult List(string SearchText)
        {                                       
            List<GarantiaBaseRow> listado = new List<GarantiaBaseRow>();
            listado.AddRange(ServiceFacade.Instance.GarantiaService.SearchGarantias(string.IsNullOrEmpty(SearchText) ? string.Empty : SearchText));
            return View(listado);
        }

        public ActionResult ListUnknown(string SearchText)
        {
            List<GarantiaBaseRow> listado = new List<GarantiaBaseRow>();
            listado.AddRange(ServiceFacade.Instance.GarantiaService.GetAllGarantiasUnknown(string.IsNullOrEmpty(SearchText) ? string.Empty : SearchText));
            return View(listado);
        }

        /// <summary>
        /// Disassociate guarantees.
        /// </summary>
        /// <returns></returns>
        public ActionResult DisassociateGuarantees()
        {
            List<SelectListItem> listOfCategoriaSuper = new List<SelectListItem>() 
            { new SelectListItem { Selected = true, Text = "", Value = "" } };

            listOfCategoriaSuper.AddRange(ServiceFacade.Instance.CategoriaSuperService.GetAll().OrderBy(o => o.Nombre)
                .Select(entity => new SelectListItem { Text = entity.Nombre, Value = entity.Key.ToString() }).ToList());

            CategoriaSuperViewModel categoriaSuper = new CategoriaSuperViewModel() { List = listOfCategoriaSuper };
            return View(categoriaSuper);
        }

        /// <summary>
        /// Get all guarantee types by guarantee id.
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTipoGarantiaSuperById(string id)
        {
            List<SelectListItem> listOfTipoCategoriaSuper = new List<SelectListItem>() { new SelectListItem { Selected = true, Text = "", Value = "" } };

            if (id.Length == 2)
            {
                listOfTipoCategoriaSuper.AddRange(ServiceFacade.Instance.TipoGarantiaSuperService.GetAll(id.ToString()).OrderBy(o => o.Nombre).Where(c=>c.IsActive == true)
                    .Select(entity => new SelectListItem { Text = string.Format("{0} - {1}", entity.Key.ToString(), entity.Nombre), Value = entity.Key.ToString() }).ToList());
            }

            return Json(listOfTipoCategoriaSuper, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cambia el tipo de garantia a unknown por tipo id y desactiva el tipo de categoria.
        /// </summary>
        /// <returns></returns>
        public string DisableGuaranteeType(string id)
        {
            // 1 ok, -1 error, 0 unauthorized
            string ret = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                ret = "0;Debe seleccionar un tipo de categoría";
            }
            else
            {
                string res = ServiceFacade.Instance.GarantiaService.DisableGuaranteeType(UserId, id);
                switch (res)
                {
                    case "1": ret = "1;La desvinculación se ha realizado satisfactoriamente";
                        break;
                    case "-1": ret = "0;La desvinculación no pudo ser realizada debido a un error interno";
                        break;
                    case "0": ret = "0;Usuario no autorizado para realizar esta acción";
                        break;
                    case "2": ret = "0;Se encontraror changesets pendientes de aprobación";
                        break;
                    default: ret = "0;Resultado de la operación desconocido";
                        break;
                }
            }

            return ret;
        }

        private bool IsInteger(string value)
        {
            int a;
            bool ret = true;
            if (!int.TryParse(value, out a))
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Creates the specified categoria super id.
        /// </summary>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(string categoriaSuperId)
        {
            GarantiaResolver resolver = new GarantiaResolver(categoriaSuperId);
            return RedirectToAction(resolver.CreateAction, resolver.Controller, new { categoriaSuperId });
        }


        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Edit(int? garantiaId, int? operationId, string categoriaSuperId, bool? useRepository, bool? isReadOnly)
        {
            GarantiaResolver resolver = new GarantiaResolver(categoriaSuperId);
            string categoriaSuperIdParam = categoriaSuperId;
            if (useRepository.HasValue && useRepository.Value==false)
                return RedirectToAction(resolver.UpdateAction, resolver.Controller, new { operationId = operationId, categoriaSuperId = categoriaSuperIdParam, useRepository = useRepository.Value, isReadOnly = isReadOnly });
            else                
                return RedirectToAction(resolver.UpdateAction, resolver.Controller, new { garantiaId = garantiaId, categoriaSuperId = categoriaSuperIdParam, useRepository = useRepository.Value, isReadOnly= isReadOnly });
            
        }


        //
        // GET: /Garantia/Delete/5

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id of type <see cref="System.Int32"/></param>
        /// <param name="categoriaSuperId">The categoria super id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public ActionResult Delete(int id, string categoriaSuperId)
        {
            GarantiaResolver resolver = new GarantiaResolver(categoriaSuperId);
            string categoriaSuperIdParam = categoriaSuperId;
            return RedirectToAction(resolver.DeleteAction, resolver.Controller, new { id, categoriaSuperId = categoriaSuperIdParam });
        }

        //
        // POST: /Garantia/Delete/5

    }
}
