using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.Infrastructure.Serialization
{
    /// <summary>
    /// The XML serializer class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlSerializer<T> : Bladex.Garantias.Infrastructure.Serialization.IEntityBaseSerializer<T> where T : EntityBase
    {
        /// <summary>
        /// Serializes the specified obj to serialize.
        /// </summary>
        /// <param name="objToSerialize">The obj to serialize of type <see cref="T"/></param>
        /// <returns></returns>
        public string Serialize(T objToSerialize)
        {
            StringBuilder strBuilder = new StringBuilder();
            StringWriter writer = new StringWriter(strBuilder);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(objToSerialize.GetType());
            x.Serialize(writer, objToSerialize);
            writer.Flush();
            writer.Close();
            return strBuilder.ToString();
        }

        /// <summary>
        /// Deserializes the specified obj to deserialize.
        /// </summary>
        /// <param name="objToDeserialize">The obj to deserialize of type <see cref="System.String"/></param>
        /// <returns></returns>
        public T Deserialize(string objToDeserialize)
        {
            StringReader reader = new StringReader(objToDeserialize);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)x.Deserialize(reader);
        }
    }
}
