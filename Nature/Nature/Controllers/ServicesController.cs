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
		private readonly IHostingEnvironment _environment;

		public ServicesController(IRepository<Service> repository, IRepository<ServiceCategory> categories, IHostingEnvironment environment)
		{
			_repository = repository;
			_categories = categories;
			_environment = environment;
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
		public async Task<ActionResult> Save(ServicesViewModel model, IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				model.ServiceCategories = _categories.Get();
				return View("ServicesForm", model);
			}

			if (file != null && file.Length > 0 && !string.IsNullOrWhiteSpace(file.FileName))
			{
				SaveImageToLocalFiles(model.Service, file);
			}

			if (model.Service.Id == 0)
				_repository.Create(model.Service);
			else _repository.Update(model.Service);

			return RedirectToAction("Index", "Services");
		}

		private void SaveImageToLocalFiles(Service service, IFormFile file)
		{
			var imagePath = @"\img\services\";
			var uploadPath = _environment.WebRootPath + imagePath;
			if (Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			var uniqFileName = Guid.NewGuid().ToString();
			var fileName = Path.GetFileName(uniqFileName + "." + file.FileName.Split(".")[1].ToLower());
			string fullPath = uploadPath + fileName;

			imagePath += @"\";
			var filePath = @".." + Path.Combine(imagePath, fileName);

			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}

			service.ImagePath = filePath;
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