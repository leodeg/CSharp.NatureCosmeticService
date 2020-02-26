using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;

namespace Nature.Components
{
	public class DoctorCategoryMenu : ViewComponent
	{
		private readonly IRepository<DoctorCategory> _categoryRepository;

		public DoctorCategoryMenu(IRepository<DoctorCategory> categoryRepository)
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
