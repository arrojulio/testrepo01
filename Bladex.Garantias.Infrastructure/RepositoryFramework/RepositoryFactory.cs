using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Bladex.Garantias.Infrastructure.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework.Configuration;
//if (repositoryType == null) throw new ConfigurationException(string.Format("The type {0} is not found. Can not create the repository.", settings.RepositoryMappings[interfaceShortName].RepositoryFullTypeName));
namespace Bladex.Garantias.Infrastructure.RepositoryFramework
{
    public static class RepositoryFactory
    {
        // Dictionary to hold the Repositories (acts as a cache)
        private static Dictionary<string, object> Repositories = new Dictionary<string, object>();

        private static Object _syncRoot = new Object();

        /// <summary>
        /// Gets or creates an instance of the requested interface.  Once a 
        /// repository is created and initialized, it is cached, and all 
        /// future requests for the repository will come from the cache.
        /// </summary>
        /// <typeparam name="TRepository">The interface of the repository 
        /// to create.</typeparam>
        /// <typeparam name="TEntity">The type of the EntityBase that the 
        /// repository is for.</typeparam>
        /// <param name="unitOfWork">The unit of work that the repository 
        /// will be participating in.</param>
        /// <returns>An instance of the interface requested.</returns>
        public static TRepository GetRepository<TRepository, TEntity>(IUnitOfWork unitOfWork)
            where TRepository : class, IRepository<TEntity>
            where TEntity : EntityBase
        {
            // Initialize the provider's default value
            TRepository repository = default(TRepository);

            string interfaceShortName = typeof(TRepository).Name;

            // Lock Access to avoid deadlocks
            lock (_syncRoot)
            {
                // See if the provider was already created and is in the cache
                if (!RepositoryFactory.Repositories.ContainsKey(interfaceShortName))
                {
                    // Not there, so create it

                    // Get the repositoryMappingsConfiguration config section
                    RepositorySettings settings = (RepositorySettings) ConfigurationManager.GetSection(RepositoryMappingConstants.RepositoryMappingsConfigurationSectionName);

                    // Get the type to be created
                    RepositoryMappingElement repoConfig = settings.RepositoryMappings[interfaceShortName];
                    if (repoConfig == null) throw new ConfigurationErrorsException(string.Format("There is no repository mapping element for the interface {0}. Please check your configuration file.", interfaceShortName));
                    string repositoryFullTypeName = repoConfig.RepositoryFullTypeName;

                    Type repositoryType = Type.GetType(repositoryFullTypeName);
                    if (repositoryType == null) throw new ConfigurationErrorsException(string.Format("The type {0} is not found. Can not create the repository.", settings.RepositoryMappings[interfaceShortName].RepositoryFullTypeName));

                    // See if an IUnitOfWork needs to be injected to the repository's constructor
                    object[] constructorArgs = null;

                    // Check if an IUnitOfWork was passed in and if the repository 
                    // type to be created derives from RepositoryBase<T>
                    if (unitOfWork != null && repositoryType.IsSubclassOf(typeof (RepositoryBase<TEntity>)))
                    {
                        constructorArgs = new object[] {unitOfWork};
                    }

                    // Create the repository, and cast it to the interface specified
                    repository = Activator.CreateInstance(repositoryType, constructorArgs) as TRepository;
                    if (repository == null) throw new ConfigurationErrorsException(string.Format("Cannot instantiate the type {0}. Check your configuration file.", repositoryType.Name));

                    // Add the new provider instance to the cache
                    //TODO: Cache of repositories enabled temporarily
                    RepositoryFactory.Repositories.Add(interfaceShortName, repository);
                }
                else
                {
                    // The provider was in the cache, so retrieve it
                    repository = (TRepository) RepositoryFactory.Repositories[interfaceShortName];
                }
            }
            return repository;
        }

        /// <summary>
        /// Gets or creates an instance of the requested interface.  Once a 
        /// repository is created and initialized, it is cached, and all 
        /// future requests for the repository will come from the cache.
        /// </summary>
        /// <typeparam name="TRepository">The interface of the repository 
        /// to create.</typeparam>
        /// <typeparam name="TEntity">The type of the EntityBase that the 
        /// repository is for.</typeparam>
        /// <returns>An instance of the interface requested.</returns>
        public static TRepository GetRepository<TRepository, TEntity>()
            where TRepository : class, IRepository<TEntity>
            where TEntity : EntityBase
        {
            return RepositoryFactory.GetRepository<TRepository, TEntity>(null);
        }


    }
}
