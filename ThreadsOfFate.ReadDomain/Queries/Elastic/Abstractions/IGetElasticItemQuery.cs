using System;
using System.Collections.Generic;
using System.Text;
using ThreadsofFate.Common.Queries;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions
{
    interface IGetElasticItemQuery : IQuery<GetGlobalSearchItemSpecification, GlobalSearchItemDto>
    {
    }
}
