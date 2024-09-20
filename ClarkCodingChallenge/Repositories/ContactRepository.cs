using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Repositories
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class ContactRepository : IRepository<Contact>
    {
        private readonly IDatabase<Contact> _dataAccess;

        public ContactRepository(IDatabase<Contact> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task AddAsync(Contact contact)
        {
            await _dataAccess.AddAsync(contact);
        }

        public async Task<IEnumerable<Contact>> GetSelectedAsync(string lastName = null, string sortOrder = "asc")
        {
            return await _dataAccess.GetSelectedAsync(lastName, sortOrder);
        }
    }
}
