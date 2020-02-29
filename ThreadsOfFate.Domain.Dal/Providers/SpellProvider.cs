using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Mapping;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;
using ThreadsOfFate.Domain.Dal.ResponseDto.Spell;

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

        public async Task<SpellExtDto> GetSpellByUid(Guid spellUid)
        {
            var spell = await Context.Spells.AsNoTracking()
                .Where(sp => sp.Id == spellUid)
                .Select(sp => new {
                    sp.Id,
                    sp.SpellName,
                    sp.CastTimeMinutes,
                    sp.DamageBonus,
                    sp.DuranceTimeMinutes,
                    sp.FullDescription,
                    sp.IsDependFromSize,
                    sp.LongDistance,
                    sp.ManaCastCost,
                    sp.ManaCostMaintenance,
                    sp.ShortDescription,
                    sp.ShortDistance,
                    elements = sp.MagicElementToSpellRef
                        .Select(mts => new {
                            mts.MagicElementCount,
                            mts.MagicElementRef.ElementName,
                            mts.MagicElementId
                        }).ToArray()
                }).FirstOrDefaultAsync().ConfigureAwait(false);

            var spellMainData = new SpellDto
            {
                Id = spell.Id,
                LongDistance = spell.LongDistance,
                ShortDistance = spell.ShortDistance,
                ShortDescription = spell.ShortDescription,
                ManaCostMaintenance = spell.ManaCostMaintenance,
                CastTimeMinutes = spell.CastTimeMinutes,
                ManaCastCost = spell.ManaCastCost,
                FullDescription = spell.FullDescription,
                DuranceTimeMinutes = spell.DuranceTimeMinutes,
                DamageBonus = spell.DamageBonus,
                IsDependFromSize = spell.IsDependFromSize,
                SpellName = spell.SpellName
            };

            var resultList = new List<SpellElementCountDto>();

            foreach (var spellElement in spell.elements)
            {
                resultList.Add(new SpellElementCountDto
                {
                    Element = new MagicElementDto
                    {
                        ElementName = spellElement.ElementName,
                        Id = spellElement.MagicElementId
                    },
                    ElementCount = spellElement.MagicElementCount
                });
            }

            var result = new SpellExtDto
            {
               SpellMainData = spellMainData,
               Elements = resultList.ToArray()
            };

            return result;
        }
    }
}
