using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManagement.Domian.Entity
{
    public class Project : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }


    }
}
