
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Domian.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext _mainDbContext;
        private IDbContextTransaction _transaction;


        public IEmployeeRepository Employee { get; private set; }

        public IProjectRepository Project { get; private set; }


        public UnitOfWork(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;

            Employee = new EmployeeRepository(_mainDbContext);
            Project = new ProjectRepository(_mainDbContext);
        }

        public IDbContextTransaction BeginTransaction() {
            return _mainDbContext.Database.BeginTransaction();
        }

        public int Complete()
        {
            return _mainDbContext.SaveChanges();
        }


        //public int SaveChanges()
        //{
        //    int result = 0;

        //    using (_transaction = _mainDbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            result += _mainDbContext.SaveChanges();

        //            result += _auditDbContext.SaveChanges();

        //            _transaction.Commit();
        //        }
        //        catch
        //        {
        //            _transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    return result;
        //}

        public void Dispose()
        {
            _mainDbContext.Dispose();
            _transaction?.Dispose();
        }
    }


}


