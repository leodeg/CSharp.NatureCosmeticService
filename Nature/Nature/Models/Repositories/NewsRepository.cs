using Microsoft.AspNetCore.Hosting;
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
		private readonly IWebHostEnvironment _environment;


		public NewsRepository(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			_environment = environment;
		}

		public IEnumerable<News> Get()
		{
			return context.News;
		}

		public News Get(int id)
		{
			return context.News.FirstOrDefault(item => item.Id == id);
		}

		public IEnumerable<News> Get(string title)
		{
			return context.News.Where(i => i.Title.Contains(title));
		}

		public void Create(News item)
		{
			item.UploadDate = DateTime.Now;
			item.EditDate = DateTime.Now;
			context.News.Add(item);
			context.SaveChanges();
		}

		public void Update(News item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.News.SingleOrDefault(i => i.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Id = item.Id;
			oldItem.Title = item.Title;
			oldItem.Description = item.Description;
			oldItem.EditDate = DateTime.Now;

			if (!string.IsNullOrWhiteSpace(item.ImagePath))
			{
				bool isEmpty = string.IsNullOrWhiteSpace(oldItem.ImagePath);
				bool isTheSameImage = string.Compare(oldItem.ImagePath, item.ImagePath) == 0;
				if (!isEmpty && !isTheSameImage)
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, oldItem.ImagePath, "news");
				}

				oldItem.ImagePath = item.ImagePath;
			}

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.News.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				if (!string.IsNullOrEmpty(item.ImagePath))
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, item.ImagePath, "news");
				}

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
