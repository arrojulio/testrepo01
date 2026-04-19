using Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker;

namespace Bladex.Garantias.DomainModel.Components.MakerChecker
{
    /// <summary>
    /// The maker checker message formatter class.
    /// </summary>
    public class MakerCheckerMessageFormatter
    {
        private MakerCheckerEmailTemplate _message;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MakerCheckerMessageFormatter"/> class.
        /// </summary>
        /// <param name="Message">The message of type <see cref="MakerCheckerEmailTemplate"/></param>
        public MakerCheckerMessageFormatter(MakerCheckerEmailTemplate Message)
        {
            _message = Message;
        }

        /// <summary>
        /// Gets the message formatted.
        /// </summary>
        /// <param name="MakerUserId">The maker user id of type <see cref="System.String"/></param>
        /// <param name="CheckerUserId">The checker user id of type <see cref="System.String"/></param>
        /// <param name="MessageData">The message data of type <see cref="System.String"/></param>
        /// <returns></returns>
        public MakerCheckerEmailTemplate GetMessageFormatted(string MakerUserId, string CheckerUserId, string MessageData)
        {
            this._message.Body.Replace(this._message.MakerIdentifier, MakerUserId);
            this._message.Body.Replace(this._message.CheckerIdentifier, CheckerUserId);
            this._message.Body.Replace(this._message.DataIdentifier, MessageData);
            return this._message;
        }
    }
}
