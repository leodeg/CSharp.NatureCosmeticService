using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class DoctorsRepository : IRepository<Doctor>
	{
		private readonly ApplicationDbContext context;

		public DoctorsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Doctor> Get()
		{
			return context.Doctors.Include(c => c.DoctorCategory).Include(c => c.Contacts);
		}

		public Doctor Get(int id)
		{
			return context.Doctors.Include(c => c.DoctorCategory).Include(c => c.Contacts).FirstOrDefault(item => item.Id == id);
		}

		public void Create(Doctor item)
		{
			context.Doctors.Add(item);
			context.SaveChanges();
		}

		public void Update(Doctor item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.Doctors.Include(c => c.Contacts).Include(c => c.DoctorCategory).SingleOrDefault(item => item.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.FirstName = item.FirstName;
			oldItem.LastName = item.LastName;
			oldItem.MiddleName = item.MiddleName;
			oldItem.Description = item.Description;
			oldItem.Image = item.Image;

			oldItem.DoctorCategoryId = item.DoctorCategoryId;
			oldItem.DoctorCategory = item.DoctorCategory;

			if (oldItem.Contacts != null && item.Contacts != null)
			{
				UpdateContacts(oldItem.Contacts, item.Contacts);
			}
			else
			{
				oldItem.ContactsId = item.ContactsId;
				oldItem.Contacts = item.Contacts;
			}

			context.SaveChanges();
		}

		private void UpdateContacts(Contacts oldInfo, Contacts newInfo)
		{
			oldInfo.Phone = newInfo.Phone;
			oldInfo.Email = newInfo.Email;
			oldInfo.Address = newInfo.Address;
			oldInfo.Country = newInfo.Country;
			oldInfo.City = newInfo.City;
		}

		public bool Delete(int id)
		{
			var item = context.Doctors.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				context.Doctors.Remove(item);
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
