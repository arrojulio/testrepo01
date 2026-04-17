using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace Bladex.Garantias.DomainModel.Components.MakerChecker
{
    /// <summary>
    /// The maker checker object class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MakerCheckerObject<T> : EntityBase 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerObject&lt;T&gt;"/> class.
        /// </summary>
        public MakerCheckerObject() : this(new JsonSerializer<MakerCheckerObject<T>>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerObject&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="serializer">The serializer of type <see cref="Bladex.Garantias.Infrastructure.Serialization.IEntityBaseSerializer&lt;T&gt;"/></param>
        public MakerCheckerObject(IEntityBaseSerializer<MakerCheckerObject<T>> serializer)
        {
            this.Serializer = serializer;
            this.Version = 1;
            this.AdditionalAttributes = new SerializableDictionary<string, object>();
        }

        /// <summary>
        ///   <see cref="Bladex.Garantias.Infrastructure.Serialization.SerializableDictionary&lt;System.String,System.Object&gt;"/>
        /// </summary>
        public SerializableDictionary<string, object> AdditionalAttributes { get; set; }

        /// <summary>
        ///   Serialization version of the document.
        /// </summary>
        [System.Xml.Serialization.XmlAttribute("version")]
        public int Version { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerAndCheckerActionEnum"/>
        /// </value>
        [System.Xml.Serialization.XmlAttribute("action")]
        public MakerAndCheckerActionEnum Action { get; set; }
        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>
        /// The object of type <see cref="T"/>
        /// </value>
        [System.Xml.Serialization.XmlElement("Object")]
        public T Object { get; set; }

        [JsonIgnore]
        [System.Xml.Serialization.XmlIgnore()]
        public string ObjectSerialized { get; set; }

        /// <summary>
        /// Gets or sets the serializer.
        /// </summary>
        /// <value>
        /// The serializer of type <see cref="Bladex.Garantias.Infrastructure.Serialization.IEntityBaseSerializer&lt;T&gt;"/>
        /// </value>
        [System.Xml.Serialization.XmlIgnore()]
        protected IEntityBaseSerializer<MakerCheckerObject<T>> Serializer { get; set; }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        public void Serialize()
        {
            this.ObjectSerialized = this.Serializer.Serialize(this);
        }

        /// <summary>
        /// Deserializes this instance.
        /// </summary>
        public void Deserialize()
        {
            var deserialized = this.Serializer.Deserialize(this.ObjectSerialized);
            if (this.Version != deserialized.Version)
            {
                throw new SerializationException(string.Format("The version of serialized document is different from the current one. Document Version: {0}, Component Version: {1}", deserialized.Version, this.Version));
            }
            this.Action = deserialized.Action;
            this.Object = deserialized.Object;
            this.AdditionalAttributes = deserialized.AdditionalAttributes;
        }
    }
}
