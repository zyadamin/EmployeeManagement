using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.Domian.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(MainDbContext mainDbContext) : base(mainDbContext)
        {
        }
    }
}
