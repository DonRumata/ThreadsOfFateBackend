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

        public async Task<SpellDto[]> SearchSpellsByText(string searchText, Guid[] ElementsId)
        {
            var result = await (from sp in Context.Spells.AsNoTracking()
                    join ets in Context.MagicElementsToSpells.AsNoTracking() on sp.Id equals ets.SpellId
                    join el in Context.Elements.AsNoTracking() on ets.MagicElementId equals el.Id
                    where ((sp.SpellName.Contains(searchText) || sp.SpellName.Contains(searchText) || string.IsNullOrWhiteSpace(searchText))
                           && (ElementsId == null || ElementsId.Length < 1 || ElementsId.Contains(el.Id)))
                    select sp)
                .Distinct()
                .ToArrayAsync()
                .ConfigureAwait(false);

            return result.Select(res => Mapper.Map<Spell, SpellDto>(res)).ToArray();
        }
    }
}
