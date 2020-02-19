using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions;
using ThreadsOfFate.Domain.Dal.Specifications;

namespace ThreadsOfFate.Domain.Dal.Queries
{
    internal class GetSpellDescriptionQuery : IGetSpellDescriptionQuery
    {
        private readonly IThreadsOfFateContext _context;

        public GetSpellDescriptionQuery(IThreadsOfFateContext context)
        {
            _context = context;
        }

        public async Task<SpellDto> Ask(GetSpellSpecification specification)
        {
            var result = await _context.Spells.AsNoTracking().Where(sp => sp.Id == specification.SpellId.Value)
                .SingleOrDefaultAsync().ConfigureAwait(false);

            return Mapper.Map<Spell, SpellDto>(result);
        }
    }
}
