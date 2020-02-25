namespace ThreadsOfFate.ReadDomain.Specifications.UniversalSearch
{
    public class SpellSearchFilterSpecification
    {
        public string[] Elements { get; set; }
        public string SpellContainsName { get; set; }
        public int? ShortDistance { get; set; }
        public int? LongDistance { get; set; }
        public int? ManaCastCost { get; set; }
        public bool? IsDependFromSize { get; set; }
        public int? ManaCostMaintenance { get; set; }
        public string DescriptionContains { get; set; }
        public decimal? CastTimeMinutes { get; set; }
        public decimal? DuranceTimeMinutes { get; set; }
        public int? DamageBonus { get; set; }
    }
}
