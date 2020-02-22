using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{

	public class ContactUsRepository : IRepository<ContactUs>
	{
		private readonly ApplicationDbContext context;

		public ContactUsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<ContactUs> Get()
		{
			return context.ContactUs;
		}

		public ContactUs Get(int id)
		{
			return context.ContactUs.FirstOrDefault(item => item.Id == id);
		}

		public void Create(ContactUs item)
		{
			context.ContactUs.Add(item);
			context.SaveChanges();
		}

		public void Update(ContactUs item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.ContactUs.SingleOrDefault(i => i.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.FirstName = item.FirstName;
			oldItem.LastName = item.LastName;
			oldItem.Phone = item.Phone;
			oldItem.Email = item.Email;
			oldItem.Description = item.Description;
			oldItem.HasBeenRead = item.HasBeenRead;

			context.SaveChanges();
		}

		public void SwitchIsContactWasReaded(int id)
		{
			var oldItem = context.ContactUs.SingleOrDefault(i => i.Id == id);
			oldItem.HasBeenRead = !oldItem.HasBeenRead;
			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.ContactUs.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.ContactUs.Remove(item);
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
