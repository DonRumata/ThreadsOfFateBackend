using System.Collections.Generic;
using Nest;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions
{
    interface IGlobalSearchQueryBuilderService
    {
        ICollection<QueryContainer> CreateQueries(GlobalSearchCollectionSpecification specification);
    }
}
