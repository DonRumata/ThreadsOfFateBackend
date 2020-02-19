using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.Domain.Dal.Providers.Abstractions
{
    public interface ISpellProvider : IWritableProvider
    {
        Task<SpellDto[]> GetSpellsByName(string spellName);
        Task<SpellDto> GetSpellByUid(Guid spellUid);
    }
}
