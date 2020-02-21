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

	public class DoctorsController : Controller
	{
		private readonly IRepository<Doctor> _repository;
		private readonly IRepository<DoctorCategory> _categories;
		private readonly IWebHostEnvironment _environment;

		public DoctorsController(IRepository<Doctor> repository, IRepository<DoctorCategory> categories, IWebHostEnvironment environment)
		{
			_repository = repository;
			_categories = categories;
			_environment = environment;
		}

		public IActionResult Index(string categoryName = "")
		{
			IEnumerable<Doctor> doctors;
			string currentCategory = string.Empty;

			if (string.IsNullOrEmpty(categoryName))
			{
				doctors = _repository.Get().OrderBy(p => p.Id);
				currentCategory = "All Categories";
			}
			else
			{
				doctors = _repository.Get()
					.Where(p => p.DoctorCategory.Title == categoryName)
					.OrderBy(p => p.Id);

				currentCategory = ((DoctorCategoriesRepository)_categories).Get(categoryName).Title;
			}

			return View(doctors);
		}

		public ActionResult New()
		{
			var doctor = new Doctor();
			DoctorsViewModel model = new DoctorsViewModel
			{
				Doctor = doctor,
				DoctorCategories = _categories.Get()
			};

			return View("DoctorsForm", model);
		}

		[HttpPost]
		public async Task<ActionResult> Save(DoctorsViewModel model, IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				model.DoctorCategories = _categories.Get();
				return View("DoctorsForm", model);
			}

			if (file != null && !string.IsNullOrWhiteSpace(file.FileName))
			{
				model.Doctor.ImagePath = await ServerFiles.SaveImageToLocalFiles(_environment, file, ServerFiles.doctors);
			}

			if (model.Doctor.Id == 0)
				_repository.Create(model.Doctor);
			else _repository.Update(model.Doctor);

			return RedirectToAction("Index", "Doctors");
		}



		public ActionResult Edit(int id)
		{
			var doctor = _repository.Get(id);
			if (doctor == null)
				return NotFound();

			DoctorsViewModel model = new DoctorsViewModel
			{
				Doctor = doctor,
				DoctorCategories = _categories.Get()
			};

			return View("DoctorsForm", model);
		}

		public ActionResult Details(int id)
		{
			var doctor = _repository.Get(id);
			if (doctor == null)
				return NotFound();

			return View("Details", doctor);
		}

		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "Doctors");
		}
	}
}