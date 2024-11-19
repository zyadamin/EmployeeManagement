
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Domian.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApi.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext _mainDbContext;
        private readonly AuditDbContext _auditDbContext;
        private IDbContextTransaction _transaction;


        public IEmployeeRepository Employee { get; private set; }

        public IProjectRepository Project { get; private set; }

        public IAuditRepository Audit { get; private set; }

        public UnitOfWork(MainDbContext mainDbContext,AuditDbContext auditDbContext)
        {
            _mainDbContext = mainDbContext;
            _auditDbContext = auditDbContext;

            Employee = new EmployeeRepository(_mainDbContext);
            Project = new ProjectRepository(_mainDbContext);
            Audit = new AuditRepository(_auditDbContext);
        }

        public int Complete()
        {
            return _mainDbContext.SaveChanges();

        }
        public int CompleteAudit()
        {
            return _auditDbContext.SaveChanges();

        }

        public int SaveChanges()
        {
            int result = 0;

            using (_transaction = _mainDbContext.Database.BeginTransaction())
            {
                try
                {
                    result +=  _mainDbContext.SaveChanges();

                    result +=  _auditDbContext.SaveChanges();

                    _transaction.Commit();
                }
                catch
                {
                    _transaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        public void Dispose()
        {
            _mainDbContext.Dispose();
            _auditDbContext.Dispose();
            _transaction?.Dispose();
        }
    }


}


