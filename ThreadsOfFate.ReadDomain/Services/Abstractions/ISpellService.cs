using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.ResponseDto.Spell;
using ThreadsOfFate.Domain.Dal.Specifications;

namespace ThreadsOfFate.ReadDomain.Services.Abstractions
{
    public interface ISpellService
    {
        Task<SpellExtDto> GetSpell(GetSpellSpecification specification);
        Task<SpellDto[]> GetSpellByName(string spellName);
    }
}
