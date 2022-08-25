﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        //IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<int> Update(T item);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
    }
}
