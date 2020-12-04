using AutoMapper;
using CicekSepeti.API.CustomException;
using CicekSepeti.API.Filter;
using CicekSepeti.DataAccess.UnitOfWork;
using CicekSepeti.Domain.Context;
using CicekSepeti.Domain.Entities;
using CicekSepeti.Domain.IContext;
using CicekSepeti.Service.IServices;
using CicekSepeti.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CicekSepeti.API
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
            services.AddScoped<IUnitOfWork, CicekSepetiUnitOfWork>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();

            services.AddDbContext<CicekSepetiDbContext>(opts =>
           opts.UseInMemoryDatabase("CicekSepetiDB"));

            services.AddScoped<ICicekSepetiDbContext>(provider => provider.GetService<CicekSepetiDbContext>());

            var context = services.BuildServiceProvider()
                       .GetService<CicekSepetiDbContext>();

            AddTestData(context);

            services.AddControllers();

            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMiddleware<ExceptionMiddleware>();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static void AddTestData(CicekSepetiDbContext context)
        {
            var testUser1 = new User
            {
                Id = new Guid("efe997f6-823e-4ad2-a7df-22ff0ab59eac"),
                Name = "Cihan",
                Password = "Skywalker"
            };
            var testUser2 = new User
            {
                Id = new Guid("60e709f3-4f3b-4833-838d-296a9e345b6d"),
                Name = "Bulut",
                Password = "Skywalker"
            };
            var testUser3 = new User
            {
                Id = new Guid("bc1bf525-5234-467f-b743-7241487bc3d2"),
                Name = "Akkurt",
                Password = "Skywalker"
            };

            context.Users.Add(testUser1);
            context.Users.Add(testUser2);
            context.Users.Add(testUser3);

            var testPost1 = new Product
            {
                Id = new Guid("91ac884f-815b-4410-a479-a43f71316e20"),
                Name = "Laptop",
                Count = 20,
                Price = 3
            };
            var testPost2 = new Product
            {
                Id = new Guid("e5f74a86-3bf4-4c65-8e94-15575fbe6bd4"),
                Name = "Telefon",
                Count = 2,
                Price = 3
            };
            var testPost3 = new Product
            {
                Id = new Guid("c3589c8b-ec40-492d-911c-4f7ade2cc180"),
                Name = "Mause",
                Count = 2,
                Price = 3
            };
            var testPost4 = new Product
            {
                Id = new Guid("48f90b81-3c8b-415b-b697-fe9df50e4e96"),
                Name = "Ekran",
                Count = 2,
                Price = 3
            };
            var testPost5 = new Product
            {
                Id = new Guid("39d071de-ff88-43de-8fa7-1d76ba3a0509"),
                Name = "TV",
                Count = 2,
                Price = 3
            };
            context.Products.Add(testPost1);
            context.Products.Add(testPost2);
            context.Products.Add(testPost3);
            context.Products.Add(testPost4);
            context.Products.Add(testPost5);

            context.SaveChanges();
        }
    }
}
