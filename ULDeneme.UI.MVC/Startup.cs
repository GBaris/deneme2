using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ULDeneme.BLL.Concrete;
using ULDeneme.DAL.Concrete;
using ULDeneme.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ULDeneme.BLL.Abstract;
using ULDeneme.DAL.Concrete.Context;
using ULDeneme.DAL.Concrete.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using ULDeneme.Model.Entities;

namespace ULDeneme.UI.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ULDenemeDbContext>(options => options.UseSqlServer("Server=OSMAN; Database=ULDenemeDB; Trusted_Connection=True; TrustServerCertificate=True"));
            services.AddTransient<ITranslationTypeDAL, TranslationTypeRepository>();
            services.AddTransient<ITranslationTypeBLL, TranslationTypeService>();
            services.AddTransient <IUserDAL, UserRepository>();
            services.AddTransient<IUserBLL, UserService>();
            services.AddTransient<ISozlukDAL, SozlukRepository>();
            services.AddTransient<ISozlukBLL, SozlukService>();
            services.AddTransient<IVocabularyDAL, VocabularyRepository>();
            services.AddTransient<IVocabularyBLL, VocabularyService>();
            //Kimlik doğrulama işlemleri için gerekli ayarlar
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
