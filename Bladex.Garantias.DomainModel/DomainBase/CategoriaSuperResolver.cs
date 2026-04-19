using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Services;

namespace Bladex.Garantias.DomainModel.DomainBase
{
    /// <summary>
    /// Clase encargada de resolver los distintos tipos de Garantía Super.
    /// Se utiliza para determinar, dada una especializacion de <see cref="GarantiaBase"/> la <see cref="CategoriaSuper"/> a la cual pertenece.
    /// </summary>
    public static class CategoriaSuperResolver
    {
        /// <summary>
        /// Metodo encargado de resolver la relacion. 
        ///01	Garantía Hipotecaria Mueble
        ///02	Garantía Hipotecaria Inmueble
        ///03	Depósitos Pignorados en el Banco
        ///04	Depósitos Pignorados en Otros Bancos
        ///05	Garantía Prendaria
        ///06	Otras Garantías
        /// </summary>
        /// <param name="garantia"><see cref="GarantiaBase"/></param>
        /// <returns><see cref="CategoriaSuper"/></returns>
        public static CategoriaSuper Resolve(GarantiaBase garantia)
        {
            CategoriaSuperService svc = new CategoriaSuperService();
            string categoriaSuperId = string.Empty;
            if (garantia is GarantiaDeposito)
            {
                categoriaSuperId = "03";
            }
            else if (garantia is GarantiaDepositoOtroBanco)
            {
                categoriaSuperId = "04";
            }
            else if (garantia is GarantiaInmueble)
            {
                categoriaSuperId = "02";
            }
            else if (garantia is GarantiaMueble)
            {
                categoriaSuperId = "01";
            }
            else if (garantia is GarantiaOtra)
            {
                categoriaSuperId = "06";
            }
            else if (garantia is GarantiaPrenda)
            {
                categoriaSuperId = "05";
            }

            return svc.GetById(categoriaSuperId);
        }

        /// <summary>
        /// Resolves the specified categoria super.
        /// </summary>
        /// <param name="categoriaSuper">The categoria super of type <see cref="Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper"/></param>
        /// <returns></returns>
        public static Type Resolve(CategoriaSuper categoriaSuper)
        {
            if (categoriaSuper == null) return null;

            if (categoriaSuper.Key.ToString() == "03")
            {
                return typeof(GarantiaDeposito);
            }
            else if (categoriaSuper.Key.ToString() == "04")
            {
                return typeof(GarantiaDepositoOtroBanco);
            }
            else if (categoriaSuper.Key.ToString() == "02")
            {
                return typeof(GarantiaInmueble);
            }
            else if (categoriaSuper.Key.ToString() == "01")
            {
                return typeof(GarantiaMueble);
            }
            else if (categoriaSuper.Key.ToString() == "06")
            {
                return typeof(GarantiaOtra);
            }
            else if (categoriaSuper.Key.ToString() == "05")
            {
                return typeof(GarantiaPrenda);
            }
            return typeof(GarantiaBase);

        }


    }
}
