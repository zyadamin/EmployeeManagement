using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.Domian.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditDbContext _auditDbContext;

        public AuditRepository(AuditDbContext auditDbContext)
        {
            _auditDbContext = auditDbContext;
        }
        public AuditLog Add(AuditLog audit)
        {
            _auditDbContext.AuditLog.Add(audit);
            return audit;
        }

        public List<AuditLog> GetAllByEmployeeId(int employeeId)
        {
           return _auditDbContext.AuditLog.Where(x=>x.EmployeeId==employeeId).ToList();
        }
    }
}
