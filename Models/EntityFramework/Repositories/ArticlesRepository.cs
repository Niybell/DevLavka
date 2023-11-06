using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.EntityFramework.Repositories
{
    public class ArticlesRepository: IBaseRepository<Article>
    {
        private ApplicationDBContext _context;

        public ArticlesRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Article entity)
        {
            await _context.Articles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Article entity)
        {
            _context.Articles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Article> GetAll()
            => _context.Articles;

        public async Task<Article> UpdateAsync(Article entity)
        {
            _context.Articles.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}