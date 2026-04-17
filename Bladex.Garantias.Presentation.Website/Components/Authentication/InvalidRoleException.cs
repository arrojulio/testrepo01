using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.Components.Authentication
{
    /// <summary>
    /// The invalid role exception class.
    /// </summary>
    public class InvalidRoleException : Exception 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRoleException"/> class.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        public InvalidRoleException(string message): base(message)
        {
        }
    }
}