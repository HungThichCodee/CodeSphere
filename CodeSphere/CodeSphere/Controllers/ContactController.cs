using CodeSphere.Constraints;
using CodeSphere.Repositories.ContactRepositories;
using CodeSphere.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository contactsRepository;

        public ContactController(IContactRepository contactsRepository)
        {
            this.contactsRepository = contactsRepository;
        }

        [BindProperty]
        public ContactInputModel Contact { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Error"] = ErrorMessages.InvalidInputModel;
                return this.View(model);
            }

            this.contactsRepository.SendEmail(model);
            this.TempData["Success"] =
                string.Format(SuccessMessages.SuccessfullySubmitedContactForm, model.Name);
            return this.RedirectToPage("/");
        }
    }
}
