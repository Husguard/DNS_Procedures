using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddSingleton<IConnectionDb,ConnectionDb>();
            services.AddSingleton<ITaskRepository,TaskRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IThemeRepository, ThemeRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();

            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddHttpContextAccessor();
            services.AddScoped<UserManager>();

           services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => //CookieAuthenticationOptions
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

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
