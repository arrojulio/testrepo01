using Bladex.Garantias.ImportSync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for GarantiaManagerTest and is intended
    ///to contain all GarantiaManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GarantiaManagerTest
    {


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
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GarantiaManager Constructor
        ///</summary>
        [TestMethod()]
        public void GarantiaManagerConstructorTest()
        {
            GarantiaManager target = new GarantiaManager();
            Assert.IsTrue(true);
        }

        /// <summary>
        ///A test for DoDelete
        ///</summary>
        [TestMethod()]
        public void DoDeleteTest()
        {
            GarantiaManager target = new GarantiaManager(); // TODO: Initialize to an appropriate value
            GarantiaBase target1 = null; // TODO: Initialize to an appropriate value
            target.DoDelete(target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsert
        ///</summary>
        [TestMethod()]
        public void DoInsertTest()
        {
            GarantiaManager target = new GarantiaManager(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsert(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertBase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertBaseTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaBase garantia = null; // TODO: Initialize to an appropriate value
            target.DoInsertBase(source, garantia);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertDeposito
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertDepositoTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertDeposito(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertDepositoOtros
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertDepositoOtrosTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertDepositoOtros(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertInmueble
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertInmuebleTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertInmueble(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertMueble
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertMuebleTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertMueble(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertOtras
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertOtrasTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertOtras(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoInsertPrendaria
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoInsertPrendariaTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            target.DoInsertPrendaria(source);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        public void DoUpdateTest()
        {
            GarantiaManager target = new GarantiaManager(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = new IMPORT_TH_ATOMO_GARANTIAS();

            GarantiaBase target1 = new GarantiaDeposito();
            target.DoUpdate(source, target1);

            Assert.IsTrue(target.LastIdAdded != -1);
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest1()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaDeposito target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest2()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaInmueble target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest3()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaDepositoOtroBanco target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest4()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaMueble target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest5()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaPrenda target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void DoUpdateTest6()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            IMPORT_TH_ATOMO_GARANTIAS source = null; // TODO: Initialize to an appropriate value
            GarantiaOtra target1 = null; // TODO: Initialize to an appropriate value
            target.DoUpdate(source, target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for convertDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Bladex.Garantias.ImportSync.dll")]
        public void convertDateTest()
        {
            GarantiaManager_Accessor target = new GarantiaManager_Accessor(); // TODO: Initialize to an appropriate value
            string dateStr = string.Empty; // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime? actual;
            actual = GarantiaManager_Accessor.ConvertDate(dateStr);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LastIdAdded
        ///</summary>
        [TestMethod()]
        public void LastIdAddedTest()
        {
            GarantiaManager target = new GarantiaManager(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.LastIdAdded = expected;
            actual = target.LastIdAdded;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
