using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Repositories.GarantiaInmueble;
using Bladex.Garantias.Infrastructure.Repositories.GarantiaPrenda;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class GarantiaInmuebleSqlRepositoryTest
    {
        private static IGarantiaInmuebleRepository repository = RepositoryFactory.GetRepository<IGarantiaInmuebleRepository, GarantiaInmueble>();
        private static GarantiaInmueble g = null;
        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            g = repository.GetAll().First();
            Assert.IsNotNull(g, "La Garantia de Prueba es Null");
        }

        [TestMethod]
        public void FindByTest()
        {
            GarantiaInmueble gp = repository.FindBy((int)g.Key);
            var expected = g.TipoGarantiaSuper.Key.ToString();
            Assert.IsNotNull(gp);
            Assert.IsNotNull(gp.TipoGarantiaSuper);
            Assert.IsNotNull(gp.TipoGarantiaSuper.Key);
            var actual = gp.TipoGarantiaSuper.Key.ToString();
            
            Assert.AreEqual(expected, actual);
        }
    }
}
