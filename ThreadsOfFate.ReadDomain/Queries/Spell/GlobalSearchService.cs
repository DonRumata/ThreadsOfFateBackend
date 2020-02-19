using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using ThreadsOfFate.ReadDomain.Extensions;
using ThreadsOfFate.ReadDomain.GlobalSearchRouting.Factorys.Abstractions;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.Elastic;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Spell
{
    class GlobalSearchService : ServiceBase, IGlobalSearchService
    {
        private readonly ISpecificRouteSearchRequestFactory _specificRouteSearchRequestFactory;

        private readonly IGetElasticItemCollectionQuery _allCollectionQuery;
        private readonly IGetElasticItemQuery _itemQuery;

        public GlobalSearchService(ISpecificRouteSearchRequestFactory specificRouteSearchRequestFactory, 
            IGetElasticItemCollectionQuery allCollectionQuery, 
            IGetElasticItemQuery itemQuery)
        {
            _specificRouteSearchRequestFactory = specificRouteSearchRequestFactory;
            _allCollectionQuery = allCollectionQuery;
        }

        public async Task<CollectionWithCountersDto<GlobalSearchItemDto>> Ask(GlobalSearchCollectionSpecification specification)
        {
            GlobalSearchItemDto[] searchResult;

            try
            {
                searchResult = await Search(specification).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка поиска", ex);
            }

            return null;
        }

        private async Task<GlobalSearchItemDto[]> Search(GlobalSearchCollectionSpecification specification)
        {
            return null;
            //var request = RouteSearchRequestToSpecificService(specification);
        }

        //private async Task<GlobalSearchItemDto[]> RouteSearchRequestToSpecificService(GlobalSearchCollectionSpecification specification)
        //{
        //    if (specification?.Filter?.ObjectType == null || specification.Filter.ObjectType.Length < 1)
        //        throw new ArgumentException("Спецификация пуста или не содержит данных по фильтрации");


        //}

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

            var data = await GlobalSearchParallel(specification).ConfigureAwait(false);

            data.QueryId = Guid.NewGuid();

            return data;
        }

        private async Task<CollectionWithCountersDto<GlobalSearchItemDto>> GlobalSearchParallel(
            GlobalSearchCollectionSpecification specification)
        {
            var tasks = _allCollectionQuery.Ask(specification);
            await Task.WhenAll(tasks).ConfigureAwait(false);

            return tasks.Result;
        }
    }
}
