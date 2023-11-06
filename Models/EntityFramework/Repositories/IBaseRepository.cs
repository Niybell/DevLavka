using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.EntityFramework.Repositories
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity);

        IQueryable<T> GetAll();

        Task DeleteAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}