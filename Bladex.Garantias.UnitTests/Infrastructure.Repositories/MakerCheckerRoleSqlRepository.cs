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
    public class MakerCheckerRoleSqlRepository
    {
        protected IMakerCheckerRoleRepository RoleRepository;
        public const string _ROLE_TO_GENERATE = "Unit-Test-Role";

        [TestInitialize]
        public void Setup()
        {
            this.RoleRepository = RepositoryFactory.GetRepository<IMakerCheckerRoleRepository, MakerCheckerRole>();
        }

        [TestMethod]
        public void GetAllReturnsSameQuantityAsCount()
        {
            var expected = this.RoleRepository.Count();
            var actual = this.RoleRepository.GetAll();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void ShouldCreateAndDeleteANewRole()
        {
            var role = new MakerCheckerRole();
            role.RoleName = _ROLE_TO_GENERATE;
            var expected = this.RoleRepository.Add(role);
            var actual = this.RoleRepository.FindBy(expected.Key);
            Assert.AreEqual(role.RoleName, actual.RoleName);
            Assert.AreEqual(role.RoleId, actual.RoleId);
            this.RoleRepository.Remove(actual);
            actual = this.RoleRepository.FindBy(expected.Key);
            Assert.IsNull(actual);
        }
    }
}
