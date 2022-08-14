using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using TOURISM.App.Infrastructure.Utils.Constants;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace TOURISM.Web.Api.Filters
{
    public class SwaggerParamsExcludeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            (
                from operationParameter in operation.Parameters
                let contextParam = (DefaultModelMetadata)context.ApiDescription.ParameterDescriptions.FirstOrDefault(row => row.Name == operationParameter.Name)?.ModelMetadata
                let exist = contextParam?.Attributes?.Attributes?.Any(row => row is JsonIgnoreAttribute) ?? false
                where exist
                select operationParameter
            ).ToList().ForEach(parameter => operation.Parameters.Remove(parameter));
        }
    }

    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .Distinct();

            if (requiredScopes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                var oAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ oAuthScheme ] = requiredScopes.ToList()
                    }
                };
            }
        }
    }
}
