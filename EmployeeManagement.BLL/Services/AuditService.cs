using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BLL.Models;
using SimpleApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<AuditLogViewDTO> GetAllByEmployeeId(int employeeId)
        {
            return _unitOfWork.Audit.GetAllByEmployeeId(employeeId).Select(audit=> new AuditLogViewDTO()
            {
                EmployeeId = employeeId,
                ActionType = audit.ActionType,
                NewData = audit.NewData,
                OldData = audit.OldData,
            }).ToList();
        }
    }
}
