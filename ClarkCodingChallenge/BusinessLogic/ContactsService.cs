using ClarkCodingChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClarkCodingChallenge.Repositories;

namespace ClarkCodingChallenge.BusinessLogic
{
    public class ContactsService : IContactsService
    {
        public IRepository<Contact> Repository;

        public ContactsService(IRepository<Contact> contactRepository)
        {
            Repository = contactRepository;
        }

        public async Task AddContactAsync(Contact obj)
        {
            //Implement contact specific operations here when adding entry
            //Model validation, etc

            await Repository.AddAsync(obj);
        }

        public async Task<IEnumerable<Contact>> GetSelectedContactAsync(string lastName = null, string sortOrder = "asc")
        {
            //Implement contact specific operations here when getting selected entry
            var contacts = await Repository.GetSelectedAsync(lastName, sortOrder);
            return contacts;
        }
    }
}
