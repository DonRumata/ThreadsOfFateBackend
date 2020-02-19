using Autofac;
using ThreadsofFate.Common.Extensions;
using ThreadsofFate.Common.Services;
using ThreadsofFate.Common.Services.Abstractions;

namespace ThreadsofFate.Common
{
    public static class ModuleBootstrapper
    {
        public static void Configure(ContainerBuilder container)
        {
            // Services
            container.AddSingleton<IElasticObjectTypeToAliasMapService, ElasticObjectTypeToAliasMapService>();
        }
    }
}
