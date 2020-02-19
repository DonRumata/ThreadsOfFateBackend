using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Contexts
{
    abstract class DbContextBase<TContext> : DbContext, IContext
    where TContext:DbContext
    {
        protected DbContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void EraseAllChanges()
        {
            foreach (var entry in base.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public async Task<IContextTransaction> BeginTransaction()
        {
            var transaction = await base.Database.BeginTransactionAsync().ConfigureAwait(false);
            return new ContextTransaction(transaction);
        }
    }
}
