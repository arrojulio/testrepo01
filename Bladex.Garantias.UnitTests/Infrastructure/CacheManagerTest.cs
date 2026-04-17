using Bladex.Garantias.Infrastructure.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bladex.Garantias.UnitTests.Infrastructure
{
    
    
    /// <summary>
    ///This is a test class for CacheManagerTest and is intended
    ///to contain all CacheManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CacheManagerTest
    {
        private static CacheManager cache = null;
        private readonly string key = Guid.NewGuid().ToString();
        private static DateTime value = DateTime.Now;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            cache = new CacheManager();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CacheManager Constructor
        ///</summary>
        [TestMethod()]
        public void CacheManagerConstructorTest()
        {
            CacheManager target = new CacheManager();
            Assert.IsNotNull(target);
            target = null;
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        [Priority(1)]
        public void AddTest()
        {
            cache.Add(key, value);
            Assert.IsTrue(cache.Contains(key));
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        [Priority(1)]
        public void AddTest1()
        {
            string key = "slidingKey";
            TimeSpan slidingTime = new TimeSpan(0,0,5); 
            cache.Add(key, value, slidingTime);
            Assert.IsTrue(cache.Contains(key));
            Assert.AreEqual(value.ToString(), ((DateTime)cache.GetData(key)).ToString());
            // Wait for sliding time.
            System.Threading.Thread.Sleep(5000);
            Assert.IsFalse(cache.Contains(key));
            Assert.IsNull(cache.GetData(key));
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        [TestMethod()]
        
        public void ContainsTest()
        {
            const bool expected = true; 
            bool actual;
            if (!cache.Contains(key))
                cache.Add(key, value);
            actual = cache.Contains(key);
            Assert.AreEqual(expected, actual);
            actual = cache.Contains(key + "somedummyvalue");
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetData
        ///</summary>
        public void GetDataTestHelper<T>()
            where T : class
        {
            T expected = null;
            T actual;
            actual = cache.GetData<T>(key);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetDataTest()
        {
            GetDataTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for GetData
        ///</summary>
        [TestMethod()]
        public void GetDataTest1()
        {
            DateTime expected = value;
            DateTime actual;
            if (!cache.Contains(key))
                cache.Add(key, value);
            actual = (DateTime)cache.GetData(key);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Instance
        ///</summary>
        [TestMethod()]
        public void InstanceTest()
        {
            CacheManager actual;
            actual = CacheManager.Instance;
            Assert.IsNotNull(actual);
        }
    }
}
