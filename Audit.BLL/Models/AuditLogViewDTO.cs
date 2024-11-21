using Audit.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.BLL.Models
{
    public class AuditLogViewDTO
    {
        
        public string Action { get; set; }

        public string Name { get; set; }

        public string Timestamp { get; set; }

        public string? OldData { get; set; }

        public string? NewData { get; set; }

    }
}
