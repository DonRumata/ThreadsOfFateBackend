using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using ThreadsofFate.Common.Const;
using ThreadsOfFate.ReadDomain.Enums;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Services.Elastic
{
    class GlobalSearchQueryBuilderService : IGlobalSearchQueryBuilderService
    {
        private readonly Dictionary<GlobalSearchQueryType, (int, float)> _searchQueryTypeRankOrderMap;

        public GlobalSearchQueryBuilderService()
        {
            _searchQueryTypeRankOrderMap = new Dictionary<GlobalSearchQueryType, (int, float)>
            {
                { GlobalSearchQueryType.PersonAllProps, (4, 0f) },
                { GlobalSearchQueryType.PersonTwoProps, (4, 1f) },
                { GlobalSearchQueryType.FullPhrase,     (3, 2f) },
                { GlobalSearchQueryType.PersonOneProp,  (3, 3f) },
                { GlobalSearchQueryType.PartialPhrase,  (2, 4f) },
                { GlobalSearchQueryType.Word,           (1, 5f) }
            };
        }

        public ICollection<QueryContainer> CreateQueries(GlobalSearchCollectionSpecification specification)
        {
            var wordsCount = GetWordsCount(specification);

            if (wordsCount < 1)
                throw new InvalidOperationException("Пустой поисковый запрос.");

            var textQueriesWithOrder = GetTextQueries(specification);
            //var personQueriesWithOrder = GetPersonQueries(specification);

            return textQueriesWithOrder
                .OrderBy(o => o.Item2)
                .Select(o => o.Item1)
                .ToArray();
        }

        private ICollection<(QueryContainer, float)> GetTextQueries(GlobalSearchCollectionSpecification specification)
        {
            var queriesWithOrder = new List<(QueryContainer, float)>();
            var wordsCount = GetWordsCount(specification);

            //var isSearchExactMatch = IsSearchExactMatch(specification);

            // Если поисковый запрос содержит только одно слово, то создаём и возвращаем MatchQuery.
            if (wordsCount == 1)
            {
                queriesWithOrder.Add(CreateSingleWordTextQuery(specification));
                return queriesWithOrder;
            }

            // Если поисковый запрос содержит более одного слова, то имеет смысл поиск по фразе.
            queriesWithOrder.Add(CreateMatchPhraseTextQuery(specification));

            // Если явно указано "искать точное совпадение", то возвращаем созданный поисковый запрос.
            //if (isSearchExactMatch)
            //    return queriesWithOrder;

            // Если явно не указано "искать точное совпадение", то расширяем варианты поиска.
            queriesWithOrder.AddRange(CreatePartialPhraseTextQueryVariants(specification));

            return queriesWithOrder;
        }

        private ICollection<(QueryContainer, float)> CreatePartialPhraseTextQueryVariants(GlobalSearchCollectionSpecification specification)
        {
            var queries = new List<(QueryContainer, float)>();
            (var rank, var order) = _searchQueryTypeRankOrderMap[GlobalSearchQueryType.PartialPhrase];

            // вычисляем знаяения boost и order для всего набора вариантов.
            var wordsCount = GetWordsCount(specification);
            var highBoost = CalculateBoostValue(rank);
            var lowBoost = CalculateBoostValue(rank - 1);
            var boostStep = (highBoost - lowBoost) / wordsCount;
            var lowOrder = order - 1;
            var orderStep = (order - lowOrder) / wordsCount;

            var boostValue = highBoost;
            var orderValue = order;

            for (var i = wordsCount; i > 0; i--)
            {
                queries.Add(
                    (new MatchQuery
                        {
                            Field = GlobalSearchElasticConst.Text,
                            Query = specification.GlobalSearchText,
                            Operator = Operator.Or,
                            MinimumShouldMatch = new MinimumShouldMatch(i),
                            Boost = boostValue
                        },
                        orderValue)
                );

                boostValue -= boostStep;
                orderValue -= orderStep;
            }

            return queries;
        }

        private (QueryContainer, float) CreateMatchPhraseTextQuery(GlobalSearchCollectionSpecification specification)
        {
            (var rank, var order) = _searchQueryTypeRankOrderMap[GlobalSearchQueryType.FullPhrase];

            return (new MatchPhraseQuery
                {
                    Field = GlobalSearchElasticConst.Text,
                    Query = specification.GlobalSearchText,
                    Slop = 0,
                    Boost = CalculateBoostValue(rank),
                },
                order);
        }

        private (QueryContainer, float) CreateSingleWordTextQuery(GlobalSearchCollectionSpecification specification)
        {
            (var rank, var order) = _searchQueryTypeRankOrderMap[GlobalSearchQueryType.Word];

            return (new MatchQuery
                {
                    Field = GlobalSearchElasticConst.Text,
                    Query = specification.GlobalSearchText,
                    Boost = CalculateBoostValue(rank)
                },
                order);
        }

        private int GetWordsCount(GlobalSearchCollectionSpecification specification)
        {
            if (string.IsNullOrWhiteSpace(specification.GlobalSearchText))
                return 0;

            return specification.GlobalSearchText.Count(char.IsWhiteSpace) + 1;
        }

        private double CalculateBoostValue(int rank, double alpha = 1.0)
        {
            return Math.Pow(rank, 2) * alpha;
        }
    }
}
