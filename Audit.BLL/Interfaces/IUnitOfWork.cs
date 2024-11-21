using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuditRepository Audit { get; }
        void UseTransaction(IDbContextTransaction dbContextTransaction);
        void EndTransaction();
        int Complete();
    }
}
