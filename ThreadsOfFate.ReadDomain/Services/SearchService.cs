using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using ThreadsofFate.Common.Extensions;
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

            var elementGuids = specification?.Filter?.Spell?.Elements?.Select(el => new Guid(el)).ToArray();

            var items = await _searchProvider.SearchSpellsByText(specification.Search, elementGuids);

            var itemsCollection = HandleSpells(items, specification.Search);

            var cntrs = new CounterDto
            {
                Count = itemsCollection?.Count ?? 0,
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

        private ICollection<SearchItemDto> HandleSpells(SpellDto[] spells, string searchString)
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
                    Id = Guid.NewGuid().ToString(),
                    Content = searchContentItemSpell,
                    Hash = searchContentItemSpell.GetHashCode().ToString(),
                    Highlights = GetHighlights(spell, searchString),
                    ObjectType = "spell",
                    Source = "spell"
                };

                result.Add(addingSpellToSearchItem);
            }

            return result;
        }

        private string[] GetHighlights(SpellDto spell, string searchString)
        {
            var words = searchString.SplitToWordsBySpace();
            words = words.Select(w => w.StripSurrogates())
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();

            var highlights = words.FindAll(w => string.Equals(w.ToLower(), searchString.ToLower()));

            return highlights.ToArray();
        }
    }
}
