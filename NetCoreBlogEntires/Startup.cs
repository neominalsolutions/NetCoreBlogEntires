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
            // mvc uygulamasýndaki validayon kontrolü için fluent validation kontrolüde kullan.
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            // startup dosyasýnýn bulunduðu uygulamada ne kadar validator varsa net core mvc projesine tanýt.

        
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddIdentity<ApplicationUser, ApplicationRole>(opt => {
                // db de False alaný false olanlar login olamaz
                opt.SignIn.RequireConfirmedEmail = true;
                
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            // uygulamaya identity ile birlikte kimlik doðrulama servisi ekleriz
            services.AddAuthentication();



            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/Auth/Login");
                opt.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
                opt.Cookie = new CookieBuilder
                {
                    Name = "TestCookie", //Oluþturulacak Cookie'yi isimlendiriyoruz.
                    HttpOnly = false, // Https serfikamýz varsa cookie saldýrý yapacak kullanýcýlar erþimesin diye koyduðumuz ayar.
                };
                opt.SlidingExpiration = true; //Expiration süresinin yarýsý kadar süre zarfýnda istekte bulunulursa eðer geri kalan yarýsýný tekrar sýfýrlayarak ilk ayarlanan süreyi tazeleyecektir.
                                              // Rememberme false ise bu ayar geçerli olamaz, cookie kalýcý olarak oluþmalýdýr. Oturum süresince oluþmamalýdýr.
                opt.ExpireTimeSpan = TimeSpan.FromDays(40); // 40 günlük 
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
