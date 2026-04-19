using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using System.Data;

namespace Bladex.Garantias.Infrastructure.Repositories.Components.MakerChecker
{

    /// <summary>
    /// The maker checker email template factory class.
    /// </summary>
    internal class MakerCheckerEmailTemplateFactory : IEntityFactory<MakerCheckerEmailTemplate>
    {
        #region Field Names

        /// <summary>
        /// The field names class.
        /// </summary>
        internal static class FieldNames
        {
            public const string EmailTemplateId = "Id";
            public const string TemplateName = "TemplateName";
            public const string Subject = "Subject";
            public const string Body = "Body";
            public const string MakerIdentifier = "MakerIdentifier";
            public const string CheckerIdentifier = "CheckerIdentifier";
            public const string DataIdentifier = "DataIdentifier";
            public const string Cc = "Cc";
            public const string Bcc = "Bcc";
        }

        /// <summary>
        /// The table names class.
        /// </summary>
        internal static class TableNames
        {
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string EmailTemplateTable = "APP_MAKERCHECKER_EmailTemplate";
            /// <summary>
            ///   <see cref="System.String"/>
            /// </summary>
            public const string EmailTemplateRoleTable = "APP_MAKERCHECKER_RoleTemplate";
        }

        #endregion

        #region IEntityFactory<Project> Members

        /// <summary>
        /// Builds the entity.
        /// </summary>
        /// <param name="reader">The reader of type <see cref="System.Data.IDataReader"/></param>
        /// <returns></returns>
        public Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerEmailTemplate BuildEntity(IDataReader reader)
        {
            var entity = new MakerCheckerEmailTemplate()
            {
                Key = reader.GetInt32(reader.GetOrdinal(FieldNames.EmailTemplateId)),
                EmailTemplateId = reader.GetInt32(reader.GetOrdinal(FieldNames.EmailTemplateId)),
                TemplateName = reader.GetString(reader.GetOrdinal(FieldNames.TemplateName)),
                Subject = reader.IsDBNull(reader.GetOrdinal(FieldNames.Subject)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.Subject)),
                Body = reader.GetString(reader.GetOrdinal(FieldNames.Body)),
                MakerIdentifier = reader.IsDBNull(reader.GetOrdinal(FieldNames.MakerIdentifier)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.MakerIdentifier)),
                CheckerIdentifier = reader.IsDBNull(reader.GetOrdinal(FieldNames.CheckerIdentifier)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.CheckerIdentifier)),
                DataIdentifier = reader.IsDBNull(reader.GetOrdinal(FieldNames.DataIdentifier)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.DataIdentifier)),
                Cc = reader.IsDBNull(reader.GetOrdinal(FieldNames.Cc)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.Cc)),
                Bcc = reader.IsDBNull(reader.GetOrdinal(FieldNames.Bcc)) ? string.Empty : reader.GetString(reader.GetOrdinal(FieldNames.Bcc))
            };
            return entity;
           
        }

        #endregion

    }
}
