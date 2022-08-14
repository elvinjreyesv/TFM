using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TOURISM.App.Services;
using TOURISM.App.Services.Abstracts;
using TOURISM.Web.Api.Utils.Extensions;
using TOURISM.Web.Api.Utils.Middlewares;
using TOURISM.App.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TOURISM.App.DataAccess.Repositories.Abstracts;
using TOURISM.App.DataAccess.Repositories;

namespace TOURISM.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<WebApiAppSettings>(Configuration.GetSection("AppSettings"));

            var appSettings = Configuration.GetSection("AppSettings").Get<WebApiAppSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(setupAction =>
                {
                    setupAction.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidIssuer = appSettings.JwtConfig.ValidIssuer,
                        ValidAudience = appSettings.JwtConfig.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtConfig.SigningKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.RespectBrowserAcceptHeader = true;
                setupAction.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

            }).AddXmlSerializerFormatters();
            services.AddHttpContextAccessor();
            services.AddSwaggerDocumentation();

            #region Repositories
            services.AddScoped<IContentRepository, ContentRepository>();
            #endregion

            #region Services
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISettingsService, SettingsService>();
            #endregion 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsStaging() || env.IsProduction())
            {
                app.UseMiddleware<RequestResponseLoggingMiddleware>();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
