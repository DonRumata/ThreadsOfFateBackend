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
    class SpellProvider : WritableProviderBase, ISpellProvider
    {
        public SpellProvider(ILogger<SpellProvider> logger, IThreadsOfFateContext context) : base(logger, context)
        {
        }

        public async Task<SpellDto[]> GetSpellsByName(string spellName)
        {
            var result = await Context.Spells.AsNoTracking()
                .Where(sp => sp.SpellName.ToLower() == spellName.ToLower())
                .ToArrayAsync()
                .ConfigureAwait(false);

            return result.Select(res => Mapper.Map<Spell, SpellDto>(res)).ToArray();
        }

        public async Task<SpellDto> GetSpellByUid(Guid spellUid)
        {
            var result = await Context.Spells.AsNoTracking()
                .Where(sp => sp.Id == spellUid).SingleOrDefaultAsync().ConfigureAwait(false);

            return Mapper.Map<Spell, SpellDto>(result);
        }
    }
}
