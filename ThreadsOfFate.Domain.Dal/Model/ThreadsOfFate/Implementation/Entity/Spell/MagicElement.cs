using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell
{
    class MagicElement
    {
        public Guid Id { get; set; }
        public string ElementName { get; set; }

        public ICollection<MagicElementToSpell> MagicElementToSpellRef { get; set; }
    }
}
