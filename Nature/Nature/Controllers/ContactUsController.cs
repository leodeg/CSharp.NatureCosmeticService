using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;

namespace Nature.Controllers
{
	[Authorize(Roles = Roles.Editor)]
	public class ContactUsController : Controller
	{
		private readonly IRepository<ContactUs> _repository;
		private const int NewsOnPage = 50;

		public ContactUsController(IRepository<ContactUs> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IActionResult Index(int page = 1, string searchQuery = "")
		{
			IEnumerable<ContactUs> contacts = GetContacts(searchQuery);
			ContactUsIndexViewModel viewModel = CreateContactUsViewModelPagination(page, contacts);
			return View(viewModel);
		}

		private IEnumerable<ContactUs> GetContacts(string searchQuery)
		{
			if (string.IsNullOrEmpty(searchQuery))
			{
				return _repository.Get()
					.OrderBy(p => p.LastName)
					.OrderBy(p => p.HasBeenRead);
			}
			else
			{
				return _repository.Get()
					.Where(p => p.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || p.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
					.OrderBy(p => p.LastName)
					.OrderBy(p => p.HasBeenRead);
			}
		}

		private ContactUsIndexViewModel CreateContactUsViewModelPagination(int currentPage, IEnumerable<ContactUs> contacts)
		{
			var count = contacts.Count();
			var contactsOnTheCurrentPage = contacts.Skip((currentPage - 1) * NewsOnPage).Take(NewsOnPage);

			PageViewModel pageViewModel = new PageViewModel(count, currentPage, NewsOnPage);
			return new ContactUsIndexViewModel
			{
				PageViewModel = pageViewModel,
				ContactUs = contactsOnTheCurrentPage
			};
		}


		[AllowAnonymous]
		public ActionResult New()
		{
			var contact = new ContactUs();
			return View("ContactUsForm", contact);
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Save(ContactUs model)
		{
			if (!ModelState.IsValid)
			{
				return View("ContactUsForm", model);
			}

			if (model.Id == 0)
				_repository.Create(model);
			else _repository.Update(model);

			return RedirectToAction("Index", "ContactUs");
		}

		public ActionResult MarkReadUnread(int id)
		{
			((ContactUsRepository)_repository).SwitchIsContactWasReaded(id);
			return RedirectToAction("Index", "ContactUs");
		}

		public ActionResult Edit(int id)
		{
			var contact = _repository.Get(id);
			if (contact == null)
				return NotFound();

			return View("ContactUsForm", contact);
		}

		public ActionResult Details(int id)
		{
			var contact = _repository.Get(id);
			if (contact == null)
				return NotFound();

			return View("Details", contact);
		}

		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "ContactUs");
		}
	}
}