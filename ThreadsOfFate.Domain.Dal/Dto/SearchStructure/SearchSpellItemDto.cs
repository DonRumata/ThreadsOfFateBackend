using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Dto.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Dto.SearchStructure
{
    [Serializable]
    public class SearchSpellItemDto : SearchContentEntryDto
    {
        //public Guid Id { get; set; }
        public string SpellName { get; set; }
        public string FullDescription { get; set; }
        public string[] Elements { get; set; }
    }
}
