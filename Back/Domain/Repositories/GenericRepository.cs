using DAL.DataBaseContexts;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GLSTablesDataBaseContext _context;
        public GenericRepository(GLSTablesDataBaseContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            var newItem = (await _context.Set<T>().AddAsync(entity)).Entity;
            var id = (await _context.SaveChangesAsync());
            return newItem;

        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
       }

        //public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        //{
        //    return _context.Set<T>().Where(expression);
        //}

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
