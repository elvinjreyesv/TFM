using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.Enums;
using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using TOURISM.App.Models.ViewModels.Shared;
using TOURISM.App.Models.ViewModels.Shared.Content.Outputs;
using TOURISM.Web.Utils.Services.Abstracts;
using TOURISM.Web.Controllers;

namespace TOURISM.Web.Controllers.BaseController
{
    public class PublicBaseController : Controller
    {
        protected readonly AppSettings AppSettings;
        protected readonly IApiClientService ApiClient;
        protected readonly ILogService LogService;

        public PublicBaseController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService)
        {
            AppSettings = settings.Value;
            LocalizationManager = new LocalizationManager(Resources.Lang.ResourceManager);
            ApiClient = apiClient;
            LogService = logService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CultureTwoLetterName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            ViewBag.Title = AppSettings.Name;

            ViewBag.CultureTwoLetterName = CultureTwoLetterName;
            await base.OnActionExecutionAsync(context, next);
        }

        public LocalizationManager LocalizationManager { get; private set; }

        protected virtual string L(string name)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;

            return LocalizationManager.GetString(name, cultureInfo);
        }

        public string CultureThreeLetterName => (CultureTwoLetterName == "EN") ? "ENG" : "SPA";

        public string CultureTwoLetterName
        {
            get
            {
                var currentLanguage = Request.Cookies["CurrentLanguage"];
                return !string.IsNullOrWhiteSpace(currentLanguage) && currentLanguage == "ES" ? "ES" : "EN";
            }
            set
            {
                var cookie = Request.Cookies["CurrentLanguage"];
                var newcookie = new CookieOptions();
                if (cookie != null)
                    cookie = value;
                else
                {
                    newcookie.Expires = DateTime.Now.AddYears(1);
                }

                Response.Cookies.Append("CurrentLanguage", value, newcookie);
            }
        }

        public string IpAddress
        {
            get
            {
                var ipAddress = new System.Net.IPAddress(default(long));
                try
                {
                    ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
                }
                catch (Exception ex)
                {
                    ipAddress = new System.Net.IPAddress(default(long));
                }

                return ipAddress.ToString();
            }
        }

        public string AbsoluteBasePath(string path)
        {
            return AppSettings.BaseUrl.Substring(0, AppSettings.BaseUrl.Length - 1) + path;
        }

        protected async Task<ViewResult> Error(EErrorCodeClientApi section, Exception ex)
        {
            LogService.AddExceptionLog(section.ToString(), ex);
            return View("~/Views/Shared/Partials/_Error.cshtml", section);
        }

        public PublicContentInputDTO InputParameter 
        {
            get
            {
                return new PublicContentInputDTO()
                {
                    SiteId = AppSettings.SiteId,
                    IpAddressClient = IpAddress,
                    Lang = CultureThreeLetterName,
                    IpAddressService = IpAddress,
                    Token = string.Empty
                };
            }
        }
    }
}