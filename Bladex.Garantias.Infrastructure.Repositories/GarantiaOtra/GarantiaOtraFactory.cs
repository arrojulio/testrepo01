using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.GarantiaOtra
{
    internal class GarantiaOtraFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.GarantiaOtra> 
    {
        #region Field Names

        internal static class FieldNames
        {

            public const string EmisorId = "Emisor";
            public const string Id = "ID";
            public const string NroReferencia = "NroReferencia";
        }

        #endregion

        #region IEntityFactory<GarantiaBase> Members

        public DomainModel.DomainBase.GarantiaOtra BuildEntity(System.Data.IDataReader reader)
        {
            IGarantiaBaseRepository repository = RepositoryFactory.GetRepository<IGarantiaBaseRepository, DomainModel.DomainBase.GarantiaBase>();

            DomainModel.DomainBase.GarantiaBase garantiaBase = repository.FindBy(reader[FieldNames.Id].ToString());
            if (garantiaBase == null)
            {
                throw new Exception("Cannot find garantia Base id: " + reader[FieldNames.Id].ToString());
            }


            DomainModel.DomainBase.GarantiaOtra garantia = AutoMapper.Mapper.Map<DomainModel.DomainBase.GarantiaBase, DomainModel.DomainBase.GarantiaOtra>(garantiaBase);


            return garantia;

        }

        #endregion
    }
}
