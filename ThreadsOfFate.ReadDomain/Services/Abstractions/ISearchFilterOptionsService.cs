using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Model.Search;
using ThreadsOfFate.Domain.Dal.Model.Search.Filters;
using ThreadsOfFate.ReadDomain.Specifications.Filters;

namespace ThreadsOfFate.ReadDomain.Services.Abstractions
{
    public interface ISearchFilterOptionsService
    {
        Task<CollectionDto<FilterOptionDto>> GetSpellElements(SearchFilterOptionsSpecification specification);
    }
}
