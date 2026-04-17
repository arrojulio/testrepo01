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
    public class GarantiaBaseSqlRepositoryTest
    {
        private static IGarantiaBaseRepository<GarantiaBase> repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository<GarantiaBase>, GarantiaBase>();
        private static GarantiaBase g = null;
        [TestInitialize()]
        public void Setup()
        {
             
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            g = repository.GetAll().First();
            Console.WriteLine("El tipo de garantia es " + g.CategoriaSuper.Nombre);
            Assert.IsNotNull(g, "La Garantia de Prueba es Null");
        }

        [TestMethod]
        public void ChangeTypeTest()
        {
            /*
            ID	Nombre
            01	Garantía Hipotecaria Mueble
            02	Garantía Hipotecaria Inmueble
            03	Depósitos Pignorados en el Banco
            04	Depósitos Pignorados en Otros Bancos
            05	Garantía Prendaria
            06	Otras Garantías
            */
            bool result = repository.ChangeType(g, new CategoriaSuper() { Key = "04", Nombre = "Depósitos Pignorados en Otros Bancos", IsDirty = false });
            Assert.IsTrue(result);

        }
    }
}
