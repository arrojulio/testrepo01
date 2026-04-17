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
    public class MakerCheckerControllerTest
    {
        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
        }

        

        [TestMethod]
        public void CurrentTest()
        {
            // GET Section
            var controller = new MakerCheckerController();
            controller.SetFakeControllerContext("maker");
            var currentActionGet = controller.Current() as ViewResult;
            Assert.IsNotNull(currentActionGet);
            Assert.IsNotNull(currentActionGet.Model);
            var viewModel = currentActionGet.Model as MakerCheckerChangesetViewerViewModel;
            Assert.IsNotNull(viewModel);
            Assert.IsNotNull(viewModel.Changeset);
            Assert.IsNotNull(viewModel.Operations);
            Console.WriteLine("Datos del Changeset: Id: {0}", viewModel.Changeset.ChangesetId);
            foreach (var o in viewModel.Operations)
            {
                Console.WriteLine("Operation {0} with status {1}.", o.Operation.OperationId, o.Operation.OperationStatus.OperationStatusDescription);
                Console.WriteLine("Operation have the model with Key: {0}", o.Model.Key);
            }

        }
    }
}
