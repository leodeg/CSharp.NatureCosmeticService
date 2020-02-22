using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;
using Nature.Models.ViewModels;

namespace Nature.Controllers
{
	public class ServicesController : Controller
	{
		private readonly IRepository<Service> _repository;
		private readonly IRepository<ServiceCategory> _categories;
		private readonly IWebHostEnvironment _environment;

		private const int NewsOnPage = 6;


		public ServicesController(IRepository<Service> repository, IRepository<ServiceCategory> categories, IWebHostEnvironment environment)
		{
			_repository = repository;
			_categories = categories;
			_environment = environment;
		}

		[HttpGet]
		public IActionResult Index(int page = 1, string title = "")
		{
			IEnumerable<Service> services = GetServicesFromRepository(title);
			ServicesIndexViewModel viewModel = CreateNewsViewModelPagination(page, services);
			return View(viewModel);
		}

		private IEnumerable<Service> GetServicesFromRepository(string title)
		{
			if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
				return _repository.Get().OrderBy(p => p.Id);

			return _repository.Get()
				.Where(p => p.ServiceCategory.Title == title)
				.OrderBy(p => p.Id);
		}

		private ServicesIndexViewModel CreateNewsViewModelPagination(int currentPage, IEnumerable<Service> services)
		{
			var count = services.Count();
			var servicesOnTheCurrentPage = services.Skip((currentPage - 1) * NewsOnPage).Take(NewsOnPage);

			PageViewModel pageViewModel = new PageViewModel(count, currentPage, NewsOnPage);
			return new ServicesIndexViewModel
			{
				PageViewModel = pageViewModel,
				Services = servicesOnTheCurrentPage
			};
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult New()
		{
			var service = new Service();
			ServicesViewModel model = new ServicesViewModel
			{
				Service = service,
				ServiceCategories = _categories.Get()
			};

			return View("ServicesForm", model);
		}

		[HttpPost]
		[Authorize(Roles = Roles.Editor)]
		public async Task<ActionResult> Save(ServicesViewModel model, IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				model.ServiceCategories = _categories.Get();
				return View("ServicesForm", model);
			}

			if (file != null && !string.IsNullOrWhiteSpace(file.FileName))
			{
				model.Service.ImagePath = await ServerFiles.SaveImageToLocalFiles(_environment, file, "services");
			}

			if (model.Service.Id == 0)
				_repository.Create(model.Service);
			else _repository.Update(model.Service);

			return RedirectToAction("Index", "Services");
		}



		[Authorize(Roles = Roles.Editor)]
		public ActionResult Edit(int id)
		{
			var service = _repository.Get(id);
			if (service == null)
				return NotFound();

			ServicesViewModel model = new ServicesViewModel
			{
				Service = service,
				ServiceCategories = _categories.Get()
			};

			return View("ServicesForm", model);
		}

		public ActionResult Details(int id)
		{
			var service = _repository.Get(id);
			if (service == null)
				return NotFound();

			return View("Details", service);
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "Services");
		}
	}
}