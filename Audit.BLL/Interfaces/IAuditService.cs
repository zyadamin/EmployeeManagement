using Audit.BLL.Models;
using Audit.Domain.Enum;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.BLL.Interfaces
{
    public interface IAuditService
    {
        List<AuditLogViewDTO> GetAllByEmployeeId(int employeeId);
        int LogAudit(int actionType, int id, DateTime timestamp, string? oldData, string? newData);
        void UseTransaction(IDbContextTransaction dbContextTransaction);
    }
}
