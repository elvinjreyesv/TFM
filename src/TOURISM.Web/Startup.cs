using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Infrastructure.Utils.Constants;
using TOURISM.Web.Resources;
using TOURISM.Web.Utils.Services.Abstracts;
using TOURISM.Web.Utils.Services;

namespace TOURISM.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(AppSettingsByEnviroment(env), optional: true, reloadOnChange: true)
                .AddJsonFile("routesettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        private string AppSettingsByEnviroment(IHostEnvironment env)
        {
            if (env.IsProduction())
                return "appsettings.json";

            if (env.IsStaging())
                return "appsettings.Staging.json";

            if (env.IsEnvironment(EnviromentConstants.Local))
                return "appsettings.Local.json";

            return "appsettings.Development.json";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(Lang)));

            services.AddHttpContextAccessor();

            services.Configure<HtmlHelperOptions>(o => o.ClientValidationEnabled = true);

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<RouteSettings>(Configuration.GetSection("RouteSettings"));

            services.AddScoped<IApiClientService, ApiClientService>();
            services.AddScoped<ILogService, LogElmahService>();
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
