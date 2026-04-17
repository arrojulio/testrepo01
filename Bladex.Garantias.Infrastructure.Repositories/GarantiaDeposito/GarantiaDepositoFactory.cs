using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.Infrastructure.Repositories.GarantiaBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaDeposito
{
    internal class GarantiaDepositoFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaDeposito> 
    {
        #region Field Names

        internal static class FieldNames
        {
           
            public const string GarantiaDepositoBancoLocalSuperId = "BancoLocalSuper";
            public const string GarantiaDepositoId = "ID";
        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.GarantiaDeposito BuildEntity(System.Data.IDataReader reader)
        {
            IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>();

            DomainModel.DomainBase.GarantiaBase garantiaBase = repository.FindBy(reader[FieldNames.GarantiaDepositoId].ToString());
            if (garantiaBase == null)
            {
                throw new ApplicationException("Cannot find garantia Base with id: " + reader[FieldNames.GarantiaDepositoId].ToString());
            }

            DomainModel.DomainBase.GarantiaDeposito garantia = AutoMapper.Mapper.Map<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaDeposito>(garantiaBase);            

            return garantia;

        }

        #endregion
    }
}
