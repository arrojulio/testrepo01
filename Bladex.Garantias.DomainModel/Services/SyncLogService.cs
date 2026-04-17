using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using Bladex.Garantias.DomainModel.Repositories;

namespace Bladex.Garantias.DomainModel.Services
{
    public class SyncLogService
    {
        /// <summary>
        /// Returns all SyncLog.
        /// </summary>
        /// <returns>A <see cref="SyncLog"/> IList</returns>
        public IList<SyncLog> GetAll()
        {
            ISyncLogRepository repository = RepositoryFactory.GetRepository<ISyncLogRepository, SyncLog>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one SyncLog by Id
        /// </summary>
        /// <param name="syncLogId">SyncLog Id</param>
        /// <returns>A <see cref="SyncLog"/> entity.</returns>
        public SyncLog GetById(string syncLogId)
        {
            ISyncLogRepository repository = RepositoryFactory.GetRepository<ISyncLogRepository, SyncLog>();
            return repository.FindBy(syncLogId);
        }

        /// <summary>
        /// Retorna el ultimo Sync Log disponible. Null en caso que no exista
        /// </summary>
        /// <returns></returns>
        public SyncLog GetLastSyncLog()
        {
            ISyncLogRepository repository = RepositoryFactory.GetRepository<ISyncLogRepository, SyncLog>();
            return repository.GetLastSyncLog();
        }

        /// <summary>
        /// Retorna el ultimo SyncLog satisfactorio. Null en caso que no exista
        /// </summary>
        /// <returns></returns>
        public SyncLog GetLastSuccessLog()
        {
            ISyncLogRepository repository = RepositoryFactory.GetRepository<ISyncLogRepository, SyncLog>();
            return repository.GetLastSuccessLog();

        }

        public SyncLog AddSyncLog(SyncLog log)
        {
            ISyncLogRepository repository = RepositoryFactory.GetRepository<ISyncLogRepository, SyncLog>();
            return repository.Add(log);
        }

    }
}
