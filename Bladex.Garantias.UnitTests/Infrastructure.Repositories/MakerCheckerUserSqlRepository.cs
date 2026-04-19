using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.Repositories.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bladex.Garantias.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class MakerCheckerUserSqlRepository
    {
        protected IMakerCheckerUserRepository UserRepository;
        protected IMakerCheckerRoleRepository RoleRepository;
        protected IMakerCheckerOperationStatusRepository OperationStatusRepository;
        protected IMakerCheckerChangesetRepository ChangesetRepository;
        protected IMakerCheckerOperationRepository OperationRepository;
        protected IGarantiaMuebleRepository GarantiaBaseRepository;
        public const string _USER_ID_TO_GENERATE = "Unit-Test-User";

        [TestInitialize]
        public void Setup()
        {
            this.UserRepository = RepositoryFactory.GetRepository<IMakerCheckerUserRepository, MakerCheckerUser>();
            this.RoleRepository = RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>();
            this.OperationStatusRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationStatusRepository, MakerCheckerOperationStatus>();
            this.ChangesetRepository = RepositoryFactory.GetRepository<IMakerCheckerChangesetRepository, MakerCheckerChangeset>();
            this.OperationRepository = RepositoryFactory.GetRepository<IMakerCheckerOperationRepository, MakerCheckerOperation>();
            this.GarantiaBaseRepository = RepositoryFactory.GetRepository<IGarantiaMuebleRepository, GarantiaMueble>();
            Bladex.Garantias.Infrastructure.Bootstrapper.Setup();
            Bladex.Garantias.Infrastructure.Repositories.Bootstrapper.Setup();
        }

        

        [TestMethod]
        public void GetAllReturnsSameQuantityAsCount()
        {
            var expected = this.UserRepository.Count();
            var actual = this.UserRepository.GetAll();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void ShouldCreateANewUser()
        {
            var user = new MakerCheckerUser();
            user.UserId = _USER_ID_TO_GENERATE;
            user.Email = "test@intelledata.com";
            user.Role = this.RoleRepository.GetAll().FirstOrDefault();
            if (user.Role == null) Assert.Inconclusive("There is no existent Role to execute the test. Please create a new Role.");

            var expected = user;
            this.UserRepository.Add(user);
            var actual = this.UserRepository.FindBy(_USER_ID_TO_GENERATE);
            Assert.IsNotNull(actual);
            Assert.AreEqual(user.UserId, actual.UserId);
        }

        
    }
}
