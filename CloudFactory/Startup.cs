using System.IO;
using CloudFactory.BLL.Interfaces;
using CloudFactory.BLL.Models;
using CloudFactory.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CloudFactory
{
	public class Startup
	{
		public IConfiguration CustomConfiguration { get; }

		public IConfiguration CommonConfiguration { get; }

		public Startup(IConfiguration configuration)
		{
			CommonConfiguration = configuration;

			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("application.json", true, true);
			CustomConfiguration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<SiteSettings>(CustomConfiguration.GetSection("siteSettings"));

			services.AddControllersWithViews();

			services.AddControllersWithViews().AddNewtonsoftJson();

			services.AddMvc(option =>
			{
				option.EnableEndpointRouting = false;
			});

			#region custom services

			services.AddTransient<IFilesLoaderService, FilesLoaderService>();

			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
