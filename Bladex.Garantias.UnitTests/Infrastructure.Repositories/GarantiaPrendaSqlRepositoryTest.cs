using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Repositories.GarantiaPrenda;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class GarantiaPrendaSqlRepositoryTest
    {
        private static int GarantiaId;
        private static GarantiaPrenda g = null;

        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            GarantiaPrendaSqlRepository repository = new GarantiaPrendaSqlRepository();
            g = repository.GetAll().First();
            Assert.IsNotNull(g, "La Garantia de Pruebas es Null");
        }

        [TestMethod]
        public void SaveTest()
        {
            GarantiaPrendaSqlRepository repository = new GarantiaPrendaSqlRepository();
            GarantiaPrenda gp = ServiceFacade.Instance.GarantiaPrendaService.GetById((int)g.Key);
            
            gp.DescripcionDeLaGarantia = "Modified by UnitTest";
            GarantiaSourceEnum? sourceCurrent = gp.Source;
            gp.Source = GarantiaSourceEnum.Interna;
            GarantiaSourceEnum? sourceNew = gp.Source;
            IndicadorAtomoEnum? indAtomoCurrent = gp.IndAtomo;
            gp.IndAtomo = IndicadorAtomoEnum.Pignorados;
            IndicadorAtomoEnum? indAtomoNew = gp.IndAtomo;
            var expected = gp.DescripcionDeLaGarantia;
            var result = repository.Add(gp);
            var actual = result.DescripcionDeLaGarantia;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(sourceNew, result.Source);
            Assert.AreEqual(indAtomoNew, result.IndAtomo);
        }

        [TestMethod]
        public void FindByTest()
        {
            GarantiaPrendaSqlRepository repository = new GarantiaPrendaSqlRepository();
            GarantiaPrenda gp = repository.FindBy((int)g.Key);//ServiceFacade.Instance.GarantiaInmuebleService.GetById(1939);
            var expected = g.TipoGarantiaSuper.Key.ToString();
            Assert.IsNotNull(gp);
            Assert.IsNotNull(gp.TipoGarantiaSuper);
            Assert.IsNotNull(gp.TipoGarantiaSuper.Key);
            var actual = gp.TipoGarantiaSuper.Key.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
