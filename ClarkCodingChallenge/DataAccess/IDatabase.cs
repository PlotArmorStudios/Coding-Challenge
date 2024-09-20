using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess
{
    public interface IDatabase<T> where T : class
    {
        IConfiguration Configuration { get; }

        string ConnectionString { get; }
        Task AddAsync(T data);
        Task<IEnumerable<T>> GetSelectedAsync(string lastName, string sortOrder);
    }
}
