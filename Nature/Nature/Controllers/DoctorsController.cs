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
	public class DoctorsController : Controller
	{
		private readonly IRepository<Doctor> _repository;
		private readonly IRepository<DoctorCategory> _categories;
		private readonly IWebHostEnvironment _environment;

		private const int NewsOnPage = 6;

		public DoctorsController(IRepository<Doctor> repository, IRepository<DoctorCategory> categories, IWebHostEnvironment environment)
		{
			_repository = repository;
			_categories = categories;
			_environment = environment;
		}

		[HttpGet]
		public IActionResult Index(int page = 1, string title = "")
		{
			IEnumerable<Doctor> doctors = GetDoctorsFromRepository(title);
			DoctorsIndexViewModel viewModel = CreateDoctorsViewModelPagination(page, doctors);
			return View(viewModel);
		}

		private IEnumerable<Doctor> GetDoctorsFromRepository(string title)
		{
			if (string.IsNullOrEmpty(title))
			{
				return _repository.Get().OrderBy(p => p.Id);
			}
			else
			{
				return _repository.Get()
					.Where(p => p.DoctorCategory.Title == title)
					.OrderBy(p => p.Id);
			}
		}

		private DoctorsIndexViewModel CreateDoctorsViewModelPagination(int currentPage, IEnumerable<Doctor> doctors)
		{
			var count = doctors.Count();
			var doctorsOnTheCurrentPage = doctors.Skip((currentPage - 1) * NewsOnPage).Take(NewsOnPage);

			PageViewModel pageViewModel = new PageViewModel(count, currentPage, NewsOnPage);
			return new DoctorsIndexViewModel
			{
				PageViewModel = pageViewModel,
				Doctors = doctorsOnTheCurrentPage
			};
		}

		[Authorize(Roles = Roles.Editor)]
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
		[Authorize(Roles = Roles.Editor)]
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

		[Authorize(Roles = Roles.Editor)]
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

		[Authorize(Roles = Roles.Editor)]
		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "Doctors");
		}
	}
}