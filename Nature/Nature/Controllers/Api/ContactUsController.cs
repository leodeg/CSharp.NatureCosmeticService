using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nature.Models;

namespace Nature.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ContactUsController : Controller
	{
		private readonly IRepository<ContactUs> _repository;

		public ContactUsController(IRepository<ContactUs> repository)
		{
			_repository = repository;
		}

		[AllowAnonymous]
		[HttpPost]
		public void Post([FromForm] string firstName, [FromForm]string lastName, [FromForm] string phone, [FromForm] string email, [FromForm] string message)
		{
			var model = new ContactUs()
			{
				FirstName = firstName,
				LastName = lastName,
				Phone = phone,
				Email = email,
				Description = message
			};

			_repository.Create(model);
		}
	}
}
