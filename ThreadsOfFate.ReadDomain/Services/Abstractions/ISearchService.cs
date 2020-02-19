using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;

namespace ThreadsOfFate.ReadDomain.Services.Abstractions
{
    public interface ISearchService
    {
        Task<CollectionWithCountersDto<SearchItemDto>> SearchAll(SearchCollectionSpecification specification);
    }
}
