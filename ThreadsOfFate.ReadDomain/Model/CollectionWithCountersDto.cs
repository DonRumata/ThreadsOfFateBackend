﻿using System;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.Domain.Dal.Model.Search;

namespace ThreadsOfFate.ReadDomain.Model
{
    public class CollectionWithCountersDto<TItem>:CollectionDto<TItem>
    {
        public CounterDto[] Counters { get; set; }

        public Guid QueryId { get; set; }
    }
}
