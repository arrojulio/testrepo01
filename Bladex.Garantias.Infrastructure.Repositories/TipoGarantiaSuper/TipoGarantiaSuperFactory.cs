using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;
using Bladex.Garantias.DomainModel.Services;

namespace Bladex.Garantias.Infrastructure.Repositories.TipoGarantiaSuper
{
    internal class TipoGarantiaSuperFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.TipoGarantiaSuper>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string TipoGarantiaSuperId = "ID";
            public const string TipoGarantiaSuperName = "Nombre";
            public const string TipoGarantiaSuperCategoriaId = "IdCategoriaSuper";
            public const string IsActive = "IsActive";
        }

        #endregion

        #region IEntityFactory<TipoGarantiaBladex> Members

        public DomainModel.DomainBase.TipoGarantiaSuper BuildEntity(System.Data.IDataReader reader)
        {
            var tipoGarantiaSuper = new DomainModel.DomainBase.TipoGarantiaSuper();
            tipoGarantiaSuper.Key = reader[FieldNames.TipoGarantiaSuperId].ToString();
            tipoGarantiaSuper.Nombre = reader[FieldNames.TipoGarantiaSuperName].ToString();
            tipoGarantiaSuper.IsActive = Convert.ToBoolean(reader[FieldNames.IsActive].ToString());
            //CategoriaSuperService srv = new CategoriaSuperService();
            //tipoGarantiaSuper.Categoria = srv.GetById(reader[FieldNames.TipoGarantiaSuperCategoriaId].ToString());
            
            return tipoGarantiaSuper;
        }

        #endregion
    }
}
