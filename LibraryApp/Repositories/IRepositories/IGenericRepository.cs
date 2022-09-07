using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<bool> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void CreateRange(List<T> entities);
        Task<T> FindById(int id);
        Task<bool> SaveChanges();
    }
}
