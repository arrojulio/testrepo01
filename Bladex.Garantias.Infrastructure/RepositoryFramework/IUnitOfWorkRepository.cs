using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.RepositoryFramework
{
    public interface IUnitOfWorkRepository
    {
        EntityBase PersistNewItem(EntityBase item);
        EntityBase PersistUpdatedItem(EntityBase item);
        void PersistDeletedItem(EntityBase item);
    }
}
