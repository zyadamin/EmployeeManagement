using Audit.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Audit.Domain.Entity
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
