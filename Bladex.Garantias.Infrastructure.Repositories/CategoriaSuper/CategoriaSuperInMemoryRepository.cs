using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.Repositories;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.Infrastructure.Repositories.CategoriaSuper
{
    public class CategoriaSuperInMemoryRepository : InMemoryRepositoryBase<Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper>, ICategoriaSuperRepository
    {
        public CategoriaSuperInMemoryRepository()
        {
            this.Database = new List<Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper>();
            // Categoria Super:
            // 01	Garantía Hipotecaria Mueble
            // 02	Garantía Hipotecaria Inmueble
            // 03	Depósitos Pignorados en el Banco
            // 04	Depósitos Pignorados en Otros Bancos
            // 05	Garantía Prendaria
            // 06	Otras Garantías
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "01", Nombre = "Garantía Hipotecaria Mueble" });
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "02", Nombre = "Garantía Hipotecaria Inmueble" });
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "03", Nombre = "Depósitos Pignorados en el Banco" });
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "04", Nombre = "Depósitos Pignorados en Otros Bancos" });
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "05", Nombre = "Garantía Prendaria" });
            this.Database.Add(new DomainModel.DomainBase.CategoriaSuper() { Key = "06", Nombre = "Otras Garantías" });

        }

        #region Implementation of ICategoriaSuperRepository

        public DomainModel.DomainBase.CategoriaSuper FindByGarantiaId(int categoriaId)
        {
            IGarantiaBaseRepository gRepo = RepositoryFactory.GetRepository<IGarantiaBaseRepository, Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>();
            Bladex.Garantias.DomainModel.DomainBase.GarantiaBase gBase = gRepo.FindBy(categoriaId);
            if (gBase != null)
                return gBase.CategoriaSuper;
            return null;
        }

        #endregion
    }
}
