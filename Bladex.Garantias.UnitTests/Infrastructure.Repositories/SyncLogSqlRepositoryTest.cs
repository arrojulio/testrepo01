using System;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Repositories.SyncLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class SyncLogSqlRepositoryTest
    {
        [TestInitialize]
        public void Initialize()
        {
            repository = new SyncLogSqlRepository();

        }

        private ISyncLogRepository repository;

        [TestMethod]
        public void AddTest()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");
            var expected = repository.Count();
            expected++;
            SyncLog log = new SyncLog(SyncLog.Status_SUCCESS, "Sincronizacion efectuada con exito.");
            log.FechaCorte = DateTime.Now;
            log.TimeStamp = DateTime.Now;
            log.ItemsAdded = "TEST Added";
            log.ItemsUpdated = "TEST Updated";
            log.ItemsDeleted = "TEST Deleted";
            repository.Add(log);

            var actual = repository.Count();
            Assert.AreEqual(actual, expected);
            
        }
    }
}
