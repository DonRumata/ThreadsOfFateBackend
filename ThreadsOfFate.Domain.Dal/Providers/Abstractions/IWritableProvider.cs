using System;
using System.Collections.Generic;
using System.Text;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Providers.Abstractions
{
    public interface IWritableProvider
    {
        IContext DbContext { get; }
    }
}
