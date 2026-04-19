using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CategoriaSuper
{
    internal class CategoriaSuperFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper>
    {
        #region Field Names

        internal static class FieldNames
        {
            public const string CategoriaSuperId = "ID";
            public const string CategoriaSuperName = "Nombre";
            public const string CategoriaSuperIsReadOnly = "IsReadOnly";
        }

        #endregion

        #region IEntityFactory<CategoriaSuper> Members

        public DomainModel.DomainBase.CategoriaSuper BuildEntity(System.Data.IDataReader reader)
        {
            var categoriaSuper = new DomainModel.DomainBase.CategoriaSuper();
            categoriaSuper.Key = reader[FieldNames.CategoriaSuperId].ToString();
            categoriaSuper.Nombre = reader[FieldNames.CategoriaSuperName].ToString();
            categoriaSuper.IsReadOnly = (bool)reader[FieldNames.CategoriaSuperIsReadOnly];
            return categoriaSuper;
        }

        #endregion
    }
}

