using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker operation class.
    /// </summary>
    public class MakerCheckerOperation : EntityBase
    {
        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        /// <value>
        /// The operation id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationId
        {
            get { return this.GetKeyAs<int>(); }
            set { this.Key = value; }
        }

        /// <summary>
        /// Gets or sets the changeset id.
        /// </summary>
        /// <value>
        /// The changeset id of type <see cref="System.Guid"/>
        /// </value>
        public Guid ChangesetId { get; set; }
        /// <summary>
        /// Gets or sets the changeset.
        /// </summary>
        /// <value>
        /// The changeset of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerChangeset"/>
        /// </value>
        public MakerCheckerChangeset Changeset {get;set;}
        /// <summary>
        /// Gets or sets the checker user id.
        /// </summary>
        /// <value>
        /// The checker user id of type <see cref="System.String"/>
        /// </value>
        public string CheckerUserId { get; set; }
        /// <summary>
        /// Gets or sets the checker user.
        /// </summary>
        /// <value>
        /// The checker user of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerUser"/>
        /// </value>
        public MakerCheckerUser CheckerUser { get; set; }
        /// <summary>
        /// Gets or sets the checker date.
        /// </summary>
        /// <value>
        /// The checker date of type <see cref="DateTime"/>
        /// </value>
        public DateTime? CheckerDate { get; set; }
        /// <summary>
        /// Gets or sets the maker date.
        /// </summary>
        /// <value>
        /// The maker date of type <see cref="System.DateTime"/>
        /// </value>
        public DateTime MakerDate { get; set; }
        /// <summary>
        /// Gets or sets the operation status id.
        /// </summary>
        /// <value>
        /// The operation status id of type <see cref="System.Int32"/>
        /// </value>
        public int OperationStatusId { get; set; }
        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        /// <value>
        /// The operation status of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperationStatus"/>
        /// </value>
        public MakerCheckerOperationStatus OperationStatus { get; set; }
        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        /// <value>
        /// The item id of type <see cref="int"/>
        /// </value>
        public int? ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item of type <see cref="System.String"/>
        /// </value>
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>
        /// The type of the item.
        /// </value>
        public string ItemType { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment of type <see cref="System.String"/>
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets the maker checker object.
        /// </summary>
        /// <returns></returns>
        public dynamic GetMakerCheckerObject()
        {
            Type elementType = Type.GetType(this.ItemType);
            Type[] types = new Type[] { elementType };
            Type listType = typeof(MakerCheckerObject<>);
            Type genericType = listType.MakeGenericType(types);
            dynamic makerCheckerObject = Activator.CreateInstance(genericType);
            makerCheckerObject.ObjectSerialized = this.Item;
            makerCheckerObject.Deserialize();
            return makerCheckerObject;
        }
    }
}
