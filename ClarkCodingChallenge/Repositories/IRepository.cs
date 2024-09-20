using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetSelectedAsync(string lastName, string sortOrder);
    }
}
