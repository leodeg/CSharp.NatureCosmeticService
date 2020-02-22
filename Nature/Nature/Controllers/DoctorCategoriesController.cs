using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nature.Models;

namespace Nature.Controllers
{
	[Authorize(Roles = Roles.Editor)]
	public class DoctorCategoriesController : Controller
	{
		private readonly IRepository<DoctorCategory> _repository;

		public DoctorCategoriesController(IRepository<DoctorCategory> repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			var categories = _repository.Get().OrderBy(c => c.Title);
			return View("Index", categories);
		}

		public ActionResult New()
		{
			var category = new DoctorCategory();
			return View("DoctorCategoryForm", category);
		}

		[HttpPost]
		public ActionResult Save(DoctorCategory category)
		{
			if (!ModelState.IsValid)
			{
				return View("DoctorCategoryForm", category);
			}

			if (category.Id == 0)
				_repository.Create(category);
			else _repository.Update(category);

			return RedirectToAction("Index", "DoctorCategories");
		}

		public ActionResult Edit(int id)
		{
			var category = _repository.Get(id);
			if (category == null)
				return NotFound();

			return View("DoctorCategoryForm", category);
		}

		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "DoctorCategories");
		}
	}
}