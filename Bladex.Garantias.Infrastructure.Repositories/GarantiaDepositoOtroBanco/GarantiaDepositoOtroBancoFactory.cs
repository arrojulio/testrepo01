using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaDepositoOtroBanco
{
    internal class GarantiaDepositoOtroBancoFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaDepositoOtroBanco> 
    {
        #region Field Names

        internal static class FieldNames
        {

            public const string BancoSuperId = "BancoSuper";
            public const string Id = "ID";
        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.GarantiaDepositoOtroBanco BuildEntity(System.Data.IDataReader reader)
        {
            IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>();

            DomainModel.DomainBase.GarantiaBase garantiaBase = repository.FindBy(reader[FieldNames.Id].ToString());
            if (garantiaBase == null)
            {
                throw new Exception("Cannot find garantia Base id: " + reader[FieldNames.Id].ToString());
            }

            DomainModel.DomainBase.GarantiaDepositoOtroBanco garantia = AutoMapper.Mapper.Map<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaDepositoOtroBanco>(garantiaBase);

            return garantia;

        }

        #endregion
    }
}
