using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nature.Models;

namespace Nature.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Contacts> Contacts { get; set; }
		public DbSet<DoctorCategory> DoctorCategories { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<ServiceCategory> ServiceCategories { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<AboutUs> AboutUs { get; set; }
		public DbSet<ContactUs> ContactUs { get; set; }
	}
}
