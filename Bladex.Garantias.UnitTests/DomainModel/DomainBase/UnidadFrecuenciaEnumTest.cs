using System.Collections.Generic;
using Bladex.Garantias.DomainModel.Components;
using Bladex.Garantias.DomainModel.DomainBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.DomainModel.DomainBase
{
    [TestClass]
    public class UnidadFrecuenciaEnumTest
    {
        [TestMethod]
        public void ResolveFromEnumTest()
        {
            Dictionary<string, UnidadFrecuenciaEnum?> expectedValues = new Dictionary<string, UnidadFrecuenciaEnum?>() { { "dd", UnidadFrecuenciaEnum.Days }, { "mm", UnidadFrecuenciaEnum.Months }, { "yy", UnidadFrecuenciaEnum.Years }, { "NULL", default(UnidadFrecuenciaEnum?) }, { "DUMMY_VALUE", default(UnidadFrecuenciaEnum?) } };
            foreach (KeyValuePair<string, UnidadFrecuenciaEnum?> kv in expectedValues)
            {
                Assert.AreEqual(kv.Value, UnidadFrecuenciaResolver.Resolve(kv.Key));
            }
        }

        [TestMethod]
        public void ResolveFromTextTest()
        {
            UnidadFrecuenciaEnum? expected = UnidadFrecuenciaEnum.Years;

            UnidadFrecuenciaEnum? actual = UnidadFrecuenciaResolver.Resolve("yy");
            Assert.AreEqual(expected, actual);

            expected = UnidadFrecuenciaEnum.Months;
            actual = UnidadFrecuenciaResolver.Resolve("mm");
            Assert.AreEqual(expected, actual);

            expected = UnidadFrecuenciaEnum.Days;
            actual = UnidadFrecuenciaResolver.Resolve("dd");
            Assert.AreEqual(expected, actual);

            expected = default(UnidadFrecuenciaEnum?);
            actual = UnidadFrecuenciaResolver.Resolve("wrongValue");
            Assert.AreEqual(expected, actual);
            
        }
    }
}
