using EmployeeManagement.Domian.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Models
{
    public class AuditLogViewDTO
    {
        public ActionType ActionType { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Timestamp { get; set; }

        public string? OldData { get; set; }

        public string? NewData { get; set; }

    }
}
