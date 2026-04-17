using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Interface used to define the functionality of a <see cref="GarantiaBase"/>
    /// </summary>
    public interface IGarantiaFunciones
    {
        /// <summary>
        /// Gets the identificacion documento garantia.
        /// </summary>
        /// <returns></returns>
        string GetIdentificacionDocumentoGarantia();
        /// <summary>
        /// Gets the identificacion documento garantia.
        /// </summary>
        /// <param name="valor">The valor of type <see cref="System.String"/></param>
        void SetIdentificacionDocumentoGarantia(string valor);
        /// <summary>
        /// Gets the nombre organismo.
        /// </summary>
        /// <returns></returns>
        string NombreOrganismo { get; set; }
        /// <summary>
        /// Gets the ratio cobertura garantia.
        /// </summary>
        /// <returns></returns>
        double GetRatioCoberturaGarantia();
        /// <summary>
        /// Gets the valor garantia super intendencia.
        /// </summary>
        /// <returns></returns>
        decimal GetValorGarantiaSuperIntendencia();

    }
}
