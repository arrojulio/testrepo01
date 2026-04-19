using Bladex.Garantias.Infrastructure.DomainBase;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Representa una Region.
    /// </summary>
    public class Region : EntityBase
    {

        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public virtual string Nombre
        {
            get;
            set;
        }

        /// <summary>
        /// Codigo super intendencia para paises. Tabla SB04
        /// </summary>
        //public virtual string Codigo
        //{
        //    get;
        //    set;
        //}

    }
}
