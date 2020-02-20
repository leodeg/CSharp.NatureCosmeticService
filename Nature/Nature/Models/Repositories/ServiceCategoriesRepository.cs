using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ServiceCategoriesRepository : IRepository<ServiceCategory>
	{
		private readonly ApplicationDbContext context;

		public ServiceCategoriesRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<ServiceCategory> Get()
		{
			return context.ServiceCategories;
		}

		public ServiceCategory Get(int id)
		{
			return context.ServiceCategories.FirstOrDefault(item => item.Id == id);
		}

		public void Create(ServiceCategory item)
		{
			context.ServiceCategories.Add(item);
			context.SaveChanges();
		}

		public void Update(ServiceCategory item)
		{
			var oldItem = context.ServiceCategories.SingleOrDefault(i => i.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Title = item.Title;
			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.ServiceCategories.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.ServiceCategories.Remove(item);
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
