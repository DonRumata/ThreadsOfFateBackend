using ThreadsOfFate.ReadDomain.Specifications.UniversalSearch;

namespace ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search
{
    public class SearchFilterSpecification
    {
        public string[] ObjectType { get; set; }
        public bool SearchExactMatch { get; set; }
        public bool UseFilter { get; set; }
        public SpellSearchFilterSpecification Spell { get; set; }
    }
}
