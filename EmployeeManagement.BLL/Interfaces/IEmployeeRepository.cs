using EmployeeManagement.Domian.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> ListEmployee(int start, int size);
        IEnumerable<Employee> GetEmployees();

        Employee GetByIdIncludeProject(int id);
        bool CheckUniqueEmail(string email);
    }
}
