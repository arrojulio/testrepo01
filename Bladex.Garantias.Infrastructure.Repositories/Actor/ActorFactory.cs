using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.EntityFactoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.Actor
{
    internal class ActorFactory : IEntityFactory<Bladex.Garantias.DomainModel.DomainBase.Actor>
    {
        #region Field Names

        internal static class FieldNames
        {
            
            public const string ActorId = "ID";
            public const string ActorNombre = "Nombre";
            public const string PaisId = "Pais";
        }

        #endregion

        #region IEntityFactory<Actor> Members

        public DomainModel.DomainBase.Actor BuildEntity(System.Data.IDataReader reader)
        {
            var  actor = new DomainModel.DomainBase.Actor();
            actor.Key = reader[FieldNames.ActorId].ToString();
            actor.Nombre = reader[FieldNames.ActorNombre].ToString();
           
            return actor;
        }

        #endregion
    }
}
