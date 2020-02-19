using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Mapping;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;
using ThreadsOfFate.ReadDomain.Extensions;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search;

namespace ThreadsOfFate.ReadDomain.Services
{
    internal class SearchService : ServiceBase, ISearchService
    {

        private readonly ISearchProvider _searchProvider;

        public SearchService(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        public async Task<CollectionWithCountersDto<SearchItemDto>> SearchAll(SearchCollectionSpecification specification)
        {
            CheckSpecificationIsNotNullOrThrow(specification);
            specification.AdjustSkipTake();
            specification.NormalizeQuery();

            var items = await _searchProvider.SearchSpellsByText(specification.Search);

            var itemsCollection = HandleSpells(items);

            var cntrs = new CounterDto
            {
                Count = itemsCollection.Count,
                Key = "spell"
            };

            var cntrsList = new List<CounterDto>();
            cntrsList.Add(cntrs);


            var result = new CollectionWithCountersDto<SearchItemDto>
            {
                Items = itemsCollection,
                Count = itemsCollection.Count,
                Counters = cntrsList.ToArray()
            };

            return result;
        }

        private ICollection<SearchItemDto> HandleSpells(SpellDto[] spells)
        {
            var result = new List<SearchItemDto>();

            if (spells == null || spells.Length < 1)
                return null;

            foreach (var spell in spells)
            {
                
                var searchContentItemSpell = new SearchContentItemDto
                {
                    Spell = Mapper.Map<SpellDto, SearchSpellItemDto>(spell)
                };

                var addingSpellToSearchItem = new SearchItemDto
                {
                    Content = searchContentItemSpell,
                    Id = Guid.NewGuid().ToString(),
                    ObjectType = "spell"
                };

                result.Add(addingSpellToSearchItem);
            }

            return result;
        }
    }
}
