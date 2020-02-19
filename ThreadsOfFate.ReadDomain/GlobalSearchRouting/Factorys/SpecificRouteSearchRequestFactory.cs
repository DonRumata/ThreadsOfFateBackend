using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.ReadDomain.GlobalSearchRouting.Factorys.Abstractions;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.GlobalSearchRouting.Factorys
{
    class SpecificRouteSearchRequestFactory : ISpecificRouteSearchRequestFactory
    {
        private readonly ISpecificSpecificationFactory _specificSpecificationFactory;

        public SpecificRouteSearchRequestFactory(ISpecificSpecificationFactory specificSpecificationFactory)
        {
            _specificSpecificationFactory = specificSpecificationFactory;
        }

        public Task<Task<GlobalSearchItemDto[]>> RouteSearchRequestToSpecificService(GlobalSearchCollectionSpecification specification)
        {
            return null;
        }
    }
}
