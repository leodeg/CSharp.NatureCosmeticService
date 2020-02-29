using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nature.Models;
using Nature.Models.ViewModels;

namespace Nature.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly IRepository<News> _news;
		private readonly IRepository<Doctor> _doctors;
		private readonly IRepository<Service> _services;

		public HomeController(ILogger<HomeController> logger, IRepository<Service> services, IRepository<News> news, IRepository<Doctor> doctors)
		{
			_logger = logger;
			_services = services;
			_news = news;
			_doctors = doctors;
		}

		public IActionResult Index()
		{
			var doctors = _doctors.Get().OrderByDescending(p => p.Id).Take(3);
			var services = _services.Get().OrderByDescending(p => p.Id).Take(3);
			var news = _news.Get().OrderByDescending(p => p.UploadDate).Take(2);

			HomeViewModel model = new HomeViewModel
			{
				Doctors = doctors,
				Services = services,
				News = news
			};

			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
