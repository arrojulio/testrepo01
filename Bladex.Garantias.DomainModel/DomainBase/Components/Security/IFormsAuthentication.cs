using System.Collections.Generic;

namespace Bladex.Garantias.DomainModel.DomainBase.Components.Security
{
    /// <summary>
    /// IFormsAuthentication interface.
    /// </summary>
    public interface IFormsAuthentication
    {
        //void SignIn(string userName, bool createPersistentCookie, IEnumerable<string> roles);
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
