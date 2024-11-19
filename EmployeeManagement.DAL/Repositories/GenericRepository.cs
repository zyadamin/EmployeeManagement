using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MainDbContext _mainDbContext;

        public GenericRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public T Add(T entity)
        {
            _mainDbContext.Set<T>().Add(entity);
            return entity;
        }


        public T GetById(int id)
        {
            return _mainDbContext.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            _mainDbContext.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null)
            {

                property.SetValue(entity, true);
                _mainDbContext.Update(entity);
            }
            else {

                _mainDbContext.Set<T>().Remove(entity);
            }

        }
    }
}
