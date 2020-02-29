using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Dto.Spell;

namespace ThreadsOfFate.Domain.Dal.ResponseDto.Spell
{
    [Serializable]
    public class SpellElementCountDto
    {
        public MagicElementDto Element { get; set; }
        public int ElementCount { get; set; }
    }
}
