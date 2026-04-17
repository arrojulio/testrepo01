using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class GarantiaContratoSqlRepositoryTest
    {
        private static IGarantiaContratoRepository repository = RepositoryFactory.GetRepository<IGarantiaContratoRepository, GarantiaContrato>();
        private static IGarantiaOtraRepository repositoryOtra = RepositoryFactory.GetRepository<IGarantiaOtraRepository, GarantiaOtra>();
        private static GarantiaOtra gOtra = null;
        private static GarantiaContrato g = null;

        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            g = repository.GetAll().First();
            
            Assert.IsNotNull(g, "GarantiaContrato de Prueba es Null");
            gOtra = repositoryOtra.GetAll().First();
            Assert.IsNotNull(gOtra, "GarantiaOtra de Prueba es Null");
        }

        [TestMethod]
        public void GetByGarantiaIdTest()
        {
            int expected = repository.GetAll().Where(o => o.GarantiaId.HasValue && o.GarantiaId.Value == gOtra.GetKeyAs<int>()).Count();
            int actual;
            List<GarantiaContrato> contratos = repository.GetByGarantiaId(gOtra.GetKeyAs<int>()).ToList();
            actual = contratos.Count;
            Assert.AreEqual(expected, actual, string.Format("GetByGarantiaIdTest no retorna todos los contratos de la garantia {0}", gOtra.Key));
        }
    }

    
}
