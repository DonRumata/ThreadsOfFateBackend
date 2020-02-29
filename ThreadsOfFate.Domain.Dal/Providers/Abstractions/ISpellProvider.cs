using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.ResponseDto.Spell;

namespace ThreadsOfFate.Domain.Dal.Providers.Abstractions
{
    public interface ISpellProvider : IWritableProvider
    {
        Task<SpellDto[]> GetSpellsByName(string spellName);
        Task<SpellExtDto> GetSpellByUid(Guid spellUid);
    }
}
