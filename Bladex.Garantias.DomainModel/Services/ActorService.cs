using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IActorRepository = Bladex.Garantias.DomainModel.Repositories.IActorRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// The Actor Service
    /// </summary>
    public class ActorService : ICacheableService
    {
        /// <summary>
        /// Returns all Actor.
        /// </summary>
        /// <returns>A <see cref="Actor"/> IList</returns>
        public IList<Actor> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, Actor>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<Actor>;
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            CacheManager.Instance.Remove(this.GetCacheKey());
        }

        /// <summary>
        /// Return one Actor by Id
        /// </summary>
        /// <param name="actorId">Actor Id</param>
        /// <returns>A <see cref="Actor"/> entity.</returns>
        public Actor GetById(string actorId)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, Actor>();
            return repository.FindBy(actorId);
        }

        /// <summary>
        /// Return one Actor by Nombre
        /// </summary>
        /// <param name="actorName">Actor name</param>
        /// <returns>A <see cref="Actor"/> entity.</returns>
        public Actor GetByName(string actorName)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, Actor>();
            Actor actor = repository.GetByName(actorName);
            return actor ?? ActorService.GetEmpty();
        }

        public Actor Save(Actor actor)
        {
            IActorRepository repository = RepositoryFactory.GetRepository<IActorRepository, Actor>();
            Actor actObj = repository.GetByName(actor.Nombre);
            if (actObj == null)
            {
                actObj = repository.FindBy(actor.Key);
                if (actObj == null)
                {
                    actor.Key = null;
                }
                else
                {
                    actor.Key = actObj.Key;
                }
            }
            else
            {
                actor.Key = actObj.Key;
            }
            return repository.Add(actor);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Actor GetEmpty()
        {
            return new Actor() { Key = "NA", Nombre = "NA", Pais = PaisService.GetEmpty() };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "ActorService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(0, 1, 0);
        }

        #endregion
    }
}
