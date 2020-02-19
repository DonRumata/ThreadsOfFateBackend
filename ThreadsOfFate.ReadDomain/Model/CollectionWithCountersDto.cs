using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;

namespace ThreadsOfFate.ReadDomain.Model
{
    public class CollectionWithCountersDto<TItem>:CollectionDto<TItem>
    {
        public CounterDto[] Counters { get; set; }

        public Guid QueryId { get; set; }
    }
}
