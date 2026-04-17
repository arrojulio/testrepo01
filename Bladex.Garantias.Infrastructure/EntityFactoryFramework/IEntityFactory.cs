using System.Data;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.EntityFactoryFramework
{
    public interface IEntityFactory<T> where T: EntityBase
    {
        T BuildEntity(IDataReader reader);
    }
}
