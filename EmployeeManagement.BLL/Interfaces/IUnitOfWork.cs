using EmployeeManagement.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApi.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        IProjectRepository Project { get; }
        IAuditRepository Audit { get; }
        int Complete();
        int CompleteAudit();
        int SaveChanges();
    }
}
