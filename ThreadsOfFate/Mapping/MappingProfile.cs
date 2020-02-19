using AutoMapper;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;
using ThreadsOfFate.ReadDomain.Specifications.UniversalSearch;
using ThreadsOfFate.Requests.GlobalSearch;
using ThreadsOfFate.Requests.UniversalSearch;

namespace ThreadsOfFate.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            InitializeGlobalSearchMap();
        }

        private void InitializeGlobalSearchMap()
        {
            CreateMap<GlobalSearchQuery, GlobalSearchCollectionSpecification>();
            CreateMap<SearchQuery, SearchCollectionSpecification>();
            CreateMap<SpellSearchQueryFilter, SpellSearchFilterSpecification>();
            CreateMap<SearchQueryFilter, SearchFilterSpecification>();
        }
    }
}
