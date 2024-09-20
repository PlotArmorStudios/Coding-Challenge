using ClarkCodingChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.BusinessLogic
{
    public interface IContactsService
    {
        Task AddContactAsync(Contact obj);
        Task<IEnumerable<Contact>> GetSelectedContactAsync(string lastName, string sortOrder);
    }
}
