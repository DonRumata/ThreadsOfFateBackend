using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic
{
    class GetElasticItemQuery : ElasticMultiIndexQueryBase, IGetElasticItemQuery
    {
        public GetElasticItemQuery(ILogger<GetElasticItemQuery> logger,
            IElasticSearchClient elastic,
            IElasticObjectTypeToAliasMapService elasticObjectTypeToAliasMapService)
            : base(logger, elastic, elasticObjectTypeToAliasMapService)
        {
        }

        public async Task<GlobalSearchItemDto> Ask(GetGlobalSearchItemSpecification specification)
        {
            var request = new SearchRequest
            {
                Query = new TermQuery
                {
                    Field = "_id",
                    Value = specification.Id
                }
            };

            var searchResult = await Search<GlobalSearchItemDto>(request).ConfigureAwait(false);
            return searchResult.Documents.SingleOrDefault();
        }
    }
}
