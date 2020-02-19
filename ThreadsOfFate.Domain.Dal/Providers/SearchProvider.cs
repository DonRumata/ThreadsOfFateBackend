using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Mapping;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Providers
{
    class SearchProvider : WritableProviderBase, ISearchProvider
    {
        public SearchProvider(ILogger<SearchProvider> logger, IThreadsOfFateContext context) : base(logger, context)
        {
        }

        public async Task<SpellDto[]> SearchSpellsByText(string searchText)
        {
            var searchLowerText = searchText.ToLower();

            var result = await Context.Spells.AsNoTracking()
                .Where(sp =>
                    sp.SpellName.ToLower().Contains(searchLowerText) ||
                    sp.FullDescription.ToLower().Contains(searchLowerText))
                .ToArrayAsync()
                .ConfigureAwait(false);


            return result.Select(res => Mapper.Map<Spell, SpellDto>(res)).ToArray();
        }
    }
}
