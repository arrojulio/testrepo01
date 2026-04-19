using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories.Components;
using System.Collections;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.Pagination
{
    public class Paged<T> //: IPaged<T>
    {
     /*   private readonly IQueryable<T> source;

        public Paged(IQueryable<T> source)
        {
            this.source = source;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return source.Count(); }
        }

        public IEnumerable<T> GetRange(int index, int count)
        {
            return source.Skip(index).Take(count);
        }*/
    }
}
