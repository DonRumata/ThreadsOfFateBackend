using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsOfFate.Domain.Dal.Contexts.Abstractions
{
    public interface IContext
    {
        Task SaveChangesAsync();
        void EraseAllChanges();
        Task<IContextTransaction> BeginTransaction();
    }
}
