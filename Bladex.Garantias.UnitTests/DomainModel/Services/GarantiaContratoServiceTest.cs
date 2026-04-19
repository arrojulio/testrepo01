using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.DomainModel.Services
{
    [TestClass]
    public class GarantiaContratoServiceTest
    {
        public static GarantiaOtraService gOtraSvc = new GarantiaOtraService();
        public static GarantiaContratoService gSvc = new GarantiaContratoService();
        public static GarantiaOtra gOtra = null;
        public static GarantiaContrato g = null;

        [TestInitialize()]
        public void Setup()
        {
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
            //Bladex.Garantias.Presentation.Website.Bootstrapper.Setup();
            //g = gSvc.GetAll().First();

            //Assert.IsNotNull(g, "GarantiaContrato de Prueba es Null");
            //gOtra = gOtraSvc.GetAll().First();
            //Assert.IsNotNull(gOtra, "GarantiaOtra de Prueba es Null");
        }

        [TestMethod]
        public void GetByGarantiaIdTest()
        {
            //int expected = gSvc.GetAll().Where(o => o.GarantiaId.HasValue && o.GarantiaId.Value == gOtra.GetKeyAs<int>()).Count();
            //int actual;
            //List<GarantiaContrato> contratos = gSvc.GetByGarantiaId(gOtra.GetKeyAs<int>()).ToList();
            //actual = contratos.Count;
            //Assert.AreEqual(expected, actual, string.Format("GetByGarantiaIdTest no retorna todos los contratos de la garantia {0}", gOtra.Key));
        }

        [TestMethod]
        public void ShouldRetrieveDealReferencesForAllCustomers()
        {
            var clientes = ServiceFacade.Instance.ClienteService.GetAllClientes();
            foreach (var c in clientes)
            {
                string customerId = c.GetKeyAs<string>();
                var deals = gSvc.GetDealReferencesByCustomer(customerId);
                Assert.IsNotNull(deals);
                if (deals.Count == 0)
                {
                    Console.WriteLine("No existen contratos para el cliente {0}.", customerId);
                }
                else
                {
                    Console.WriteLine("Contratos para el cliente {0}:", customerId);
                }
                foreach (var d in deals)
                {
                    Console.WriteLine(d.ToString());
                }
                
            }
        }

        [TestMethod]
        public void ShouldRetrieveDealReferencesByCustomer()
        {
            var customerId = "TENEMXMX0";
            var cliente = ServiceFacade.Instance.ClienteService.GetById(customerId);
            var deals = gSvc.GetDealReferencesByCustomer(cliente.GetKeyAs<string>());
            Assert.IsNotNull(deals);
            if (deals.Count == 0)
            {
                Console.WriteLine("No existen contratos para el cliente {0}.", string.Concat(customerId, " - ", cliente.Nombre));
            }
            else
            {
                Console.WriteLine("Contratos para el cliente {0}:", string.Concat(customerId, " - ", cliente.Nombre));
            }
            foreach (var d in deals)
            {
                Console.WriteLine(d.ToString());
            }
        }

        [TestMethod]
        public void ShouldAddGarantiaContratoToMakerCheckerObject()
        {
            var operationId = 66;
            GarantiaContrato entity = new GarantiaContrato();
            entity.DealReference = "Test";
            entity.FechaRegistroInicial = DateTime.Now.AddMonths(1);
            entity.FechaVencimientoGarantia = DateTime.Now.AddMonths(2);
            entity.FechaVencimientoRiesgo = DateTime.Now.AddMonths(3);
            entity.GarantiaId = operationId;
            entity.NetBalancePrincipal = 10000;
            entity.PorcUtilization = 100;
            var result = ServiceFacade.Instance.GarantiaContratoService.Save(entity, "npascual");
            Assert.IsNotNull(result);
            Console.WriteLine("The generated ID is {0}", result.ID);
        }

        [TestMethod]
        public void ShouldRetrieveGarantiaContratoListFromMakerCheckerObject()
        {
            var operationId = 67;
            var operation = ServiceFacade.Instance.MakerCheckerService.GetOperation(operationId);
            MakerCheckerObject<GarantiaBase> mcObj = AutoMapper.Mapper.Map(operation.GetMakerCheckerObject(), operation.GetMakerCheckerObject().GetType(), typeof(MakerCheckerObject<GarantiaBase>));
            var addAttr = mcObj.AdditionalAttributes;
            if (addAttr.ContainsKey(GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey))
            {
                dynamic objColl = addAttr[GarantiaContratoService.MakerCheckerObjectGarantiaContratoKey];

                List<GarantiaContrato> listColl = objColl.ToObject<List<GarantiaContrato>>();
                Assert.IsNotNull(listColl);
            }

        }
    }
}
