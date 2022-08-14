using Microsoft.AspNetCore.Mvc.Filters;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;
using TOURISM.App.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOURISM.Web.Api.Filters.Public
{
    public class PublicPostSecurityActionFilter : Attribute, IActionFilter
    {
        private readonly ISettingsService _settingsService;
        public PublicPostSecurityActionFilter(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var key in context.ActionArguments.Keys)
            {
                if (!(context.ActionArguments[key] is ClientTCInputDTO dto)) continue;

                dto.IpAddressService = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1";

                if (!(_settingsService.InfoSite(dto.SiteId) is SiteInfo info)) continue;

                dto.SiteId = info.SiteId;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
