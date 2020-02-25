using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ThreadsofFate.Common.Const;
using ThreadsofFate.Common.Extensions;
using ThreadsOfFate.Domain.Dal.Contexts;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Providers;
using ThreadsOfFate.Domain.Dal.Providers.Abstractions;
using ThreadsOfFate.Domain.Dal.Queries;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions;
using ThreadsOfFate.Domain.Dal.Queries.Abstractions.Spell;
using ThreadsOfFate.Domain.Dal.Queries.Spell;

namespace ThreadsOfFate.Domain.Dal
{
    public static class ModuleBootstrapper
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder container)
        {
            container.AddDbContext<ThreadsOfFateContext, IThreadsOfFateContext>(ob => ob.UseSqlServer(configuration[AppSettingsConst.ConnectionStrings.ThreadsOfFate],
                    sob => sob.CommandTimeout(Convert.ToInt32(configuration.GetValue(AppSettingsConst.SqlCommandsTimeouts.ThreadsOfFate, AppSettingsConst.SqlCommandsTimeouts.ThreadsOfFateDefault)))),
                opt => new ThreadsOfFateContext(opt));

            container.AddScoped<ISpellProvider, SpellProvider>();
            container.AddScoped<ISearchProvider, SearchProvider>();

            container.AddScoped<IGetSpellDescriptionQuery, GetSpellDescriptionQuery> ();
            container.AddScoped<IGetSpellElementsFilterOptionsQuery, GetSpellElementsFilterOptionsQuery>();
        }
    }
}
