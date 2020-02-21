using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

		public ServicesController(IRepository<Service> repository, IRepository<ServiceCategory> categories, IWebHostEnvironment environment)
		{
			_repository = repository;
			_categories = categories;
			_environment = environment;
		}

		public IActionResult Index(string categoryName = "")
		{
			IEnumerable<Service> services;
			string currentCategory = string.Empty;

			if (string.IsNullOrEmpty(categoryName))
			{
				services = _repository.Get().OrderBy(p => p.Id);
				currentCategory = "All Categories";
			}
			else
			{
				services = _repository.Get()
					.Where(p => p.ServiceCategory.Title == categoryName)
					.OrderBy(p => p.Id);

				currentCategory = ((ServiceCategoriesRepository)_categories).Get(categoryName).Title;
			}

			return View(services);
		}

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

		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "Services");
		}
	}
}