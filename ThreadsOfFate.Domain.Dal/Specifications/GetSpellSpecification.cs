using System;
using ThreadsofFate.Common.Specifications;

namespace ThreadsOfFate.Domain.Dal.Specifications
{
    public class GetSpellSpecification: ISpecification
    {
        public Guid? SpellId { get; }

        public GetSpellSpecification(Guid spellId)
        {
            SpellId = spellId;
        }
    }
}
