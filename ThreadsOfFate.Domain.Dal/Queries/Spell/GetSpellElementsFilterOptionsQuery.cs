using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Model.Search;
using ThreadsOfFate.Domain.Dal.Model.Search.Filters;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions.Spell;
using ThreadsOfFate.ReadDomain.Specifications.Filters;

namespace ThreadsOfFate.Domain.Dal.Queries.Spell
{
    class GetSpellElementsFilterOptionsQuery : IGetSpellElementsFilterOptionsQuery
    {
        private readonly IThreadsOfFateContext _context;

        public GetSpellElementsFilterOptionsQuery(IThreadsOfFateContext context)
        {
            _context = context;
        }

        public async Task<CollectionDto<FilterOptionDto>> Ask(SearchFilterOptionsSpecification specification)
        {
            var query = await (from sp in _context.Spells.AsNoTracking()
                    join ets in _context.MagicElementsToSpells.AsNoTracking()
                        on sp.Id equals ets.SpellId
                    join el in _context.Elements.AsNoTracking()
                        on ets.MagicElementId equals el.Id
                    select el)
                .Distinct()
                .OrderBy(q => q.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);

            if (query.Length == 0)
                return null;

            var result = GetOptions(query);
            return new CollectionDto<FilterOptionDto>
            {
                Items = SkipTakeOptions(specification, result),
                Count = result.Length
            };
        }

        private FilterOptionDto[] GetOptions(MagicElement[] elements)
        {
            return elements.Select(id => new FilterOptionDto
            {
                Id = id.Id.ToString(),
                Value = id.ElementName
            }).ToArray();
        }

        private ICollection<FilterOptionDto> SkipTakeOptions(SearchFilterOptionsSpecification specification, FilterOptionDto[] options)
        {
            if (specification.OptionsSkip == null || specification.OptionsTake == null)
                return options;

            return options
                .Skip(specification.OptionsSkip.Value)
                .Take(specification.OptionsTake.Value)
                .ToArray();
        }
    }
}
