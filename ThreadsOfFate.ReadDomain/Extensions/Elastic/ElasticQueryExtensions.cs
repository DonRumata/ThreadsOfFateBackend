using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace ThreadsOfFate.ReadDomain.Extensions.Elastic
{
    static class ElasticQueryExtensions
    {
        public static void AddTermsQuery<TValue>(this ICollection<QueryContainer> queries, string fieldName, TValue[] filterValues)
        {
            if (filterValues?.Any() != true)
                return;

            if (filterValues.Length == 1)
            {
                queries.Add(new TermQuery { Field = fieldName, Value = filterValues.Single() });
            }
            else
            {
                queries.Add(new TermsQuery { Field = fieldName, Terms = filterValues.Cast<object>() });
            }
        }

        public static void AddDateRangeQuery(this ICollection<QueryContainer> queries, string fieldName, DateTime? startValue, DateTime? endValue)
        {
            if (startValue == null || endValue == null)
                return;

            queries.Add(new DateRangeQuery
            {
                Field = fieldName,
                GreaterThanOrEqualTo = startValue,
                LessThanOrEqualTo = endValue
            });
        }

        public static void AddMatchPhrasePrefixQuery(this ICollection<QueryContainer> queries, string fieldName, string searchValue, int? slop = null)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchValue))
                return;

            queries.Add(new MatchPhrasePrefixQuery
            {
                Field = fieldName,
                Query = searchValue,
                Slop = slop,
                //Analyzer = ElasticAnalyzerConst.SimpleAnalyzer
            });
        }

        public static void AddTermPrefixQuery(this ICollection<QueryContainer> queries, string fieldName, string searchValue)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchValue))
                return;

            queries.Add(new PrefixQuery
            {
                Field = fieldName,
                Value = searchValue
            });
        }

        public static void AddMatchPhraseQuery(this ICollection<QueryContainer> queries, string fieldName, string searchValue)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchValue))
                return;

            queries.Add(new MatchPhraseQuery
            {
                Field = fieldName,
                Query = searchValue
            });
        }

        /// <summary>
        /// Добавляет булево сложение фильтров типа MathcPhrasePrefix для нескольких полей индекса
        /// удобно когда нужно искать одновременно по нескольким полям
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="fields">Поля индекса</param>
        /// <param name="searchValue"></param>
        public static void AddBoolShouldMatchPhrasePrefixQueries(this ICollection<QueryContainer> queries, string[] fields, string searchValue)
        {
            if (!fields.Any())
                return;

            var matchPhraseQueries = new List<QueryContainer>();

            foreach (var field in fields)
            {
                matchPhraseQueries.AddMatchPhrasePrefixQuery(field, searchValue);
            }

            queries.Add(new BoolQuery
            {
                Should = matchPhraseQueries
            });
        }
    }
}
