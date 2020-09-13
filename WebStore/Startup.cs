using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => { option.Filters.Add(new SimpleActionFilter()); });
            
            services.AddDbContext<WebStoreContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductService, SqlProductService>();

            services.AddSingleton<IEmployeesService, InMemoryEmployeesService>();

            services.AddSingleton<IPhoneService, InMemoryPhonesService>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to / Account / Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to / Account / Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to / Account / AccessDenied
                options.SlidingExpiration = true;
            });
        

    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute(); //краткий аналог
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //Маршрут по умолчанию состоит из трёх частей разделённыйх "/"
                //Первой частью указывается имя контроллера,
                //Второй - имя действия (метода) в контроллере,
                //Третьй - опциональный параметр с именем "id"
                //Если часть не указана - используется значение по умолчанию:
                //для контроллера имя "Home",
                //для действия - "Index"

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync(hello);
                //});
            });
        }
    }
}
