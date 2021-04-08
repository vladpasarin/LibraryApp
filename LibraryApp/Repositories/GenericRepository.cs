using AutoMapper;
using LibraryApp.Data;
using LibraryApp.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibraryDbContext _context;
        protected readonly DbSet<T> _table;

        public GenericRepository(LibraryDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void CreateRange(List<T> entities)
        {
            _table.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<T> FindById(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
