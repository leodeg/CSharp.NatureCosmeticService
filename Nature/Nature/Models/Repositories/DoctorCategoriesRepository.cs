using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class DoctorCategoriesRepository : IRepository<DoctorCategory>
	{
		private readonly ApplicationDbContext context;

		public DoctorCategoriesRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<DoctorCategory> Get()
		{
			return context.DoctorCategories;
		}

		public DoctorCategory Get(int id)
		{
			return context.DoctorCategories.FirstOrDefault(item => item.Id == id);
		}

		public void Create(DoctorCategory item)
		{
			context.DoctorCategories.Add(item);
			context.SaveChanges();
		}

		public void Update(DoctorCategory item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.DoctorCategories.SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Title = item.Title;
			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.DoctorCategories.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.DoctorCategories.Remove(item);
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
