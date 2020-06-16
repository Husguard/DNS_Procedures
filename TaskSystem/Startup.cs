using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskSystem.Models;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Objects.Repositories;
using TaskSystem.Models.Options;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Implementations;
using TaskSystem.Models.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;

namespace TaskSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStringOptions>((settings) =>
            {
                Configuration.GetSection("ConnectionStrings").Bind(settings);
            });
            services.AddControllersWithViews();
            services.AddSingleton<IConnectionDb,ConnectionDb>();
            services.AddSingleton<ITaskRepository,TaskRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IThemeRepository, ThemeRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ICommentService, CommentService>();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
