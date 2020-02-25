using ThreadsofFate.Common.Specifications;
using ThreadsOfFate.Domain.Dal.Specifications.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Abstractions;

namespace ThreadsOfFate.ReadDomain.Specifications.GlobalSearch
{
    public class GlobalSearchCollectionSpecification : IGlobalSearch, ISkipTake, ISpecification
    {
        public string GlobalSearchText { get; set; }
        public bool ExactSearch { get; set; }
        public GlobalSearchFilterSpecification Filter { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
