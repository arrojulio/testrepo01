using System.Diagnostics;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Bladex.Garantias.Infrastructure.DomainBase;
using System.Collections.Generic;

namespace Bladex.Garantias.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for EntityBaseXmlSerializerTest and is intended
    ///to contain all EntityBaseXmlSerializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityBaseXmlSerializerTest
    {
        protected IMakerCheckerUserRepository UserRepository;
        protected IMakerCheckerRoleRepository RoleRepository;
        protected IMakerCheckerOperationStatusRepository OperationStatusRepository;
        protected IMakerCheckerChangesetRepository ChangesetRepository;
        protected IMakerCheckerOperationRepository OperationRepository;
        protected IGarantiaMuebleRepository GarantiaBaseRepository;
        public const string _USER_ID_TO_GENERATE = "Unit-Test-User";

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
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        { 
            UserRepository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>();
            RoleRepository = RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>();
            OperationStatusRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>();
            ChangesetRepository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>();
            OperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            GarantiaBaseRepository = RepositoryFactory.GetRepository<IGarantiaMuebleRepository, GarantiaMueble>();
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        
        #endregion



        /// <summary>
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
        public void SerializeAndDeserializeTest()
        {
            // Serialization
            var garantia = ServiceFacade.Instance.GarantiaMuebleService.GetById(8431);
            // Custom method
            Stopwatch watch = new Stopwatch();
            watch.Start();
            MakerCheckerObject<GarantiaMueble> objToSerializeXml = new MakerCheckerObject<GarantiaMueble>(new Bladex.Garantias.Infrastructure.Serialization.XmlSerializer<MakerCheckerObject<GarantiaMueble>>());
            objToSerializeXml.AdditionalAttributes.Add("UserId", "Admin");
            objToSerializeXml.Action = MakerAndCheckerActionEnum.Edit;
            objToSerializeXml.Object = garantia;
            objToSerializeXml.Serialize();
            var resultXml = objToSerializeXml.ObjectSerialized;
            Console.WriteLine("XmlSerializer takes " + watch.Elapsed.ToString());
            watch.Restart();
            MakerCheckerObject<GarantiaMueble> objToSerializeJson = new MakerCheckerObject<GarantiaMueble>(new Bladex.Garantias.Infrastructure.Serialization.JsonSerializer<MakerCheckerObject<GarantiaMueble>>());
            objToSerializeJson.Action = MakerAndCheckerActionEnum.Edit;
            objToSerializeJson.Object = garantia;
            objToSerializeJson.AdditionalAttributes.Add("UserId", "Admin");
            objToSerializeJson.Serialize();
            var resultJson = objToSerializeJson.ObjectSerialized;
            Console.WriteLine("JsonSerializer takes " + watch.Elapsed.ToString());
            watch.Stop();
            Assert.IsNotNull(resultXml);
            Assert.IsNotNull(resultJson);
            Console.WriteLine(resultXml);
            Console.WriteLine(resultJson);

            // Deserialization
            MakerCheckerObject<GarantiaMueble> objToDeserializeXml = new MakerCheckerObject<GarantiaMueble>(new Bladex.Garantias.Infrastructure.Serialization.XmlSerializer<MakerCheckerObject<GarantiaMueble>>());
            objToDeserializeXml.ObjectSerialized = resultXml;
            objToDeserializeXml.Deserialize();
            Assert.IsNotNull(objToDeserializeXml.Object);
            Assert.AreEqual(garantia, objToDeserializeXml.Object);
            Assert.IsNotNull(objToDeserializeXml.AdditionalAttributes);
            Assert.IsTrue(objToDeserializeXml.AdditionalAttributes.ContainsKey("UserId"));
            Assert.IsNotNull(objToDeserializeXml.AdditionalAttributes["UserId"]);
            Assert.AreEqual("Admin", objToDeserializeXml.AdditionalAttributes["UserId"]);
            MakerCheckerObject<GarantiaMueble> objToDeserializeJson = new MakerCheckerObject<GarantiaMueble>(new Bladex.Garantias.Infrastructure.Serialization.JsonSerializer<MakerCheckerObject<GarantiaMueble>>());
            objToDeserializeJson.ObjectSerialized = resultJson;
            objToDeserializeJson.Deserialize();
            Assert.IsNotNull(objToDeserializeJson.Object);
            Assert.AreEqual(garantia, objToDeserializeJson.Object);
            Assert.IsNotNull(objToDeserializeJson.AdditionalAttributes);
            Assert.IsTrue(objToDeserializeJson.AdditionalAttributes.ContainsKey("UserId"));
            Assert.IsNotNull(objToDeserializeJson.AdditionalAttributes["UserId"]);
            Assert.AreEqual("Admin", objToDeserializeJson.AdditionalAttributes["UserId"]);
        }

        [TestMethod]
        public void ShouldCreateANewGuarantee()
        {
            var garantia = new GarantiaMueble();
            garantia.Cliente = ServiceFacade.Instance.ClienteService.GetAll().First();
            garantia.AseguradorSuper = ServiceFacade.Instance.AseguradorasService.GetAll().First();
            garantia.ValorInicial = 5000;
            garantia.ValorMercado = 10000;

            MakerCheckerCore<GarantiaMueble> core = new MakerCheckerCore<GarantiaMueble>(this.UserRepository, this.RoleRepository, this.OperationStatusRepository, this.ChangesetRepository, this.OperationRepository);
            Assert.IsNotNull(core);
            var operationId = core.Save("Maker", 1, garantia, MakerAndCheckerActionEnum.Add);
            core.CommitChangeset("Maker", core.GetChangesetIdentifier("Maker"));
            core.OnPersistObject += new MakerCheckerCore<GarantiaMueble>.PersistObjectHandler(core_OnPersistObject);
            core.Update(operationId, "Checker", 3, "Approved Operation");
            core.PersistObject(operationId, "Maker");
        }

        void core_OnPersistObject(object sender, MakerCheckerCorePersistOperationEventArgs<GarantiaMueble> e)
        {
            if (e.OperationStatusId == 3)
            {
                if (e.MakerCheckerObject.ObjectSerialized != null)
                {
                    e.MakerCheckerObject.Deserialize();
                }
                if (e.MakerCheckerObject.Object != null)
                {
                    ServiceFacade.Instance.GarantiaMuebleService.Save(e.MakerCheckerObject.Object);
                }
            }
        }
        /// <summary>
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
        public void ShouldSerializeWithAdditionalAttributes()
        {
            Dictionary<string, object> AdditionalAttributes = new Dictionary<string, object>();
            AdditionalAttributes.Add("TestAttribute_1_Int32", 1);
            AdditionalAttributes.Add("TestAttribute_2_String", "TestValue2");
            AdditionalAttributes.Add("TestAttribute_3_Bool", false);
            AdditionalAttributes.Add("TestAttribute_4_DateTime", DateTime.Now );
            // Serialization
            var garantia = ServiceFacade.Instance.GarantiaMuebleService.GetById(8431);
            var result = EntityBaseXmlSerializer.Serialize(garantia, EntityBaseXmlSerializer.MakerAndCheckerAction.Edit, AdditionalAttributes);
            Assert.IsNotNull(result);
            Console.WriteLine(result.Entity);
            // Deserialization
            var deserializedEntity = EntityBaseXmlSerializer.Deserialize(result.Entity);
            Assert.AreEqual(garantia, deserializedEntity);
            
        }
    }
}
