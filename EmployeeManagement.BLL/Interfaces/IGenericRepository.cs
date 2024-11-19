using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        T Add(T entity);
        T GetById(int id);
        T Update(T entity);
        void Delete(T entity);

    }

}
