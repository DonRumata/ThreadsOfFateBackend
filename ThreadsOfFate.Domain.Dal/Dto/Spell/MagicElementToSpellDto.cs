using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.Domain.Dal.Dto.Spell
{
    public class MagicElementToSpellDto
    {
        public Guid SpellId { get; set; }
        public Guid MagicElementId { get; set; }
        public int MagicElementCount { get; set; }
    }
}
