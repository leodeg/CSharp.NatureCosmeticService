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
	public class NewsController : Controller
	{
		private readonly IRepository<News> _repository;
		private readonly IWebHostEnvironment _environment;

		public NewsController(IRepository<News> repository, IWebHostEnvironment environment)
		{
			_repository = repository;
			_environment = environment;
		}

		[HttpGet]
		public IActionResult Index(string title = "")
		{
			IEnumerable<News> news;

			string currentCategory = string.Empty;
			if (string.IsNullOrEmpty(title))
				news = _repository.Get().OrderByDescending(p => p.UploadDate);
			else news = ((NewsRepository)_repository).Get(title).OrderByDescending(p => p.UploadDate);

			return View(news);
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult New()
		{
			var news = new News();
			return View("NewsForm", news);
		}

		[HttpPost]
		[Authorize(Roles = Roles.Editor)]
		public async Task<ActionResult> Save(News model, IFormFile file)
		{
			if (!ModelState.IsValid)
			{
				return View("NewsForm", model);
			}

			if (file != null && !string.IsNullOrWhiteSpace(file.FileName))
			{
				model.ImagePath = await ServerFiles.SaveImageToLocalFiles(_environment, file, ServerFiles.news);
			}

			if (model.Id == 0)
				_repository.Create(model);
			else _repository.Update(model);

			return RedirectToAction("Index", "News");
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult Edit(int id)
		{
			var news = _repository.Get(id);
			if (news == null)
				return NotFound();

			return View("NewsForm", news);
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult Details(int id)
		{
			var news = _repository.Get(id);
			if (news == null)
				return NotFound();

			return View("Details", news);
		}

		[Authorize(Roles = Roles.Editor)]
		public ActionResult Remove(int id)
		{
			if (!_repository.Delete(id))
				return NotFound();

			return RedirectToAction("Index", "News");
		}
	}
}