using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Providers.Abstractions
{
    abstract class WritableProviderBase : IWritableProvider
    {

        protected ILogger Logger { get; }
        protected IThreadsOfFateContext Context { get; }

        public IContext DbContext => Context;

        protected WritableProviderBase(ILogger logger, IThreadsOfFateContext context)
        {
            Logger = logger;
            Context = context;
        }
    }
}
