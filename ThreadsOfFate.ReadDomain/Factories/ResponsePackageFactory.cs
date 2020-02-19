using System.Linq;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;
using ThreadsOfFate.ReadDomain.Enums;
using ThreadsOfFate.ReadDomain.Factories.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;
using ThreadsOfFate.ReadDomain.Specifications.UniversalSearch;

namespace ThreadsOfFate.ReadDomain.Factories
{
    class ResponsePackageFactory : IResponsePackageFactory
    {
        private ISearchProvider _searchProvider;

        public ResponsePackageFactory(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        public void BuildFilteredQuery(SearchFilterSpecification specification)
        {
            var exactMatchObjectType = ObjectTypes.All;

            foreach (var obj in specification.ObjectType)
            {
                switch (obj.ToLower())
                {
                    case "spell":
                        exactMatchObjectType = ObjectTypes.Spell;
                        break;

                    default:
                        exactMatchObjectType = ObjectTypes.All;
                        break;
                }
            }
        }

        public void BuildFilterQuery(SearchFilterSpecification specification)
        {
            throw new System.NotImplementedException();
        }
    }
}
