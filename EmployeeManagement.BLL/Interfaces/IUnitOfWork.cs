using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        IProjectRepository Project { get; }
        IDbContextTransaction BeginTransaction();
        int Complete();
        //int SaveChanges();
    }
}
