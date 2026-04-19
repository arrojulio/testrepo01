using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Serialization
{
    /// <summary>
    /// IEntityBaseSerializer&lt;T&gt; interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityBaseSerializer<T> where T : EntityBase
    {
        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize of type <see cref="T"/></param>
        /// <returns></returns>
        string Serialize(T objToSerialize);

        /// <summary>
        /// Deserializes the specified obj to deserialize.
        /// </summary>
        /// <param name="objToDeserialize">The obj to deserialize of type <see cref="System.String"/></param>
        /// <returns></returns>
        T Deserialize(string objToDeserialize);
    }
}
