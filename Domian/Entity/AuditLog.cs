using EmployeeManagement.Domian.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domian.Entity
{
    public class AuditLog
    {

        public int id { get; set; }

        public ActionType ActionType { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [MaxLength]
        public string? OldData { get; set; }

        [MaxLength]
        public string? NewData { get; set; }



    }
}
