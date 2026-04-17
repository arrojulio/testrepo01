using System;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure;
using Bladex.Garantias.Infrastructure.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bladex.Garantias.DomainModel.DomainBase;
using System.Collections.Generic;

namespace Bladex.Garantias.UnitTests.DomainModel.Repositories
{
    
    
    /// <summary>
    ///This is a test class for IClienteRepositoryTest and is intended
    ///to contain all IClienteRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IClienteRepositoryTest
    {

        private UnitOfWork unitOfWork;
        private IClienteRepository repository;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            this.unitOfWork = new UnitOfWork();
            this.repository = RepositoryFactory.GetRepository<IClienteRepository,Cliente>(this.unitOfWork);
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
        }

        #endregion


        internal virtual IClienteRepository CreateIClienteRepository()
        {
            // TODO: Instantiate an appropriate concrete class.
            return this.repository;
        }

        /// <summary>
        ///A test for FindByCodigoPais
        ///</summary>
        [TestMethod()]
        public void SearchByCodigoPaisTest()
        {
            IClienteRepository target = CreateIClienteRepository();
            string CodigoPais = "UK";
            IList<Cliente> expected = new List<Cliente>();
            
            expected.Add(new Cliente() { Rating = "5", Key = "BERTSP1", Nombre = "Cliente de Prueba 2", GrupoEconomico = "ECONOMICGROUPSAMPLE", Pais = new Pais() { Key = "AR", Nombre = "Argentina" } });
            IList<Cliente> actual;
            actual = target.FindByCodigoPais(CodigoPais);
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.IsTrue(actual[0] == expected[0]);
            Assert.IsTrue(expected[0].Equals(actual[0]));
            if (expected.Count > 0) Assert.AreEqual(expected[0].Key, actual[0].Key);
            if (expected.Count > 0) Assert.AreEqual(expected[0].Pais.Key, actual[0].Pais.Key);
            
        }

        [TestMethod]
        public void AddTest()
        {
            IClienteRepository target = CreateIClienteRepository();
            string CustomerID = Guid.NewGuid().ToString().Substring(0, 9);
            
            Cliente expected = new Cliente() { Rating = "7", Key = CustomerID, Nombre = "Cliente de Prueba " + CustomerID, GrupoEconomico = "ECONOMICGROUPSAMPLE2", Pais = new Pais() { Key = "AR", Nombre = "Argentina" } };
            target.Add(expected);
            this.unitOfWork.Commit();

            Cliente actual = target.FindBy(CustomerID);
            //Assert.IsTrue(actual == expected);
            //Assert.IsTrue(expected.Equals(actual));
            Assert.AreEqual(expected.Key, actual.Key);
            Assert.AreEqual(expected.Nombre, actual.Nombre);
        }

        [TestMethod]
        public void CountTest()
        {
            IClienteRepository target = CreateIClienteRepository();
            long expected = target.Count();
            expected++;
            this.AddTest();
            long actual = target.Count();

            Assert.AreEqual<long>(expected, actual);
        }

        /// <summary>
        ///A test for FindByGrupoEconomico
        ///</summary>
        [TestMethod()]
        public void FindByGrupoEconomicoTest()
        {
            IClienteRepository target = CreateIClienteRepository();
            string GrupoEconomico = "ECONOMICGROUPSAMPLETEST";
            IList<Cliente> expected = new List<Cliente>();
            expected.Add(new Cliente() { Rating = "5", Key = "BERTSP1", Nombre = "Cliente de Prueba 2", GrupoEconomico = GrupoEconomico, Pais = new Pais() { Key = "UK", Nombre = "United Kingdom" } });
            IList<Cliente> actual;
            actual = target.FindByGrupoEconomico(GrupoEconomico);
            Assert.AreEqual(expected.Count, actual.Count);
            if (expected.Count > 0) Assert.AreEqual(expected[0].Key, actual[0].Key);
            if (expected.Count > 0) Assert.AreEqual(expected[0].GrupoEconomico, actual[0].GrupoEconomico);
        }

        /// <summary>
        ///A test for FindByName
        ///</summary>
        [TestMethod()]
        public void FindByNameTest()
        {
            IClienteRepository target = CreateIClienteRepository();
            string Name = "Cliente de Prueba 2";
            IList<Cliente> expected = new List<Cliente>();
            expected.Add(new Cliente() { Rating = "5", Key = "BERTSP1", Nombre = "Cliente de Prueba 2", GrupoEconomico = "ECONOMICGROUPSAMPLE", Pais = new Pais() { Key = "AR", Nombre = "Argentina" } });
            IList<Cliente> actual;
            actual = target.FindByName(Name);
            Assert.AreEqual(expected.Count, actual.Count);
            if (expected.Count > 0) Assert.AreEqual(expected[0].Key, actual[0].Key);
            if (expected.Count > 0) Assert.AreEqual(expected[0].Nombre, actual[0].Nombre);
        }
    }
}
