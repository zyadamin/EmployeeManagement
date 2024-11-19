using EmployeeManagement.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IEmployeeService
    {
        int Add(EmployeeCreateDTO entity);
        EmployeeDetailsDTO GetById(int id);
        int Update(EmployeeDetailsDTO entity);
        void Delete(int id);
        List<EmployeeViewDTO> List(int start, int size);
    }
}
