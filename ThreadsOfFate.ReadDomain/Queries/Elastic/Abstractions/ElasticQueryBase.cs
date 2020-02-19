using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Nest;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions
{
    abstract class ElasticQueryBase
    {
        private readonly SearchType _searchType;
        private readonly bool _ignoreUnavailable;

        protected ElasticQueryBase(ILogger logger,
            IElasticSearchClient elastic,
            IElasticObjectTypeToAliasMapService elasticObjectTypeToAliasMapService,
            SearchType searchType,
            bool ignoreUnavailable)
        {
            _searchType = searchType;
            _ignoreUnavailable = ignoreUnavailable;
            Logger = logger;
            Elastic = elastic;
            IndexObjectTypeAliasMapService = elasticObjectTypeToAliasMapService;
        }

        protected IElasticSearchClient Elastic { get; }
        protected ILogger Logger { get; }
        protected IElasticObjectTypeToAliasMapService IndexObjectTypeAliasMapService { get; }

        protected async Task<ISearchResponse<TResult>> Search<TResult>(SearchRequest request, string specifiedIndexName = null)
            where TResult : class
        {
            request = BuildSearchRequest(request, specifiedIndexName);
            var searchResult = await Elastic.SearchAsync<TResult>(request).ConfigureAwait(false);

            if (!searchResult.IsValid)
            {
                Logger.LogError($"Ошибка Elastic (Search result is not valid): See the following debug information.\n{searchResult.DebugInformation}\n");
                throw new InvalidOperationException("Ошибка поиска в Elastic", searchResult.OriginalException);
            }

            if (searchResult.Shards.Failed > 0)
            {
                Logger.LogError($"Ошибка поиска в Elastic (Shards Failed): number of failed shards {searchResult.Shards.Failed} of {searchResult.Shards.Total} total shards. See the following debug information.\n{searchResult.DebugInformation}\n");
                throw new Exception("еба");
                //throw new ElasticShardFailureException<TResult>(searchResult);
            }

            return searchResult;
        }

        protected async Task<IGetResponse<TResult>> Get<TResult>(IGetRequest request)
            where TResult : class
        {
            var getResult = await Elastic.GetAsync<TResult>(request).ConfigureAwait(false);

            if (!getResult.IsValid)
            {
                Logger.LogError(getResult.DebugInformation);
                throw new InvalidOperationException("Ошибка поиска в Elastic", getResult.OriginalException);
            }

            return getResult;
        }

        private SearchRequest BuildSearchRequest(SearchRequest request, string specifiedIndexName)
        {
            var searchIndexName = specifiedIndexName ?? IndexObjectTypeAliasMapService.GetTextSearchIndexName();

            return new SearchRequest(searchIndexName, "globalSearch")
            {
                Query = request.Query,
                PostFilter = request.PostFilter,
                Highlight = request.Highlight,
                From = request.From,
                Size = request.Size,
                Aggregations = request.Aggregations,
                SearchType = _searchType,
                IgnoreUnavailable = _ignoreUnavailable
            };
        }
    }
}
