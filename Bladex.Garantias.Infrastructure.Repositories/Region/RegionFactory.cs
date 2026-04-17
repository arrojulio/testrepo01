using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Region
{
    internal class RegionFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Region>
    {
        #region Field Names

        internal static class FieldNames
        {
            // TODO: Map SQL Column Names here.
            public const string RegionId = "ID";
            public const string RegionNombre = "Nombre";
        }

        #endregion

        #region IEntityFactory<PaisId> Members

        public Bladex.Garantias.DomainModel.DomainBase.Region BuildEntity(IDataReader reader)
        {
            var region = new DomainModel.DomainBase.Region();
            region.Key = reader[FieldNames.RegionId].ToString();
            region.Nombre = reader[FieldNames.RegionNombre].ToString();

            return region;
        }

        #endregion

    }
}