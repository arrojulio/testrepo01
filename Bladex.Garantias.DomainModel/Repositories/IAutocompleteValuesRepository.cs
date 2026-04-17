using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bladex.Garantias.DomainModel.DomainBase;
using Bladex.Garantias.DomainModel.DomainBase.Components.Autocomplete;
using Bladex.Garantias.Infrastructure.RepositoryFramework;

namespace Bladex.Garantias.DomainModel.Repositories
{
    public interface IAutocompleteValueRepository : IRepository<AutocompleteValue>
    {
        /// <summary>
        /// Gets the autocomplete values grouped by the html control id
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <returns></returns>
        IList<AutocompleteValue> GetByHtmlControlId(string htmlControlId);

        /// <summary>
        /// Gets the autocomplete unique value 
        /// </summary>
        /// <param name="htmlControlId">The HTML control id of type <see cref="System.String"/></param>
        /// <param name="value">The value of type <see cref="System.String"/></param>
        /// <returns></returns>
        DomainModel.DomainBase.Components.Autocomplete.AutocompleteValue GetByHtmlControlIdAndValue(string htmlControlId, string value);
    }
}
