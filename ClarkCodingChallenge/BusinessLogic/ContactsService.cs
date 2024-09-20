using ClarkCodingChallenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClarkCodingChallenge.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj, serviceProvider: null, items: null);

            if (!Validator.TryValidateObject(obj, context, validationResults, true))
            {
                var validationErrors = validationResults.Select(r => r.ErrorMessage).ToList();
                throw new ValidationException(string.Join("; ", validationErrors));
            }

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
