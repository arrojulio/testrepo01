using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Services
{
    public class StatusService 
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Status> GetAll()
        {
            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            return repository.GetAll();
        }

        /// <summary>
        /// Return one Status by Id
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <returns>A <see cref="InternalStatus"/> entity.</returns>
        public Status GetById(string statusId)
        {
            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            return repository.FindBy(statusId);
        }

        public Status GetNormalStatus()
        {
            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            return this.GetStatus(repository, Status.NORMAL);
        }

        public Status GetEnEjecucionStatus()
        {
            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            return this.GetStatus(repository, Status.EN_EJECUCION);
        }
        public Status GetEjecutadaStatus()
        {
            IStatusRepository repository = RepositoryFactory.GetRepository<IStatusRepository, Status>();
            return this.GetStatus(repository, Status.EJECUTADA);
        }

        private Status GetStatus(IStatusRepository repository, String statusId)
        {
            Status status = repository.FindBy(statusId);
            
            return status;
        }

        /// <summary>
        /// Devuelve una entidad representativa vacia. 
        /// Esto se debe utilizar cuando el id es "vacio" o invalido
        /// </summary>
        /// <returns></returns>
        public static Status GetEmpty()
        {
            return new Status() { Key = "NA", Nombre = "NA" };
        }
    }
}
