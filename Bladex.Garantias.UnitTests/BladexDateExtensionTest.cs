using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bladex.Garantias.Infrastructure.Extensions;
namespace Bladex.Garantias.UnitTests
{
    [TestClass]
    public class BladexDateExtensionTestClass
    {
        [TestMethod]
        public void BladexDateExtensionTest()
        {
            string expected = "20110101";
            DateTime value = new DateTime(2011, 1, 1);

            string actual = value.ToBladexFormat();

            Assert.AreEqual(expected, actual);
        }
    }
}
