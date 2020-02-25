using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Queries;
using ThreadsOfFate.Domain.Dal.Model.Search;
using ThreadsOfFate.Domain.Dal.Model.Search.Filters;
using ThreadsOfFate.ReadDomain.Specifications.Filters;

namespace ThreadsOfFate.Domain.Dal.Queries.Abstractions
{
    public interface IFilterOptionsQuery : IQuery<SearchFilterOptionsSpecification, CollectionDto<FilterOptionDto>>
    {
    }
}
