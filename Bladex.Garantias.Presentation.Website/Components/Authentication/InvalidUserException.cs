using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bladex.Garantias.Presentation.Website.Components.Authentication
{
    /// <summary>
    /// The invalid user exception class.
    /// </summary>
    public class InvalidUserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        /// <param name="message">The message of type <see cref="System.String"/></param>
        public InvalidUserException(string message)
            : base(message)
        {
        }
    }
}