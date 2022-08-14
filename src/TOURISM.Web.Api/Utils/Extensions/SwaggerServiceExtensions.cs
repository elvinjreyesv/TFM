using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using TOURISM.App.Infrastructure.Utils.Constants;
using TOURISM.Web.Api.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace TOURISM.Web.Api.Utils.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(SwaggerConstants.SWAGGER_DOC_WEB_KEY, 
                new OpenApiInfo
                { 
                    Title = SwaggerConstants.SWAGGER_DOC_WEB_DESCRIPTION, 
                    Version = SwaggerConstants.SWAGGER_DOC_WEB_KEY 
                });

                setupAction.DocInclusionPredicate(DocInclusion);

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                setupAction.OperationFilter<SwaggerParamsExcludeFilter>();
                setupAction.OperationFilter<AuthOperationFilter>();

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (System.IO.File.Exists(xmlPath))
                    setupAction.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(setupAction =>
            {
                setupAction.RouteTemplate = "/docs/{documentName}/docs.json";
            });

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.DocumentTitle = "API - Test Suite";
                setupAction.SwaggerEndpoint($"/docs/{SwaggerConstants.SWAGGER_DOC_WEB_KEY}/docs.json", SwaggerConstants.SWAGGER_DOC_WEB_DESCRIPTION);
                setupAction.RoutePrefix = "";
            });

            return app;
        }

        #region Utils
        private static bool DocInclusion(string document, ApiDescription apiDescription)
        {
            switch (document)
            {
                default:
                    return true;
            }
        }

        #endregion
    }
}
