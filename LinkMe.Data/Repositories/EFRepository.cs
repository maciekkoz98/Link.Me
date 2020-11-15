using LinkMe.Core.Entities;
using LinkMe.Core.Interfaces;
using LinkMe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Data.Repositories
{
    public class EFRepository<T> : IAsyncRepository<T>
        where T : BaseEntity
    {
        private readonly LinkMeContext dbContext;

        public EFRepository(LinkMeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await this.dbContext.Set<T>().AddAsync(entity);
            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            this.dbContext.Remove(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
