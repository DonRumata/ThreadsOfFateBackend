using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions;
using ThreadsOfFate.Domain.Dal.Specifications;
using ThreadsOfFate.ReadDomain.Services.Abstractions;

namespace ThreadsOfFate.ReadDomain.Services
{
    internal class SpellService : ServiceBase, ISpellService
    {
        private readonly ISpellProvider _spellProvider;
        private readonly IGetSpellDescriptionQuery _spellQuery;

        public SpellService(ISpellProvider spellProvider, IGetSpellDescriptionQuery spellQuery)
        {
            _spellProvider = spellProvider;
            _spellQuery = spellQuery;
        }

        public async Task<SpellDto> GetSpell(GetSpellSpecification specification)
        {
            return await _spellProvider.GetSpellByUid(specification.SpellId.Value).ConfigureAwait(false);
        }

        public async Task<SpellDto[]> GetSpellByName(string spellName)
        {
            return await _spellProvider.GetSpellsByName(spellName).ConfigureAwait(false);
        }
    }
}
