using System;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.Domain.Dal.Dto.SearchStructure
{
    [Serializable]
    public class SearchContentItemDto
    {
        public SearchSpellItemDto Spell { get; set; }
    }
}
