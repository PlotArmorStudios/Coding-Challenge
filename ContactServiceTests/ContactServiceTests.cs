using ClarkCodingChallenge.BusinessLogic;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContactServiceTests
{
    public class ContactServiceTests
    {
        private readonly ContactsService _contactService;
        private readonly Mock<IRepository<Contact>> _mockRepo;

        public ContactServiceTests()
        {
            _mockRepo = new Mock<IRepository<Contact>>();

            //Inject the mock into the service
            _contactService = new ContactsService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddContact_ShouldCallRepositoryAddAsync()
        {
            //Arrange
            var contact = new Contact { FirstName = "Khenan", LastName = "Newton", Email = "knewton@gmail.com" };

            //Act
            await _contactService.AddContactAsync(contact);

            //Assert
            //Verify that AddAsync was called once
            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public async Task GetSelected_ShouldReturnMatchingContacts()
        {
            //Arrange
            var contact1 = new Contact { FirstName = "Tom", LastName = "Jerry", Email = "tjerry@gmail.com" };
            var contact2 = new Contact { FirstName = "Markson", LastName = "Jerry", Email = "mjerry@gmail.com" };

            var matchingContacts = new List<Contact> { contact1, contact2 };

            //If GetSelectedAsync is called with these parameters, return matchingContacts
            _mockRepo.Setup(repo => repo.GetSelectedAsync("Jerry", "asc"))
                           .ReturnsAsync(matchingContacts);

            //Act
            var selectedContacts = await _contactService.GetSelectedContactAsync("Jerry", "asc");

            //Assert
            //Verify that the desired method was called
            _mockRepo.Verify(repo => repo.GetSelectedAsync("Jerry", "asc"), Times.Once);

            //Verify that matchingContacts has been returned
            Assert.Equal(2, selectedContacts.Count());
            Assert.Contains(selectedContacts, contact => contact.FirstName == "Tom" && contact.LastName == "Jerry");
            Assert.Contains(selectedContacts, contact => contact.FirstName == "Markson" && contact.LastName == "Jerry");

        }
    }
}
