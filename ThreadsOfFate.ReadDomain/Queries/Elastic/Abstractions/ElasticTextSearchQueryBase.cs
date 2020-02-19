using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using Nest;
using ThreadsofFate.Common.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;
using ThreadsOfFate.ReadDomain.Extensions.Elastic;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Specifications.GlobalSearch;

namespace ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions
{
    abstract class ElasticTextSearchQueryBase : ElasticMultiIndexQueryBase
    {
        protected const string TermsAggregationCounters = "counters";
        protected const string ThemesVector = "themesVector";

        private const string ConstThemes = "themes";

        protected ElasticTextSearchQueryBase(ILogger logger,
            IElasticSearchClient elastic,
            IElasticObjectTypeToAliasMapService elasticObjectTypeToAliasMapService,
            IElasticHealthService elasticHealthService)
            : base(logger, elastic, elasticObjectTypeToAliasMapService)
        {
            ElasticHealthService = elasticHealthService;
        }

        protected IElasticHealthService ElasticHealthService { get; }

        protected abstract QueryContainer CreateQuery(GlobalSearchCollectionSpecification specification);

        protected virtual QueryContainer CreatePostFilter(GlobalSearchCollectionSpecification specification)
        {
            // По умолчанию PostFilter отключен.
            return null;
        }

        protected SearchRequest CreateSearchRequestFromSpecification(GlobalSearchCollectionSpecification specification)
        {
            var query = CreateQuery(specification);

            //if (specification.HasSemanticThemes() && ElasticHealthService.CanUseThemeScripts())
            //    query = BuildScriptScoreQuery(specification.SemanticThemes, query);

            var request = new SearchRequest
            {
                Query = query,
                PostFilter = CreatePostFilter(specification),
                Highlight = GetTextHighlightQuery(),
                From = specification.Skip,
                Size = specification.Take,
                Aggregations = GetAggregationQuery(specification)
            };

            Debug.Write($"\nElasticSearchRequest:\n{Elastic.RequestToJson(request)}\n\n");

            return request;
        }

        protected virtual TermsAggregation GetAggregationQuery(GlobalSearchCollectionSpecification specification)
        {
            return new TermsAggregation(TermsAggregationCounters)
            {
                Field = "objectType",
                Size = IndexObjectTypeAliasMapService.TextSearchObjectTypeCount
            };
        }

        protected Highlight GetTextHighlightQuery()
        {
            return new Highlight
            {
                FragmentSize = 150,
                NumberOfFragments = 3,
                Fragmenter = HighlighterFragmenter.Span,
                Fields = new Dictionary<Field, IHighlightField>
                {
                    {
                        new Field("text"),
                        new HighlightField {Type = HighlighterType.Fvh}
                    }
                }
            };
        }

        //private QueryContainer BuildScriptScoreQuery(float[] semanticThemes, QueryContainer searchQuery)
        //{
        //    var functions = new List<IScoreFunction>();

        //    functions.Add(new ScriptScoreFunction
        //    {
        //        Script = new ScriptQuery
        //        {
        //            Source = BuildScriptSource(semanticThemes),
        //            Params = new Dictionary<string, object>
        //            {
        //                { ThemesVector, semanticThemes }
        //            }
        //        }
        //    });

        //    var funcQuery = new FunctionScoreQuery
        //    {
        //        Query = searchQuery,
        //        BoostMode = FunctionBoostMode.Multiply,
        //        Functions = functions
        //    };

        //    return funcQuery;
        //}

        //private string BuildScriptSource(float[] themes)
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("1");

        //    for (var i = 0; i < themes.Length; i++)
        //    {
        //        if (themes[i] > 0)
        //            sb.Append($"+(params.{ThemesVector}[{i}] * doc['{ConstThemes}'][{i}])");
        //    }

        //    return sb.ToString();
        //}
    }
}
