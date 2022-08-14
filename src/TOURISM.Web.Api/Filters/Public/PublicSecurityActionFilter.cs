using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.Enums;
using TOURISM.App.Models.ViewModels.Shared;
using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;
using TOURISM.App.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOURISM.Web.Api.Filters.Public
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PublicSecurityActionFilter : Attribute, IActionFilter
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthenticationService _authenticationService;

        public PublicSecurityActionFilter(ISettingsService settingsService, IAuthenticationService authenticationService)
        {
            this._settingsService = settingsService;
            this._authenticationService = authenticationService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            var input = context.ActionArguments.FirstOrDefault(row => row.Value is ClientTCInputDTO dto).Value as ClientTCInputDTO;
            if (input == null || string.IsNullOrWhiteSpace(input.SiteId))
            {
                context.Result = new UnauthorizedObjectResult(AppResponse.Create(EAppResponse.InvalidInput));
                return;
            }

            var pageValidated = false;
            foreach (var key in context.ActionArguments.Keys)
            {
                if (!(context.ActionArguments[key] is ClientTCInputDTO dto))
                    continue;

                dto.IpAddressService = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1";

                pageValidated = true;
                var isPageRequest = _authenticationService.IsPageRequest(context.ActionArguments[key], dto.SiteId, dto.Token);
                dto.SetDefaults();
                if (isPageRequest?.Status != EAppResponse.Success)
                    context.Result = new UnauthorizedObjectResult(isPageRequest);

                dto.SiteId = _settingsService.InfoSite(input.SiteId).Id;
                dto.IpAddressService = string.Empty;
                break;
            }

            if (!pageValidated)
            {
                context.Result = new UnauthorizedObjectResult(AppResponse.Create(EAppResponse.InvalidInput));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
