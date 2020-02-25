using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class DoctorsRepository : IRepository<Doctor>
	{
		private readonly ApplicationDbContext context;
		private readonly IWebHostEnvironment _environment;

		public DoctorsRepository(ApplicationDbContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			_environment = environment;
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
			if (item.Contacts != null)
				context.Contacts.Add(item.Contacts);

			context.Doctors.Add(item);
			context.SaveChanges();
		}

		public void Update(Doctor item)
		{
			if (item == null)
				throw new ArgumentNullException();

			var oldItem = context.Doctors.Include(c => c.Contacts).Include(c => c.DoctorCategory).SingleOrDefault(i => i.Id == item.Id);

			if (oldItem == null)
				throw new ArgumentOutOfRangeException("Can't find and update item with id: " + item.Id);

			oldItem.FirstName = item.FirstName;
			oldItem.LastName = item.LastName;
			oldItem.MiddleName = item.MiddleName;
			oldItem.Description = item.Description;

			if (!string.IsNullOrWhiteSpace(item.ImagePath))
			{
				bool isEmpty = string.IsNullOrWhiteSpace(oldItem.ImagePath);
				bool isTheSameImage = string.Compare(oldItem.ImagePath, item.ImagePath) == 0;
				if (!isEmpty && !isTheSameImage)
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, oldItem.ImagePath, "doctors");
				}

				oldItem.ImagePath = item.ImagePath;
			}

			if (item.DoctorCategoryId != 0)
				oldItem.DoctorCategoryId = item.DoctorCategoryId;

			if (oldItem.Contacts != null && item.Contacts != null)
			{
				oldItem.Contacts.Phone = item.Contacts.Phone;
				oldItem.Contacts.Email = item.Contacts.Email;
				oldItem.Contacts.Address = item.Contacts.Address;
				oldItem.Contacts.Country = item.Contacts.Country;
				oldItem.Contacts.City = item.Contacts.City;
			}
			else if (item.Contacts != null)
			{
				oldItem.ContactsId = item.Contacts.Id;
				oldItem.Contacts = item.Contacts;
				context.Contacts.Add(item.Contacts);
			}

			context.SaveChanges();
		}

		public bool Delete(int id)
		{
			var item = context.Doctors.SingleOrDefault(item => item.Id == id);
			if (item != null)
			{
				var contacts = context.Contacts.SingleOrDefault(i => i.Id == item.ContactsId);
				if (contacts != null)
					context.Contacts.Remove(contacts);

				if (!string.IsNullOrEmpty(item.ImagePath))
				{
					ServerFiles.DeleteImageFromLocalFiles(_environment.WebRootPath, item.ImagePath, "doctors");
				}

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
