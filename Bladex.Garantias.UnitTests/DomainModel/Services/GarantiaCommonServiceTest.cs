using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.DomainModel.Services
{
    [TestClass]
    public class GarantiaCommonServiceTest
    {
        [TestInitialize]
        public void WarmUp()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
        }

        [TestMethod]
        public void ChangeTypeTest()
        {
            List<GarantiaBase> garantias = ServiceFacade.Instance.GarantiaService.GetAllGarantias().ToList();
            foreach (GarantiaBase g in garantias)
            {
                // Obtengo una garantia de prueba

                //GarantiaBase gPrenda = ServiceFacade.Instance.GarantiaService.GetAllGarantias().FirstOrDefault();
                GarantiaBase gPrenda = g;
                // Guardo su Key.
                int key = gPrenda.GetKeyAs<int>();

                System.Console.WriteLine("Modificando Garantia con ID: " + key);

                // Obtengo el tipo actual
                CategoriaSuper currentType = gPrenda.CategoriaSuper;
                System.Console.WriteLine("Tipo Original: " + currentType.Nombre);
                // Obtengo el listado de tipos a modificar.
                List<CategoriaSuper> newTypes = ServiceFacade.Instance.CategoriaSuperService.GetAll().ToList();
                // Quito el tipo actual.
                newTypes.Remove(gPrenda.CategoriaSuper);
                foreach (var newType in newTypes)
                {
                    // Cambio el tipo a la garantia.
                    bool result = false;
                    System.Console.WriteLine("Cambiando de {0} a {1}: ", currentType.Nombre, newType.Nombre);
                    switch (currentType.Key.ToString())
                    {
                        case "01":
                            result = ServiceFacade.Instance.GarantiaMuebleService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                        case "02":
                            result = ServiceFacade.Instance.GarantiaInmuebleService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                        case "03":
                            result = ServiceFacade.Instance.GarantiaDepositoService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                        case "04":
                            result = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                        case "05":
                            result = ServiceFacade.Instance.GarantiaPrendaService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                        case "06":
                            result = ServiceFacade.Instance.GarantiaOtraService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                            break;
                    }
                    System.Console.WriteLine("Resultado del cambio de tipo: {0}", result);

                    //bool result = ServiceFacade.Instance.GarantiaPrendaService.ChangeType(gPrenda.GetKeyAs<int>(), currentType.Key.ToString(), newType.Key.ToString(), "npascual");
                    // Verifico operacion exitosa.
                    Assert.IsTrue(result);
                    // Obtengo el nuevo tipo.
                    CategoriaSuper newChangedType = ServiceFacade.Instance.CategoriaSuperService.GetByGarantiaId(key);

                    // Comparo que el nuevo tipo sea igual al tipo esperado.
                    Assert.AreEqual(newType, newChangedType);

                    currentType = newChangedType;
                    switch (newChangedType.Key.ToString())
                    {
                        case "01":
                            gPrenda = ServiceFacade.Instance.GarantiaMuebleService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                        case "02":
                            gPrenda = ServiceFacade.Instance.GarantiaInmuebleService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                        case "03":
                            gPrenda = ServiceFacade.Instance.GarantiaDepositoService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                        case "04":
                            gPrenda = ServiceFacade.Instance.GarantiaDepositoOtroBancoService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                        case "05":
                            gPrenda = ServiceFacade.Instance.GarantiaPrendaService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                        case "06":
                            gPrenda = ServiceFacade.Instance.GarantiaOtraService.GetById(gPrenda.GetKeyAs<int>());
                            break;
                    }
                }
                /*
                ID	Nombre
                01	Garantía Hipotecaria Mueble
                02	Garantía Hipotecaria Inmueble
                03	Depósitos Pignorados en el Banco
                04	Depósitos Pignorados en Otros Bancos
                05	Garantía Prendaria
                06	Otras Garantías
                */
            }
        }
    }
}
