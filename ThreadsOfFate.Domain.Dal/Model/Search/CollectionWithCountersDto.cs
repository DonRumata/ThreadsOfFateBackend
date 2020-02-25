using System;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;

namespace ThreadsOfFate.Domain.Dal.Model.Search
{
    public class CollectionWithCountersDto<TItem>:CollectionDto<TItem>
    {
        public CounterDto[] Counters { get; set; }

        public Guid QueryId { get; set; }
    }
}
