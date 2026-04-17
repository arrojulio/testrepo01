using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TooltipService : ICacheableService
    {
        protected ITooltipRepository Repository = RepositoryFactory.GetRepository<ITooltipRepository, Tooltip>();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public List<Tooltip> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                CacheManager.Instance.Add(this.GetCacheKey(), Repository.GetAll() as List<Tooltip>);
            }
            return CacheManager.Instance.GetData<List<Tooltip>>(this.GetCacheKey());
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="tooltipId">The tooltip id.</param>
        /// <returns></returns>
        public Tooltip GetById(string tooltipId)
        {
            string cacheKey = string.Format("TooltipService.GetById({0})", tooltipId);
            if (!CacheManager.Instance.Contains(cacheKey))
            {
                CacheManager.Instance.Add(cacheKey, Repository.FindBy(tooltipId));
            }
            return CacheManager.Instance.GetData<Tooltip>(cacheKey);
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <param name="htmlControlId">The HTML control id.</param>
        /// <returns></returns>
        public static Tooltip GetEmpty(string htmlControlId)
        {
            return new Tooltip() { HtmlControlId = htmlControlId, TooltipName = "Help Not Found", TooltipHtmlText = "" };
        }


        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "TooltipService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
