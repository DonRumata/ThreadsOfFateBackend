using System;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.Domain.Dal.ResponseDto.Spell
{
    [Serializable]
    public class SpellExtDto
    {
        public SpellDto SpellMainData { get; set; }
        public SpellElementCountDto[] Elements { get; set; }
    }
}
