using Bladex.Garantias.Infrastructure.Serialization;

namespace Bladex.Garantias.Infrastructure.DomainBase.Serialization
{
    /// <summary>
    /// Generic IEntityBaseSerializable interface that requires an <see cref="EntityBase"/>
    /// </summary>
    /// <typeparam name="T"><see cref="EntityBase"/></typeparam>
    public interface IMakerCheckerObject<T> where T: EntityBase
    {
        /// <summary>
        ///   Serialization version of the document.
        /// </summary>
        int Version { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action of type <see cref="Bladex.Garantias.DomainModel.Components.MakerChecker.MakerAndCheckerActionEnum"/>
        /// </value>
        
        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>
        /// The object of type <see cref="T"/>
        /// </value>
        T Object { get; set; }

        string ObjectSerialized { get; set; }

        void Deserialize();
    }
}
