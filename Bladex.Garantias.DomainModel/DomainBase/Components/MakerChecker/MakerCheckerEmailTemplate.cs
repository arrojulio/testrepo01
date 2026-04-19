using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker
{
    /// <summary>
    /// The maker checker email template class.
    /// </summary>
    public class MakerCheckerEmailTemplate : EntityBase
    {
        /// <summary>
        /// Gets or sets the email template id.
        /// </summary>
        /// <value>
        /// The email template id of type <see cref="System.Int32"/>
        /// </value>
        public int EmailTemplateId
        {
            get { return this.GetKeyAs<int>(); }
            set { this.Key = value; }
        }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>
        /// The name of the template.
        /// </value>
        public string TemplateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject of type <see cref="System.String"/>
        /// </value>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body of type <see cref="System.String"/>
        /// </value>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>
        /// The cc of type <see cref="System.String"/>
        /// </value>
        public string Cc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        /// <value>
        /// The BCC of type <see cref="System.String"/>
        /// </value>
        public string Bcc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maker identifier.
        /// </summary>
        /// <value>
        /// The maker identifier of type <see cref="System.String"/>
        /// </value>
        public string MakerIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the checker identifier.
        /// </summary>
        /// <value>
        /// The checker identifier of type <see cref="System.String"/>
        /// </value>
        public string CheckerIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data identifier.
        /// </summary>
        /// <value>
        /// The data identifier of type <see cref="System.String"/>
        /// </value>
        public string DataIdentifier
        {
            get;
            set;
        }
    }
}