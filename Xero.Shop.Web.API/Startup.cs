using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Xero.Shop.Repositories;
using Xero.Shop.Repositories.Products;
using Xero.Shop.Services.Products;
using Xero.Shop.Services.Products.Validators;



namespace Xero.Shop.Web.API
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
			services.AddDbContext<ShopDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("ShopDbContext"))
			);

			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IOptionService, OptionService>();

			//Validations
			services.AddScoped<IUniqueProductValidation, UniqueProductValidation>();
			services.AddScoped<IProductIdValidation, ProductIdValidation>();

			services.AddSwaggerGen();

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseMiddleware<ExceptionHandlerMiddleware>();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
