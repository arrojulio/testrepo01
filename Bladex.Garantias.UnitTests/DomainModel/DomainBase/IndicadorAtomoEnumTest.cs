using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.Components;
using Bladex.Garantias.DomainModel.DomainBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.DomainModel.DomainBase
{
    [TestClass]
    public class IndicadorAtomoEnumTest
    {
        [TestMethod]
        public void ResolveFromTeinsaTest()
        {
            Dictionary<string, IndicadorAtomoEnum?> expectedValues = new Dictionary<string, IndicadorAtomoEnum?>() { { "ATOMO", IndicadorAtomoEnum.Atomo }, { "PIGNORADOS", IndicadorAtomoEnum.Pignorados }, { "NO ESTAN EN ATOMO", IndicadorAtomoEnum.NoEstaEnAtomo }, { "NULL", default(IndicadorAtomoEnum?) }, { "DUMMY_VALUE", default(IndicadorAtomoEnum?)} };
            foreach(KeyValuePair<string, IndicadorAtomoEnum?> kv in expectedValues)
            {
                Assert.AreEqual(kv.Value, IndicadorAtomoResolver.Resolve(kv.Key));
            }
            Assert.IsTrue(!IndicadorAtomoResolver.Resolve("PEPE").HasValue);
            Assert.IsTrue(!default(IndicadorAtomoEnum?).HasValue);
        }

        [TestMethod]
        public void GetIntFromEnumTest()
        {
            IndicadorAtomoEnum? obj = IndicadorAtomoEnum.Pignorados;
            int expected = 2;
            int actual = (int)obj;
            Assert.AreEqual(expected, actual);


            Enum obj2 = obj;
            object actual2temp = Enum.Parse(obj2.GetType(), obj2.ToString());
            int actual2 = (int)actual2temp;
            Assert.AreEqual(expected, actual2);
        }

        /*
         * /// 1 - ATOMO
        /// 2 - PIGNORADOS
        /// 3 - NO ESTA EN ATOMO
        Null = 0, Atomo = 1, Pignorados = 2, NoEstaEnAtomo = 3
         * */

    }
}
