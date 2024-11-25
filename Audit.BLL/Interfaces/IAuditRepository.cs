using Audit.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.BLL.Interfaces
{
    public interface IAuditRepository
    {
        AuditLog Add(AuditLog audit);
        List<AuditLog> GetAllByEmployeeId(int employeeId);

        List<AuditEmployeeName> GetAllEmployeeName();

    }
}
