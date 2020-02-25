using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Model.Search;
using ThreadsOfFate.Domain.Dal.Model.Search.Filters;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions.Spell;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Filters;

namespace ThreadsOfFate.ReadDomain.Services
{
    class SearchFilterOptionsService: ServiceBase, ISearchFilterOptionsService
    {
        private readonly IGetSpellElementsFilterOptionsQuery _getSpellElementsFilterOptionsQuery;

        public SearchFilterOptionsService(IGetSpellElementsFilterOptionsQuery getSpellElementsFilterOptionsQuery)
        {
            _getSpellElementsFilterOptionsQuery = getSpellElementsFilterOptionsQuery;
        }

        public async Task<CollectionDto<FilterOptionDto>> GetSpellElements(SearchFilterOptionsSpecification specification)
        {
            var result = await _getSpellElementsFilterOptionsQuery.Ask(specification);
            return result;
        }

        
    }
}
