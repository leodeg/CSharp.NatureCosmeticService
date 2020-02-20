using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nature.Models;

namespace Nature.Controllers
{
	public class ServiceCategoriesController : Controller
	{
		private readonly IRepository<ServiceCategory> _repository;

		public ServiceCategoriesController(IRepository<ServiceCategory> repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			var categories = _repository.Get();
			return View("Index", categories);
		}

		public ActionResult New()
		{
			var category = new ServiceCategory();
			return View("ServiceCategoryForm", category);
		}

		[HttpPost]
		public ActionResult Save(ServiceCategory category)
		{
			if (!ModelState.IsValid)
			{
				return View("ServiceCategoryForm", category);
			}

			if (category.Id == 0)
				_repository.Create(category);
			else _repository.Update(category);

			return RedirectToAction("Index", "ServiceCategories");
		}

		public ActionResult Edit(int id)
		{
			var category = _repository.Get(id);
			if (category == null)
				return NotFound();

			return View("ServiceCategoryForm", category);
		}

		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "ServiceCategories");
		}
	}
}