using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.Domian.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MainDbContext _mainDbContext;

        public EmployeeRepository(MainDbContext mainDbContext) : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public Employee GetByIdIncludeProject(int id)
        {
            return _mainDbContext.Employee.Where(x => x.Id == id).Include(x => x.Projects).FirstOrDefault();
        }

        public IEnumerable<Employee> ListEmployee(int start, int size)
        {
           return _mainDbContext.Employee.Skip(start).Take(size).ToList();
        }
    }
}
