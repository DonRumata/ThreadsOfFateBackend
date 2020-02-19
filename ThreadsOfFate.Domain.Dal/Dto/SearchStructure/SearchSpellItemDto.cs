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
        public int? ShortDistance { get; set; }
        public int? ManaCastCost { get; set; }
        public bool IsDependFromSize { get; set; }
        public int? ManaCostMaintenance { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal? CastTimeMinutes { get; set; }
        public decimal? DuranceTimeMinutes { get; set; }
        public int? LongDistance { get; set; }
        public int? DamageBonus { get; set; }
    }
}
