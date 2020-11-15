using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkMe.Core.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task DeleteAsync(T entity);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
