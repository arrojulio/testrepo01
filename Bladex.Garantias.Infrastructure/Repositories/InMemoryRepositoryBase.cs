using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories
{
    /// <summary>
    /// Provides the base functionality for all in-memory repositories
    /// </summary>
    /// <typeparam name="T">An <see cref="EntityBase"/> which will be treated by the repository.</typeparam>
    public class InMemoryRepositoryBase<T> : RepositoryBase<T> where T:EntityBase
    {
        /// <summary>
        /// In Memory Collection of entities to simulate a database.
        /// </summary>
        protected IList<T> Database;

        public override long Count()
        {
            return this.Database.Count;
        }

        public override T FindBy(object key)
        {
            T result = default(T);
            result = this.Database.FirstOrDefault(o => o.Key == key);
            return result;
        }

        public override IList<T> GetAll()
        {
            return this.Database;
        }

        protected override T PersistNewItem(T item)
        {
            this.Database.Add(item);
            // Set a dummy identificator for tracking purposes.
            item.Key = new Guid();
            return item;
        }

        protected override T PersistUpdatedItem(T item)
        {
            T foundItem = this.FindBy(item.Key);
            if (foundItem != null)
            {
                this.PersistDeletedItem(foundItem);
                this.Database.Add(item);
            }
            return item;
        }

        protected override void PersistDeletedItem(T item)
        {
            T foundItem = this.FindBy(item.Key);
            if (foundItem != null)
            {
                this.Database.Remove(foundItem);
            }
        }

    }
}
