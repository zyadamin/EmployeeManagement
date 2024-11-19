using EmployeeManagement.Domian.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IAuditRepository
    {
        AuditLog Add(AuditLog audit);
        List<AuditLog> GetAllByEmployeeId(int employeeId);

    }
}
