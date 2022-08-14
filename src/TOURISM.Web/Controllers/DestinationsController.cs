using TOURISM.App.Infrastructure.Utils;
using TOURISM.Web.Utils.Services.Abstracts;
using TOURISM.Web.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using TOURISM.App.Models.Enums;
using Newtonsoft.Json;
using System.Linq;

namespace TOURISM.Web.Controllers
{
    public class DestinationsController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public DestinationsController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService, ILogger<HomeController> logger) : base(settings, apiClient, logService)
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

        public async Task<IActionResult> Detail([FromQuery] string destination)
        {
            try
            {
                var input = InputParameter;
                input.ItemClass = string.Concat(AppSettings.OntologyPrefix, destination);
                var result = await ApiClient.GetClassContent(input, AppSettings.SecretKey);

                if (result.Count > 1)
                    result = result.Where(row => row.Class == input.ItemClass).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.DestinationController, ex);
            }
        }
    }
}
