using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.DomainModel.Repositories.Components
{
    public interface IPaged<T> : IEnumerable<T>
    {
        ///<summary>
        /// Get the total entity count.
        ///</summary>
        int Count { get; }

        ///<summary>
        /// Get a range of persited entities.
        ///</summary>
        IEnumerable<T> GetRange(int index, int count);
    }
}
