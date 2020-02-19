using Microsoft.Extensions.Configuration;
using Autofac;
using Microsoft.Extensions.Options;
using ReadDomainBootstrapper = ThreadsOfFate.ReadDomain.ModuleBootstrapper;
using CommonBootstrapper = ThreadsofFate.Common.ModuleBootstrapper;
using DomainDalBootstrapper = ThreadsOfFate.Domain.Dal.ModuleBootstrapper;

namespace ThreadsOfFate
{
    static class ModuleBootstrapper
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder container)
        {
            CommonBootstrapper.Configure(container);
            DomainDalBootstrapper.Configure(configuration,container);
            ReadDomainBootstrapper.Configure(configuration, container);
        }
    }
}
