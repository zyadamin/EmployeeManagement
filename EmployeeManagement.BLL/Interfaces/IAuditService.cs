using EmployeeManagement.BLL.Models;
using EmployeeManagement.Domian.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IAuditService
    {
        List<AuditLogViewDTO> GetAllByEmployeeId(int employeeId);

    }
}
