using Nest;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions
{
    interface IBuildFilterQueryService
    {
        QueryContainer BuildFilterQuery(GlobalSearchCollectionSpecification specification);
    }
}
