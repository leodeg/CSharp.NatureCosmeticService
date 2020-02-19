using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ContactsRepository : IRepository<Contacts>
	{
		private readonly ApplicationDbContext context;

		public ContactsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Contacts> Get()
		{
			return context.Contacts;
		}

		public Contacts Get(int id)
		{
			return context.Contacts.FirstOrDefault(item => item.Id == id);
		}

		public void Create(Contacts item)
		{
			context.Contacts.Add(item);
			context.SaveChanges();
		}

		public void Update(Contacts item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.Contacts.SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.Id = item.Id;
			oldItem.Phone = item.Phone;
			oldItem.Email = item.Email;
			oldItem.Address = item.Address;
			oldItem.Country = item.Country;
			oldItem.City = item.City;

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.Contacts.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.Contacts.Remove(item);
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
