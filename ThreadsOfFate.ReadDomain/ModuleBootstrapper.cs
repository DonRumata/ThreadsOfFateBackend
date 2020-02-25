using Autofac;
using Microsoft.Extensions.Configuration;
using ThreadsofFate.Common.Const;
using ThreadsofFate.Common.Extensions;
using ThreadsOfFate.ReadDomain.Contexts;
using ThreadsOfFate.ReadDomain.Contexts.Abstractions;
using ThreadsOfFate.ReadDomain.Factories;
using ThreadsOfFate.ReadDomain.Factories.Abstractions;
using ThreadsOfFate.ReadDomain.Queries.Abstractions;
using ThreadsOfFate.ReadDomain.Queries.Elastic;
using ThreadsOfFate.ReadDomain.Queries.Elastic.Abstractions;
using ThreadsOfFate.ReadDomain.Services;
using ThreadsOfFate.ReadDomain.Services.Abstractions;
using ThreadsOfFate.ReadDomain.Services.Elastic;
using ThreadsOfFate.ReadDomain.Services.Elastic.Abstractions;

namespace ThreadsOfFate.ReadDomain
{
    public static class ModuleBootstrapper
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder container)
        {
            container.AddSingleton<IElasticSearchClient>(p =>
                new ElasticSearchClient(configuration[AppSettingsConst.ConnectionStrings.ThreadsOfFate]));

            container.AddSingleton<IElasticHealthService, ElasticHealthService>();
            container.AddSingleton<IGlobalSearchQueryBuilderService, GlobalSearchQueryBuilderService>();

            container.AddScoped<ISpellService, SpellService>();
            container.AddScoped<ISearchService, SearchService>();
            container.AddScoped<IGlobalSearchService, GlobalSearchService>();
            container.AddScoped<IBuildFilterQueryService, BuildFilterQueryService>();
            container.AddScoped<ISearchFilterOptionsService, SearchFilterOptionsService>();

            //Queries
            container.AddScoped<IGetElasticItemQuery, GetElasticItemQuery>();
            container.AddScoped<IGetElasticItemCollectionQuery, GetElasticItemCollectionQuery>();

            //Factory
            container.AddScoped<IResponsePackageFactory, ResponsePackageFactory>();

        }
    }
}
