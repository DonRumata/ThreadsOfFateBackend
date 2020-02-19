using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Queries;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Specifications;

namespace ThreadsOfFate.Domain.Dal.Queries.Abstractions
{
    public interface IGetSpellDescriptionQuery : IQuery<GetSpellSpecification, SpellDto>
    {
    }
}
