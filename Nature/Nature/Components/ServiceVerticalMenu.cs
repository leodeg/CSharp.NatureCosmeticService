using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;

namespace Nature.Components
{

	public class ServiceVerticalMenu : ViewComponent
	{
		private readonly IRepository<ServiceCategory> _categoryRepository;

		public ServiceVerticalMenu(IRepository<ServiceCategory> categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			var categories = _categoryRepository.Get().OrderBy(c => c.Title);
			return View(categories);
		}
	}
}
