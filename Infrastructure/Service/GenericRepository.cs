using Core.Entities;
using Core.Interface;
using Infrastructure.Data.AppDB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext Context;
        private DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await dbSet.FirstOrDefaultAsync(n => n.Id == id);

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);

            await SaveAsync();
        }

        protected async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}