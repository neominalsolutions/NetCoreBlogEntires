using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Models;
using NetCoreBlogEntires.Services;
using NetCoreBlogEntires.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires
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
            // mvc uygulamas�ndaki validayon kontrol� i�in fluent validation kontrol�de kullan.
            services.AddControllersWithViews().AddFluentValidation();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPostRepository,PostRepository>();
            services.AddScoped<IPostCommentCountRepo, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostService, PostService>();

            // FluentValidation ile kulland���m�z servisleri Transient olarak i�aretleriz.��nk� her bir post istedi�inde validayon kontrol� tekrar tekrar s�f�rdan yapaml�y�z.
            services.AddTransient<ContactInputModelValidator>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
        "Admin",
        "Admin",
        "Admin/{controller=Post}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
