using ThreadsofFate.Common.Queries;
using ThreadsOfFate.ReadDomain.Model;
using ThreadsOfFate.ReadDomain.Model.GlobalSearch;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions
{
    interface IGetElasticItemCollectionQuery : IQuery<GlobalSearchCollectionSpecification, CollectionWithCountersDto<GlobalSearchItemDto>>
    {
    }
}
