using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{

	public class ServicesRepository : IRepository<Service>
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _environment;

		public ServicesRepository(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this._context = context;
			_environment = environment;
		}

		public IEnumerable<Service> Get()
		{
			return _context.Services.Include(item => item.ServiceCategory);
		}

		public Service Get(int id)
		{
			return _context.Services.Include(item => item.ServiceCategory).FirstOrDefault(item => item.Id == id);
		}

		public void Create(Service item)
		{
			_context.Services.Add(item);
			_context.SaveChanges();
		}

		public void Update(Service item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = _context.Services.Include(item => item.ServiceCategory).SingleOrDefault(i => i.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Title = item.Title;
			oldItem.Price = item.Price;
			oldItem.Description = item.Description;

			if (!string.IsNullOrWhiteSpace(item.ImagePath))
			{
				bool isEmpty = string.IsNullOrWhiteSpace(oldItem.ImagePath);
				bool isTheSameImage = string.Compare(oldItem.ImagePath, item.ImagePath) == 0;
				if (!isEmpty && !isTheSameImage)
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, oldItem.ImagePath, "services");
				}

				oldItem.ImagePath = item.ImagePath;
			}

			if (item.ServiceCategoryId != 0)
				oldItem.ServiceCategoryId = item.ServiceCategoryId;

			_context.Services.Update(oldItem);
			_context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = _context.Services.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				if (!string.IsNullOrEmpty(item.ImagePath))
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, item.ImagePath, "services");
				}

				_context.Services.Remove(item);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
