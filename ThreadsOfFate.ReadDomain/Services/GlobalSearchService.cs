using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Extensions;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Services
{
    class GlobalSearchService : ServiceBase, IGlobalSearchService
    {
        private readonly IGetElasticItemQuery _itemQuery;
        private readonly IGetElasticItemCollectionQuery _allCollectionQuery;
        //private readonly ILoggingService _loggingService;
        //private readonly ISearchAddonsService _searchAddonsService;
        private readonly IElasticObjectTypeToAliasMapService _elasticObjectTypeToAliasMapService;

        public GlobalSearchService(
            IGetElasticItemQuery itemQuery,
            IGetElasticItemCollectionQuery allCollectionQuery,
            IElasticObjectTypeToAliasMapService elasticObjectTypeToAliasMapService
            )
        {
            _itemQuery = itemQuery;
            _allCollectionQuery = allCollectionQuery;
            _elasticObjectTypeToAliasMapService = elasticObjectTypeToAliasMapService;
        }

        public async Task<GlobalSearchContentItemDto> GetItem(GetGlobalSearchItemSpecification specification)
        {
            CheckSpecificationIsNotNullOrThrow(specification);

            var item = await _itemQuery.Ask(specification).ConfigureAwait(false);

            return item?.Content;
        }

        public async Task<CollectionWithCountersDto<GlobalSearchItemDto>> GlobalSearchFind(GlobalSearchCollectionSpecification specification)
        {
            CheckSpecificationIsNotNullOrThrow(specification);
            specification.AdjustSkipTake();
            specification.AdjustSearchForElastic();


            var queryId = Guid.NewGuid();

            var data = await SearchParallel(specification).ConfigureAwait(false);

            //data.RepairedQuery = await GetRepairedSearchQuery(specification).ConfigureAwait(false);
            data.QueryId = queryId;

            return data;
        }

        private async Task<CollectionWithCountersDto<GlobalSearchItemDto>> SearchParallel(
            GlobalSearchCollectionSpecification specification)
        {
            var searchTask = _allCollectionQuery.Ask(specification);
            //var addonsTask = GetAddonsFindTask(specification);
            await Task.WhenAll(searchTask).ConfigureAwait(false);

            return (searchTask.Result);
        }
    }
}
