using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Controllers;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bladex.Garantias.UnitTests.Presentation_Layer
{
    [TestClass]
    public class AvalControllerTest
    {
        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
        }

        [TestMethod]
        public void TestAvaltoAvalViewModelMapping()
        {
            var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(87);
            Assert.IsNotNull(operation);
            MakerCheckerObject<GarantiaOtra> mcObj = operation.GetMakerCheckerObject();
            Assert.IsNotNull(mcObj);
            List<Aval> avales = mcObj.Object.Avales;
            Assert.IsNotNull(avales);
            foreach (var a in avales)
            {
                Console.WriteLine("Aval: Id: {0} - GarantiaId: {1} - P. Cobertura: {2}", a.Key, a.GarantiaId, a.PorcentajeCobertura);
            }
            List<AvalViewModel> viewModelAvales = AutoMapper.Mapper.Map<List<Aval>, List<AvalViewModel>>(avales);
            Assert.IsNotNull(viewModelAvales);
            foreach (var a in viewModelAvales)
            {
                Console.WriteLine("Aval ViewModel: Id: {0} - GarantiaId: {1} - P. Cobertura: {2}", a.Key, a.GarantiaId, a.PorcentajeCobertura);
            }

        }

        [TestMethod]
        public void ShouldEditAvalViewModel()
        {
            var avalId = 32;
            var garantiaId = 10217;
            var operationId = 87;
            // GET Section
            var controller = new AvalController();
            controller.SetFakeControllerContext();
            var editGet = controller.Edit(avalId, garantiaId, operationId) as ViewResult;
            Assert.IsNotNull(editGet);
            Assert.IsNotNull(editGet.Model);
            var viewModel = editGet.Model as AvalViewModel;
            Assert.IsNotNull(viewModel);
            Console.WriteLine("Datos del Aval: GarantiaId: {0} - Aval Id: {1} - Porcentaje Cobertura: {2}", viewModel.GarantiaId, viewModel.Key, viewModel.PorcentajeCobertura);
            // POST Section
            viewModel.PorcentajeCobertura = viewModel.PorcentajeCobertura / 1.38;
            var editPost = controller.Edit(viewModel, garantiaId, operationId) as ViewResult;
            Assert.IsNotNull(editPost);
            Assert.IsNotNull(editPost.ViewData);
            var editSuccess = (bool)editPost.ViewData["EDIT_SUCESSFULL"];
            Assert.IsTrue(editSuccess);
        }
    }
}
