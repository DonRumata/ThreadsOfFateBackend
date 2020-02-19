using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.Domain.Dal.Dto.SearchStructure;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;
using ThreadsOfFate.ReadDomain.Extensions;
using ThreadsOfFate.ReadDomain.Extensions.Elastic;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic
{
    class GetElasticItemCollectionQuery : ElasticTextSearchQueryBase, IGetElasticItemCollectionQuery
    {
        private readonly IBuildFilterQueryService _buildFilterQueryService;
        private readonly IGlobalSearchQueryBuilderService _searchQueryBuilderService;

        public GetElasticItemCollectionQuery(ILogger<string> logger,
           IElasticSearchClient elastic,
           IElasticObjectTypeToAliasMapService indexObjectTypeAliasMapService,
           IElasticHealthService elasticHealthService,
           IBuildFilterQueryService buildFilterQueryService,
           IGlobalSearchQueryBuilderService searchQueryBuilderService)
           : base(logger, elastic, indexObjectTypeAliasMapService, elasticHealthService)
        {
            _buildFilterQueryService = buildFilterQueryService;
            _searchQueryBuilderService = searchQueryBuilderService;
        }

        public async Task<CollectionWithCountersDto<GlobalSearchItemDto>> Ask(GlobalSearchCollectionSpecification specification)
        {
            ISearchResponse<GlobalSearchItemDto> response;

            try
            {
                response = await Search(specification).ConfigureAwait(false);
            }
            catch (Exception ex)
            // Если упала шарда, то принимаем решение, в зависимости от полученных данных.
            {
                //// Если произошла ошибка в скоринговом скрипте,
                //// то можем отключить темы (они используют скрипты) и перевыполнить запрос.
                //if (ElasticHealthService.HasShardScriptError(ex.Response.Shards))
                //{
                //    ElasticHealthService.SuspendUseThemeScripts();
                //    // Результаты повторного поиска не анализируем, принимаем как есть.
                //    response = await Search(specification).ConfigureAwait(false);
                //}
                //// Если упали не все shard, то не блокируем сервис, отдаём, что есть.
                //else if (ex.Response.Shards.Successful > 0)
                //{
                //    response = ex.Response;
                //}
                //else
                //{
                //    throw new InvalidOperationException("Ошибка поиска в Elastic (All Shards Failed), see inner exception for details.", ex);
                //}

                throw new InvalidOperationException();
            }

            var aggregations = response?.Aggregations.Terms(TermsAggregationCounters);

            return new CollectionWithCountersDto<GlobalSearchItemDto>
            {
                Items = GetDocuments(response),
                Count = (int)(response?.Total ?? 0),
                Counters = GetCounters(aggregations)
            };
        }

        protected override QueryContainer CreateQuery(GlobalSearchCollectionSpecification specification)
        {
            var searchQuery = BuildTextSearchQuery(specification);
            var filterQuery = _buildFilterQueryService.BuildFilterQuery(specification);

            return new BoolQuery
            {
                Must = new[] { searchQuery },
                Filter = new[] { filterQuery }
            };
        }

        protected override QueryContainer CreatePostFilter(GlobalSearchCollectionSpecification specification)
        {
            var filter = specification.Filter;

            // Если в запросе нет фильтрации, то фильтрация не требуется.
            if (filter == null)
                return null;

            // Если в запросе не указаны типы объектов, то фильтрация не требуется.
            if (filter.ObjectType?.Any() != true)
                return null;

            // Если в запросе указаны все типы объектов, то фильтрация не требуется.
            if (IndexObjectTypeAliasMapService.AreAllSearchObjectTypes(filter.ObjectType))
                return null;

            var queries = new List<QueryContainer>();

            // Фильтр документов по их типам.
            AddObjectTypeFilterQuery(filter, queries);

            return new BoolQuery
            {
                Filter = queries
            };
        }

        private async Task<ISearchResponse<GlobalSearchItemDto>> Search(GlobalSearchCollectionSpecification specification)
        {
            var request = CreateSearchRequestFromSpecification(specification);
            return await Search<GlobalSearchItemDto>(request).ConfigureAwait(false);
        }

        private void AddObjectTypeFilterQuery(GlobalSearchFilterSpecification filter, ICollection<QueryContainer> queries)
        {
            if (filter?.ObjectType?.Any() != true)
                return;

            IndexObjectTypeAliasMapService.CheckTextSearchObjectTypeOrThrow(filter.ObjectType);
            queries.AddTermsQuery("objectType", filter.ObjectType);
        }

        private QueryContainer BuildTextSearchQuery(GlobalSearchCollectionSpecification specification)
        {
            if (!specification.HasSearch())
                return new MatchAllQuery();

            var queries = _searchQueryBuilderService.CreateQueries(specification);

            return new BoolQuery
            {
                Should = queries,
                MinimumShouldMatch = 1
            };
        }

        protected override TermsAggregation GetAggregationQuery(GlobalSearchCollectionSpecification specification)
        {
            var aggregations = new TermsAggregation(TermsAggregationCounters)
            {
                Field = "objectType",
                Size = IndexObjectTypeAliasMapService.TextSearchObjectTypeCount
            };

            return aggregations;
        }

        private ICollection<GlobalSearchItemDto> GetDocuments(ISearchResponse<GlobalSearchItemDto> searchResult)
        {
            if (searchResult == null)
                return Array.Empty<GlobalSearchItemDto>();

            var list = new List<GlobalSearchItemDto>();
            AddDocumentsToCollection(searchResult.Hits, list);

            return list;
        }

        private void AddDocumentsToCollection(IReadOnlyCollection<IHit<GlobalSearchItemDto>> hits, ICollection<GlobalSearchItemDto> list)
        {
            foreach (var hit in hits)
            {
                var doc = hit.Source;
                //doc.Highlights = (hit.Highlights.ContainsKey(IndexFieldNameConst.Text) ? hit.Highlights[IndexFieldNameConst.Text]?.Highlights?.ToArray() : null)
                //                 ?? Array.Empty<string>();
                list.Add(doc);
            }
        }

        private CounterDto[] GetCounters(TermsAggregate<string> aggregations)
        {
            if (aggregations == null)
                return Array.Empty<CounterDto>();

            return aggregations.Buckets
                .Select(b => new CounterDto { Key = b.Key, Count = b.DocCount ?? 0 })
                .ToArray();
        }
    }
}
