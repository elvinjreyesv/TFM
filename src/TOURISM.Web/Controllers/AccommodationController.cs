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
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;

namespace TOURISM.Web.Controllers
{
    public class AccommodationController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public AccommodationController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService, ILogger<HomeController> logger) : base(settings, apiClient, logService)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Detail([FromQuery] string destination, string accommodation, string type)
        {
            try
            {
                var output = new IndividualModelDTO();
                var input = InputParameter;
                input.ItemClass = string.Concat(AppSettings.OntologyPrefix, accommodation);
                input.ParentClass = string.Concat(AppSettings.OntologyPrefix, type);
                var result = await ApiClient.GetIndividualContent(input, AppSettings.SecretKey);

                output.Destination = destination;
                output.Individuals = result;

                return View(output);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.AccommodationController, ex);
            }
        }
    }
}
