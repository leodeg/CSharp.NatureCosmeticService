using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{

	public class NewsRepository : IRepository<News>
	{
		private readonly ApplicationDbContext context;

		public NewsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<News> Get()
		{
			return context.News;
		}

		public News Get(int id)
		{
			return context.News.FirstOrDefault(item => item.Id == id);
		}

		public void Create(News item)
		{
			context.News.Add(item);
			context.SaveChanges();
		}

		public void Update(News item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.News.SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Id = item.Id;
			oldItem.Title = item.Title;
			oldItem.Description = item.Description;
			oldItem.Image = item.Image;

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.News.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.News.Remove(item);
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
