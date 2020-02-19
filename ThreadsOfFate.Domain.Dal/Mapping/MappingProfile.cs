using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.Domain.Dal.Dto.Spell;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Spell, SpellDto>();
            CreateMap<SpellDto, SearchSpellItemDto>();
        }
    }
}
