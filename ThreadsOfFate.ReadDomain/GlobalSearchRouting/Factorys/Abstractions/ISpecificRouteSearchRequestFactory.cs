using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.GlobalSearchRouting.Factorys.Abstractions
{
    public interface ISpecificRouteSearchRequestFactory
    {
        Task<Task<GlobalSearchItemDto[]>> RouteSearchRequestToSpecificService(
            GlobalSearchCollectionSpecification specification);
    }
}
