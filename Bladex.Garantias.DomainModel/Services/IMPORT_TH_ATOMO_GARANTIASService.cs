using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.DomainModel.DomainBase;

namespace Bladex.Garantias.DomainModel.Services
{
    public class IMPORT_TH_ATOMO_GARANTIASService
    {
        /// <summary>
        /// Returns all IMPORT_TH_ATOMO_GARANTIAS.
        /// </summary>
        /// <returns>A <see cref="IMPORT_TH_ATOMO_GARANTIAS"/> IList</returns>
        public IList<IMPORT_TH_ATOMO_GARANTIAS> GetAll()
        {
            IIMPORT_TH_ATOMO_GARANTIASRepository repository = RepositoryFactory.GetRepository<IIMPORT_TH_ATOMO_GARANTIASRepository, IMPORT_TH_ATOMO_GARANTIAS>();
            return repository.GetAll();
        }

        /// <summary>
        /// Returns all IMPORT_TH_ATOMO_GARANTIAS by Fecha Corte
        /// </summary>
        /// <param name="fechaCorte">Fecha de corte</param>
        /// <returns>A <see cref="IMPORT_TH_ATOMO_GARANTIAS"/> IList</returns>
        public IList<IMPORT_TH_ATOMO_GARANTIAS> GetByFechaCorte(DateTime fechaCorte)
        {
            IIMPORT_TH_ATOMO_GARANTIASRepository repository = RepositoryFactory.GetRepository<IIMPORT_TH_ATOMO_GARANTIASRepository, IMPORT_TH_ATOMO_GARANTIAS>();
            return repository.GetByFechaCorte(fechaCorte);
        }

        /// <summary>
        /// Devuelve la ultima fecha corte disponible. Null en caso contrario
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastFechaCorte()
        {
            IIMPORT_TH_ATOMO_GARANTIASRepository repository = RepositoryFactory.GetRepository<IIMPORT_TH_ATOMO_GARANTIASRepository, IMPORT_TH_ATOMO_GARANTIAS>();
            DateTime? result = default(DateTime?);
            try
            {
                result = repository.GetLastFechaCorte();
            }
            catch (Exception ex)
            {
                result = default(DateTime?);
            }
            return result;
        }
    }
}
