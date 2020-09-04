using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            services.AddSingleton<IEmployeesService, InMemoryEmployeesService>();

            services.AddSingleton<IPhoneService, InMemoryPhonesService>();

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

            

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute(); //������� ������
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //������� �� ��������� ������� �� ��� ������ ����������� "/"
                //������ ������ ����������� ��� �����������,
                //������ - ��� �������� (������) � �����������,
                //������ - ������������ �������� � ������ "id"
                //���� ����� �� ������� - ������������ �������� �� ���������:
                //��� ����������� ��� "Home",
                //��� �������� - "Index"

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync(hello);
                //});
            });
        }
    }
}
