using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Presentation.Website.Controllers;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Presentation_Layer
{
    [TestClass]
    public class GarantiaContratoControllerTest
    {
        private int garantiaId;
        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            garantiaId = 10184;
        }

        [TestMethod]
        public void ShouldRetrieveGarantiaContratoDealReference()
        {
            var controller = new GarantiaContratoController();
            controller.SetFakeControllerContext();
            var firstResult = controller.Create(garantiaId, null, CategoriaSuper.OTRAS_ID) as ViewResult;
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(firstResult.ViewData);
            Assert.IsNotNull(firstResult.ViewData.Model);
            GarantiaContratoViewModel viewModel = (GarantiaContratoViewModel)firstResult.ViewData.Model;
            Assert.IsNotNull(viewModel);
        }
       

        [TestMethod]
        public void ShouldBuildAGarantiaContratoDealReferenceTest()
        {
            var eDealReference = new GarantiaContratoDealReference() { Customer = new Cliente() { Key = "ENVAPELI0" }, DealReference = "001LADB112660003", FechaRegistroInicial = new DateTime(2011, 9, 23), FechaVencimientoGarantia = new DateTime(2012, 3, 21), NetBalance = 1000000, ProductGroup = "LDLOAN", Key = "001LADB112660003" };
            var dealReference = ServiceFacade.Instance.GarantiaContratoService.GetDealReference("001LADB112660003");
            Assert.AreEqual(eDealReference.Key, dealReference.Key);
            Assert.AreEqual(eDealReference.Customer.Key, dealReference.Customer.Key);
            Assert.AreEqual(eDealReference.DealReference, dealReference.DealReference);
            Assert.AreEqual(eDealReference.FechaRegistroInicial, dealReference.FechaRegistroInicial);
            Assert.AreEqual(eDealReference.FechaVencimientoGarantia, dealReference.FechaVencimientoGarantia);
            Assert.AreEqual(eDealReference.FechaVencimientoRiesgo, dealReference.FechaVencimientoRiesgo);
            Assert.AreEqual(eDealReference.NetBalance, dealReference.NetBalance);
            Assert.AreEqual(eDealReference.ProductGroup, dealReference.ProductGroup);
        }

        [TestMethod]
        public void ShouldPrepareTheViewModelTest()
        {
            var garantia = ServiceFacade.Instance.GarantiaService.FindById(garantiaId);
            
            var dealReferences = ServiceFacade.Instance.GarantiaContratoService.GetDealReferencesByCustomer(garantia.Cliente.GetKeyAs<string>());
            GarantiaContratoViewModel vm = new GarantiaContratoViewModel();
            vm.CustomerId = garantia.Cliente.Key.ToString();
            vm.CustomerName = garantia.Cliente.Nombre;
            vm.GarantiaContratoModel = new GarantiaContratoModel() { GarantiaId = garantia.GetKeyAs<int>() };
            vm.DealReferenceList = AutoMapper.Mapper.Map<List<GarantiaContratoDealReference>, List<DealReferenceSelectionViewModel>>(dealReferences);
            Assert.IsNotNull(vm);
            Assert.AreEqual(garantiaId, garantia.GetKeyAs<int>());
            Assert.IsNotNull(vm.CustomerId);
            Assert.IsNotNull(vm.CustomerName);
            Assert.IsNotNull(vm.GarantiaContratoModel);
            Assert.IsNotNull(vm.DealReferenceList);
        }
    }
}
