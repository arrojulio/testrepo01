using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bladex.Garantias.Application.Facades;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Logging;

namespace Bladex.Garantias.Presentation.Website.Models
{
    public static class GarantiaExtensions
    {
        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="garantia">The garantia of type <see cref="Bladex.Garantias.DomainModel.DomainBase.GarantiaBase"/></param>
        /// <returns></returns>
        public static GarantiaBaseModel CreateModel(this GarantiaBase garantia)
        {
            GarantiaBaseModel model;
            if (garantia is GarantiaDeposito)
            {
                model = new GarantiaDepositoModel();
            }
            else if (garantia is GarantiaDepositoOtroBanco)
            {
                model = new GarantiaDepositoOtroBancoModel();
            }
            else if (garantia is GarantiaInmueble)
            {
                model = new GarantiaInmuebleModel();
            }
            else if (garantia is GarantiaMueble)
            {
                model = new GarantiaMuebleModel();
            }
            else if (garantia is GarantiaPrenda)
            {
                model = new GarantiaPrendaModel();
            }
            else if (garantia is GarantiaOtra)
            {
                model = new GarantiaOtraModel();
            }
            else
            {
                throw new ArgumentException("La garantia no es de un tipo valido.", "garantia");
            }
            try
            {
                garantia.Key = Convert.ToInt32(garantia.Key);
                model = (GarantiaBaseModel)AutoMapper.Mapper.Map(garantia, garantia.GetType(), model.GetType());
                
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error convirtiendo {0} a {1}.", garantia.GetType(), model.GetType()), null, ex);
                throw;
            }

            return model;
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="model">The model of type <see cref="Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel"/></param>
        /// <returns></returns>
        public static GarantiaBase CreateEntity(this GarantiaBaseModel model)
        {
            GarantiaBase garantia;
            if (model is GarantiaDepositoModel)
            {
                garantia = new GarantiaDeposito();
            }
            else if (model is GarantiaDepositoOtroBancoModel)
            {
                garantia = new GarantiaDepositoOtroBanco();
            }
            else if (model is GarantiaInmuebleModel)
            {
                garantia = new GarantiaInmueble();
            }
            else if (model is GarantiaMuebleModel)
            {
                garantia = new GarantiaMueble();
            }
            else if (model is GarantiaPrendaModel)
            {
                garantia = new GarantiaPrenda();
            }
            else if (model is GarantiaOtraModel)
            {
                garantia = new GarantiaOtra();
            }
            else
            {
                throw new ArgumentException("La garantia no es de un tipo valido.", "model");
            }
            try
            {
                garantia = (GarantiaBase)AutoMapper.Mapper.Map(model, garantia, model.GetType(), garantia.GetType());
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error convirtiendo {0} a {1}.", model.GetType(), garantia.GetType()), null, ex);
                throw;
            }

            return garantia;
        }
    }
}