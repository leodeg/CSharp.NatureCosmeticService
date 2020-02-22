using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;

namespace Nature.Controllers
{
	public class AboutUsController : Controller
	{
		private readonly IRepository<AboutUs> _repository;

		public AboutUsController(IRepository<AboutUs> repository)
		{
			_repository = repository;
		}

		public IActionResult Index()
		{
			var aboutUs = _repository.Get().FirstOrDefault();
			return View(aboutUs);
		}

		[Authorize(Roles = Roles.Editor)]
		[HttpPost]
		public async Task<ActionResult> Save(AboutUs model)
		{
			if (!ModelState.IsValid)
				return View("AboutUsForm", model);

			_repository.Update(model);
			return RedirectToAction("Index", "AboutUs");
		}


		[Authorize(Roles = Roles.Editor)]
		public ActionResult Edit(int id)
		{
			var aboutUs = _repository.Get().FirstOrDefault();
			if (aboutUs == null)
				aboutUs = new AboutUs();

			return View("AboutUsForm", aboutUs);
		}
	}
}