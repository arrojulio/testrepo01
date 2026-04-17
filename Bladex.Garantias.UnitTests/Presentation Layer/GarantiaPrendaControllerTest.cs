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
using Bladex.Garantias.Presentation.Website.Controllers;
using Bladex.Garantias.Presentation.Website.Models;
using Bladex.Garantias.Presentation.Website.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bladex.Garantias.UnitTests.Presentation_Layer
{
    [TestClass]
    public class GarantiaPrendaControllerTest
    {
        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
        }

        [TestMethod]
        public void Save()
        {
            var garantiaId = 1413;
            var categoriaSuperId = "05";
            var controller = new GarantiaPrendaController();

            controller.SetFakeControllerContext();
            var firstResult = controller.Edit(null, garantiaId, categoriaSuperId, true, false) as ViewResult;
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(firstResult.ViewData);
            Assert.IsNotNull(firstResult.ViewData.Model);

            GarantiaPrendaViewModel viewModel = (GarantiaPrendaViewModel) firstResult.ViewData.Model;
            viewModel.Garantia.DescripcionDeLaGarantia = "Unit Test By Novaris at " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            var result = controller.Edit(viewModel) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
            //Assert.AreEqual(result.RouteValues["controller"], "Garantia");
        }

        
        

        
    }


    


}
