using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.ReadDomain.Model.GlobalSearch
{
    public class GlobalSearchContentSpellDto : GlobalSearchContentEntryDto
    {
        private SpellDto Fields { get; set; }
    }
}
