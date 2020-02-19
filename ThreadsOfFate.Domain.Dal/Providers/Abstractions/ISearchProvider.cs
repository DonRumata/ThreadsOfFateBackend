using System.Threading.Tasks;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.Domain.Dal.Providers.Abstractions
{
    public interface ISearchProvider
    {
        Task<SpellDto[]> SearchSpellsByText(string searchText);
    }
}
