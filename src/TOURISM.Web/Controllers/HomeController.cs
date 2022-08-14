using TOURISM.App.Infrastructure.Utils;
using TOURISM.Web.Utils.Services.Abstracts;
using TOURISM.Web.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TOURISM.App.Models.ViewModels.Shared;
using System.Threading.Tasks;
using System;
using TOURISM.App.Models.Enums;
using Newtonsoft.Json;

namespace TOURISM.Web.Controllers
{
    public class HomeController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService, ILogger<HomeController> logger) : base(settings, apiClient, logService)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await ApiClient.GetBaseContent(InputParameter, AppSettings.SecretKey);

                return View(result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        public IActionResult Ontology()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string returnUrl, string newLang)
        {
            if (string.IsNullOrWhiteSpace(newLang) || newLang == CultureTwoLetterName)
                return Json(AppResponseAjax.CreateTarget(returnUrl, false));

            CultureTwoLetterName = newLang;
            return Json(AppResponseAjax.CreateTarget(returnUrl, false));
        }
    }
}
