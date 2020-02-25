using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell
{
    class MagicElementToSpell
    {
        public Guid SpellId { get; set; }
        public Guid MagicElementId { get; set; }
        public int MagicElementCount { get; set; }
    }
}
