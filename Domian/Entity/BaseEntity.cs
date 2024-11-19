using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domian.Entity
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false; 
        public DateTime? DeletedAt { get; set; }
    }
}
