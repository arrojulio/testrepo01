using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class MakerCheckerOperationStatusSqlRepository
    {
        protected IMakerCheckerOperationStatusRepository OperationStatusRepository;
        public const string _OPERATION_STATUS_TO_GENERATE = "Sample Operation";
        public const string _OPERATION_STATUS_TO_UPDATE = "Sample Operation Updated";

        [TestInitialize]
        public void Setup()
        {
            this.OperationStatusRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>();
        }

        [TestMethod]
        public void GetAllReturnsSameQuantityAsCount()
        {
            var expected = this.OperationStatusRepository.Count();
            var actual = this.OperationStatusRepository.GetAll();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void ShouldCreateAndUpdateAnOperationStatus()
        {
            var expected = new MakerCheckerOperationStatus();
            expected.OperationStatusDescription = _OPERATION_STATUS_TO_GENERATE;
            expected = this.OperationStatusRepository.Add(expected);
            var actual = this.OperationStatusRepository.FindBy(expected.OperationStatusId);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.OperationStatusId, actual.OperationStatusId);
            Assert.AreEqual(expected.OperationStatusDescription, actual.OperationStatusDescription);
            actual.OperationStatusDescription = _OPERATION_STATUS_TO_UPDATE;
            actual = this.OperationStatusRepository.Add(actual);
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(expected.OperationStatusDescription, actual.OperationStatusDescription);
        }
    }
}
