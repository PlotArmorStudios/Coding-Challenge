using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.BusinessLogic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClarkCodingChallenge.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactService;

        public ContactsController(IContactsService contactRepository)
        {
            _contactService = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string lastName = null, string sortOrder = "asc")
        {
            //Using asynchronous programming to not hold up the calling thread while
            //querying desired contacts
            var contacts = await _contactService.GetSelectedContactAsync(lastName, sortOrder);
            return View(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact obj)
        {
            if (!ModelState.IsValid)
            {
                await _contactService.AddContactAsync(obj);
                return RedirectToAction(nameof(ShowCreateForm));
            }
            return RedirectToAction(nameof(ShowConfirmationPage));
        }

        public IActionResult ShowCreateForm()
        {
            return View();
        }

        public IActionResult ShowConfirmationPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
