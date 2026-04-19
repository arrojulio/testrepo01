using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.Autocomplete;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IActorRepository = Bladex.Garantias.DomainModel.Repositories.IActorRepository;

namespace Bladex.Garantias.DomainModel.Services
{

    /// <summary>
    /// The autocomplete value service class.
    /// </summary>
    public class AutocompleteValueService : ICacheableService
    {
        /// <summary>
        ///   <see cref="Bladex.Garantias.DomainModel.Repositories.IAutocompleteValueRepository"/>
        /// </summary>
        private IAutocompleteValueRepository _repository = RepositoryFactory.GetRepository<IAutocompleteValueRepository, AutocompleteValue>();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<AutocompleteValue> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                CacheManager.Instance.Add(this.GetCacheKey(), _repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<AutocompleteValue>;
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            CacheManager.Instance.Remove(this.GetCacheKey());
        }

        /// <summary>
        /// Gets the by HTML control id.
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <returns></returns>
        public List<AutocompleteValue> GetByHtmlControlId(string htmlControlId)
        {
            
            List<AutocompleteValue> entity = _repository.GetByHtmlControlId(htmlControlId).ToList();
            return entity;
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity of type <see cref="Bladex.Garantias.DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue"/></param>
        /// <returns></returns>
        public AutocompleteValue Save(AutocompleteValue entity)
        {
            if(_repository.GetByHtmlControlIdAndValue(entity.HtmlControlId, entity.Value) == null)
                return _repository.Add(entity);
            return entity;
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static AutocompleteValue GetEmpty()
        {
            return new AutocompleteValue();
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "AutocompleteValueService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(0, 1, 0);
        }

        #endregion
    }
}
