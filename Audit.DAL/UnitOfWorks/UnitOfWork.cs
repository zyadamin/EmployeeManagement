
using Audit.BLL.Interfaces;
using Audit.DAL.Context;
using Audit.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuditDbContext _auditDbContext;
        private IDbContextTransaction _transaction;

        public IAuditRepository Audit { get; private set; }

        public UnitOfWork(AuditDbContext auditDbContext)
        {
            _auditDbContext = auditDbContext;
            Audit = new AuditRepository(_auditDbContext);
        }

        public int Complete()
        {
            return _auditDbContext.SaveChanges();
        }
        public void Dispose()
        {
            _auditDbContext.Dispose();
            _transaction?.Dispose();

        }

        public void UseTransaction(IDbContextTransaction dbContextTransaction)
        {
            _auditDbContext.Database.UseTransaction(dbContextTransaction.GetDbTransaction());
            _transaction = dbContextTransaction;
        }

        public void EndTransaction()
        {
            try { 
                _transaction.Commit();
            
            } catch {

                _transaction.Rollback();
            
            }
        }
    }


}


