namespace Bladex.Garantias.Infrastructure.Repositories
{
    using System.Data;
    using Cliente;
    using DomainModel.Repositories;
    using RepositoryFramework;

    /// <summary>
    /// ResolveCore
    /// </summary>
    public class PaisResolver : AutoMapper.ValueResolver<IDataReader,DomainModel.DomainBase.Pais>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>DomainModel.DomainBase.Pais object</returns>
        protected override DomainModel.DomainBase.Pais ResolveCore(IDataReader source)
        {
            IPaisRepository repository = RepositoryFactory.GetRepository<IPaisRepository, DomainModel.DomainBase.Pais>();
            
            return repository.FindBy(source[(string)ClienteFactory.FieldNames.CodigoPaisId]);
        }
    }
}