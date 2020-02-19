using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class AboutUsRepository : IRepository<AboutUs>
	{
		private readonly ApplicationDbContext context;

		public AboutUsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<AboutUs> Get()
		{
			return context.AboutUs;
		}

		public AboutUs Get(int id)
		{
			return context.AboutUs.FirstOrDefault(item => item.Id == id);
		}

		public void Create(AboutUs item)
		{
			context.AboutUs.Add(item);
			context.SaveChanges();
		}

		public void Update(AboutUs item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.AboutUs.SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Id = item.Id;
			oldItem.Title = item.Title;
			oldItem.Description = item.Description;

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.AboutUs.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.AboutUs.Remove(item);
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
