using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;
using IInternalStatusRepository = Bladex.Garantias.DomainModel.Repositories.IInternalStatusRepository;

namespace Bladex.Garantias.DomainModel.Services
{
    public class InternalStatusService
    {

        /// <summary>
        /// Returns all Status.
        /// </summary>
        /// <returns>A <see cref="InternalStatus"/> IList</returns>
        public IList<InternalStatus> GetAll()
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Status by Id
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <returns>A <see cref="InternalStatus"/> entity.</returns>
        public InternalStatus GetById(string statusId)
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return repository.FindBy(statusId);
        }


        private InternalStatus GetStatusOrCreateMissing(IInternalStatusRepository repository, String statusId)
        {
            InternalStatus status = repository.FindBy(statusId);
            if (status == null)
            {
                status = InternalStatusService.GetEmpty();
                status.Key = InternalStatus.ACTIVE_ID;
                status.Nombre = "Active";
                repository.Add(status);
            }
            return status;
        }

        public InternalStatus GetActiveStatus()
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return this.GetStatusOrCreateMissing(repository, InternalStatus.ACTIVE_ID);
        }

        public InternalStatus GetDeletedStatus()
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return this.GetStatusOrCreateMissing(repository, InternalStatus.DELETED_ID);
        }
        public InternalStatus GetBlockedStatus()
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return this.GetStatusOrCreateMissing(repository, InternalStatus.BLOCKED_ID);
        }

        public InternalStatus GetUnknownStatus()
        {
            IInternalStatusRepository repository = RepositoryFactory.GetRepository<IInternalStatusRepository, InternalStatus>();
            return this.GetStatusOrCreateMissing(repository, InternalStatus.UNKNOWN_ID);
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static InternalStatus GetEmpty()
        {
            return new InternalStatus() { Key = "NA", Nombre = "NA" };
        }

    }
}
