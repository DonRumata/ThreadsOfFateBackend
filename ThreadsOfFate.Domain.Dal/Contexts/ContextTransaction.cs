using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;

namespace ThreadsOfFate.Domain.Dal.Contexts
{
    sealed class ContextTransaction : IContextTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public ContextTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
