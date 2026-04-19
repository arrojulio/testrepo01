using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IGarantiaBaseRepository : IRepository<GarantiaBase>
    {
        GarantiaBase GetByFccRef(String FCCRef);
        IDictionary<String, int> GetFccReferences();
        Decimal GetUdfGarantiasValorGarantia(int garantiaId);   //Valor Necesario de la garantia
        Boolean GetUdfValidationUnblockGuarantee(int garantiaId);
        IList<GarantiaBase> GetAllDeleted();        
        void SetInternalStatus(int garantiaId, InternalStatus internalStatus);
        List<GarantiaBaseRow> SearchGarantias(string SearchText);
        List<GarantiaBaseRow> SearchGarantiasPaged(string SearchText, int Page, int RecsPerPage);
        List<GarantiaBaseRow> GetAllUnknown(string SearchText);
        string DisableGuaranteeType(string UserId, string TipoGarantiaSuperId);
        string GetOriginalInternalStatus(string GarantiaId);
    }

    public interface IGarantiaBaseRepository<TEntity> : IRepository<TEntity> 
        where TEntity : GarantiaBase
    {
        bool ChangeType(TEntity garantia, CategoriaSuper newCategoriaSuper);        
        void WriteRecord(TEntity garantia);
    }
}