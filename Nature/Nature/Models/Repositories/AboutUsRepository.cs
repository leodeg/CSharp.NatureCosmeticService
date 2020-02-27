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
			return context.AboutUs.Include(c => c.Contacts);
		}

		public AboutUs Get(int id)
		{
			return context.AboutUs.Include(c => c.Contacts).FirstOrDefault(item => item.Id == id);
		}

		public void Create(AboutUs item)
		{
			if (item.Contacts != null)
				context.Contacts.Add(item.Contacts);

			context.AboutUs.Add(item);
			context.SaveChanges();
		}

		public void Update(AboutUs newItem)
		{
			if (newItem == null)
				throw new ArgumentNullException();

			var oldItem = context.AboutUs.Include(c => c.Contacts).SingleOrDefault(i => i.Id == newItem.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + newItem.Id);

			oldItem.Id = newItem.Id;
			oldItem.Title = newItem.Title;
			oldItem.Description = newItem.Description;

			UpdateContacts(newItem, oldItem);
			context.SaveChanges();
		}

		private void UpdateContacts(AboutUs newItem, AboutUs oldItem)
		{
			if (newItem.ContactsId > 0 && oldItem.Contacts == null)
			{
				context.Contacts.Add(newItem.Contacts);
				oldItem.ContactsId = newItem.ContactsId;
			}
			else if (oldItem.Contacts != null)
			{
				oldItem.Contacts.Email = newItem.Contacts.Email;
				oldItem.Contacts.Phone = newItem.Contacts.Phone;
				oldItem.Contacts.City = newItem.Contacts.City;
				oldItem.Contacts.Address = newItem.Contacts.Address;
				oldItem.Contacts.Country = newItem.Contacts.Country;

				oldItem.Contacts.Twitter = newItem.Contacts.Twitter;
				oldItem.Contacts.Facebook = newItem.Contacts.Facebook;
				oldItem.Contacts.VKontakte = newItem.Contacts.VKontakte;
				oldItem.Contacts.LinkedIn = newItem.Contacts.LinkedIn;
				oldItem.Contacts.YouTube = newItem.Contacts.YouTube;
				oldItem.Contacts.Website = newItem.Contacts.Website;
			}
		}

		public bool Delete(int id)
		{
			var item = context.AboutUs.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				var contacts = context.Contacts.SingleOrDefault(i => i.Id == item.ContactsId);
				if (contacts != null)
					context.Contacts.Remove(contacts);

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
