using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreBlogEntires.Data.Contexts;
using NetCoreBlogEntires.Data.Repositories;
using NetCoreBlogEntires.Identity;
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            // startup dosyas�n�n bulundu�u uygulamada ne kadar validator varsa net core mvc projesine tan�t.

        
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddIdentity<ApplicationUser, ApplicationRole>(opt => {
                // db de False alan� false olanlar login olamaz
                opt.SignIn.RequireConfirmedEmail = true;
                
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            // uygulamaya identity ile birlikte kimlik do�rulama servisi ekleriz
            services.AddAuthentication();



            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/Auth/Login");
                opt.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
                opt.Cookie = new CookieBuilder
                {
                    Name = "TestCookie", //Olu�turulacak Cookie'yi isimlendiriyoruz.
                    HttpOnly = false, // Https serfikam�z varsa cookie sald�r� yapacak kullan�c�lar er�imesin diye koydu�umuz ayar.
                };
                opt.SlidingExpiration = true; //Expiration s�resinin yar�s� kadar s�re zarf�nda istekte bulunulursa e�er geri kalan yar�s�n� tekrar s�f�rlayarak ilk ayarlanan s�reyi tazeleyecektir.
                                              // Rememberme false ise bu ayar ge�erli olamaz, cookie kal�c� olarak olu�mal�d�r. Oturum s�resince olu�mamal�d�r.
                opt.ExpireTimeSpan = TimeSpan.FromDays(40); // 40 g�nl�k 
            });


            services.AddScoped<IPostRepository,PostRepository>();
            services.AddScoped<IPostCommentCountRepo, PostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddSingleton<IEmailService, SendGridEmailService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
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
            // bu servisi aktif hale getiririz.
            app.UseAuthentication();
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
