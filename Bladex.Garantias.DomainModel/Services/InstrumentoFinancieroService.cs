using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.Caching;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IInstrumentoFinancieroRepository = Bladex.Garantias.DomainModel.Repositories.IInstrumentoFinancieroRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class InstrumentoFinancieroService : ICacheableService
    {
        /// <summary>
        /// Returns all InstrumentoFinanciero.
        /// </summary>
        /// <returns>A <see cref="InstrumentoFinanciero"/> IList</returns>
        public IList<InstrumentoFinanciero> GetAll()
        {
            if (!CacheManager.Instance.Contains(this.GetCacheKey()))
            {
                IInstrumentoFinancieroRepository repository = RepositoryFactory.GetRepository<IInstrumentoFinancieroRepository, InstrumentoFinanciero>();
                CacheManager.Instance.Add(this.GetCacheKey(), repository.GetAll().ToList(), this.GetTimeSpan());
            }
            return CacheManager.Instance.GetData(this.GetCacheKey()) as List<InstrumentoFinanciero>;
        }

        /// <summary>
        /// Return one InstrumentoFinanciero by Id
        /// </summary>
        /// <param name="instrumentoFinancieroId">InstrumentoFinanciero Id</param>
        /// <returns>A <see cref="InstrumentoFinanciero"/> entity.</returns>
        public InstrumentoFinanciero GetById(string instrumentoFinancieroId)
        {
            IInstrumentoFinancieroRepository repository = RepositoryFactory.GetRepository<IInstrumentoFinancieroRepository, InstrumentoFinanciero>();
            return repository.FindBy(instrumentoFinancieroId);
        }

        /// <summary>
        /// Return one InstrumentoFinanciero by Id
        /// </summary>
        /// <param name="instrumentoFinancieroId">InstrumentoFinanciero Id</param>
        /// <returns>A <see cref="InstrumentoFinanciero"/> entity.</returns>
        public InstrumentoFinanciero GetByNombre(string instrumentoFinancieroNombre)
        {
            IInstrumentoFinancieroRepository repository = RepositoryFactory.GetRepository<IInstrumentoFinancieroRepository, InstrumentoFinanciero>();
            return repository.GetAll().Where(o=>o.Nombre== instrumentoFinancieroNombre).FirstOrDefault();
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static InstrumentoFinanciero GetEmpty()
        {
            return new InstrumentoFinanciero() { Key ="NA", Nombre="NA" };
        }

        #region ICacheableService Members

        public string GetCacheKey()
        {
            return "InstrumentoFinancieroService.GetAll()";
        }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(1, 0, 0);
        }

        #endregion
    }
}
