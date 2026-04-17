using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;
using Newtonsoft.Json;

namespace Bladex.Garantias.Infrastructure.Serialization
{
    /// <summary>
    /// The json serializer class.
    /// </summary>
    public class JsonSerializer<T> : IEntityBaseSerializer<T> where T : EntityBase
    {
        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize of type <see cref="T"/></param>
        /// <returns></returns>
        public string Serialize(T objToSerialize)
        {
            string json = JsonConvert.SerializeObject(objToSerialize);
            return json;
        }

        /// <summary>
        /// Deserializes the specified obj to deserialize.
        /// </summary>
        /// <param name="objToDeserialize">The obj to deserialize of type <see cref="System.String"/></param>
        /// <returns></returns>
        public T Deserialize(string objToDeserialize)
        {
            return (T)JsonConvert.DeserializeObject(objToDeserialize,typeof(T));
        }
    }
}
