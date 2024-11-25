using Audit.BLL.Interfaces;
using Audit.BLL.Models;
using Audit.Domain.Entity;
using Audit.Domain.Enum;
using Audit.Domain.Helper;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.BLL.Services
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
            var EmployeesName = _unitOfWork.Audit.GetAllEmployeeName();
            return _unitOfWork.Audit.GetAllByEmployeeId(employeeId).Select(audit => new AuditLogViewDTO()
            {

                Name = EmployeesName.FirstOrDefault(x=>x.Id == employeeId)?.Name ?? "",
                Action = Helper.ActionTypes[(int)audit.ActionType-1],
                Timestamp = audit.Timestamp.ToShortTimeString(),
                NewData = audit.NewData,
                OldData = audit.OldData,
            }).ToList();
        }

        public int LogAudit(int actionType, int id, DateTime timestamp, string? oldData, string? newData)
        {

            AuditLog auditLog = new AuditLog()
            {
                ActionType = (ActionType)actionType,
                EmployeeId = id,
                Timestamp = timestamp,
                OldData = oldData,
                NewData = newData
            };

            var result = _unitOfWork.Audit.Add(auditLog);
            _unitOfWork.Complete();

            return result.id;
        }

        public void UseTransaction(IDbContextTransaction dbContextTransaction)
        {
            _unitOfWork.UseTransaction(dbContextTransaction);
        }
    }
}
