using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Abstractions
{
    public interface IGlobalSearchService
    {
        Task<GlobalSearchContentItemDto> GetItem(GetGlobalSearchItemSpecification specification);
        Task<CollectionWithCountersDto<GlobalSearchItemDto>> GlobalSearchFind(GlobalSearchCollectionSpecification specification);
    }
}
