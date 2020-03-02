using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Nature.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nature.Models;

namespace Nature
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
				Configuration.GetConnectionString("DefaultConnection")));

			AddIdentityProperties(services);
			AddRepositories(services);

			services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddMvc();
		}

		private static void AddIdentityProperties(IServiceCollection services)
		{
			services.AddIdentity<IdentityUser, IdentityRole>()
					.AddDefaultTokenProviders()
					.AddDefaultUI()
					.AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<IdentityOptions>(options => {
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 5;
				options.Password.RequireNonAlphanumeric = true;
				options.User.RequireUniqueEmail = true;
			});
		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddTransient<IRepository<Contacts>, ContactsRepository>();

			services.AddTransient<IRepository<ServiceCategory>, ServiceCategoriesRepository>();
			services.AddTransient<IRepository<Service>, ServicesRepository>();

			services.AddTransient<IRepository<DoctorCategory>, DoctorCategoriesRepository>();
			services.AddTransient<IRepository<Doctor>, DoctorsRepository>();

			services.AddTransient<IRepository<News>, NewsRepository>();
			services.AddTransient<IRepository<AboutUs>, AboutUsRepository>();
			services.AddTransient<IRepository<ContactUs>, ContactUsRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
