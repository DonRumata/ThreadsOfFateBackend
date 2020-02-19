using ThreadsofFate.Common.Specifications;
using ThreadsOfFate.ReadDomain.Specifications.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Abstractions.UniversalSearch;

namespace ThreadsOfFate.ReadDomain.Specifications.GlobalSearch.Search
{
    public class SearchCollectionSpecification : ISearch, ISkipTake, ISpecification
    {
        public string Search { get; set; }
        public bool ExactSearch { get; set; }
        public SearchFilterSpecification Filter { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
