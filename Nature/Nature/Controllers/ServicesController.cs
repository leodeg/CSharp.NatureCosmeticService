using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;
using Nature.Models.ViewModels;

namespace Nature.Controllers
{
	public class ServicesController : Controller
	{
		private readonly IRepository<Service> _repository;
		private readonly IRepository<ServiceCategory> _categories;

		public ServicesController(IRepository<Service> repository, IRepository<ServiceCategory> categories)
		{
			_repository = repository;
			_categories = categories;
		}

		public IActionResult Index()
		{
			var services = _repository.Get();
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
		public ActionResult Save(ServicesViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.ServiceCategories = _categories.Get();
				return View("ServicesForm", model);
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