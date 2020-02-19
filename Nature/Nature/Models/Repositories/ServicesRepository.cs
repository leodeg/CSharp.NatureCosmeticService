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
		private readonly ApplicationDbContext context;

		public ServicesRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Service> Get()
		{
			return context.Services.Include(item => item.ServiceCategory);
		}

		public Service Get(int id)
		{
			return context.Services.Include(item => item.ServiceCategory).FirstOrDefault(item => item.Id == id);
		}

		public void Create(Service item)
		{
			context.Services.Add(item);
			context.SaveChanges();
		}

		public void Update(Service item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.Services.Include(item => item.ServiceCategory).SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Title = item.Title;

			oldItem.Price = item.Price;
			oldItem.Description = item.Description;
			oldItem.Image = item.Image;

			oldItem.ServiceCategoryId = oldItem.ServiceCategoryId;
			oldItem.ServiceCategory = oldItem.ServiceCategory;

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.Services.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.Services.Remove(item);
				context.SaveChanges();
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
					context.Dispose();
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
