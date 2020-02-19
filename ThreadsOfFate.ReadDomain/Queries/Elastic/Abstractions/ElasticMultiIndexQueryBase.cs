using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions
{
    abstract class ElasticMultiIndexQueryBase : ElasticQueryBase
    {
        protected ElasticMultiIndexQueryBase(ILogger logger,
            IElasticSearchClient elastic,
            IElasticObjectTypeToAliasMapService elasticObjectTypeToAliasMapService)
            : base(logger, elastic, elasticObjectTypeToAliasMapService, SearchType.DfsQueryThenFetch, true)
        { }
    }
}
