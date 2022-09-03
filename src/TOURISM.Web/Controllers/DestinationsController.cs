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
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using System.Reflection;
using TOURISM.Web.Utils.Extensions;
using Microsoft.Extensions.Caching.Memory;
using TOURISM.App.Infraestructure.Utils.Constants;
using System.Collections.Generic;
using System.Data;

namespace TOURISM.Web.Controllers
{
    public class DestinationsController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;
        public DestinationsController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService, ILogger<HomeController> logger, IMemoryCache memoryCache) : base(settings, apiClient, logService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(3600),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromSeconds(3600)
            };
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

                var cacheKey = $"{CacheKeyConstants.OntologyClass}_{destination}";
                if (!_memoryCache.TryGetValue(cacheKey, out List<OntologyDTO> result))
                {
                    result = await ApiClient.GetClassContent(input, AppSettings.SecretKey);
                    if (result.Count > 1)
                        result = result.Where(row => row.Class == input.ItemClass).ToList();

                    foreach(var item in result)
                    {
                        foreach(var ind in item.Individuals)
                        {
                            ind.ValueList = string.Join(",", (ind.Properties.Where(row => row.Axioms != null)
                                .SelectMany(row => row.Axioms).Where(row => row.Values != null)
                                    .SelectMany(r => r.Values)
                                        .Select(row => row.Value.ReplaceOntology())).ToList());

                        }
                    }
                    
                    _memoryCache.Set(cacheKey, result, cacheExpiryOptions);
                }

                return View(result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.DestinationController, ex);
            }
        }

        public async Task<IActionResult> DestinationDetail([FromQuery] string type, string destination, string city)
        {
            try
            {
                var input = InputParameter;
                input.ItemClass = string.Concat(AppSettings.OntologyPrefix, destination);
                input.Individual = string.Concat(AppSettings.OntologyPrefix, city.Replace(" ", "_"));
                var result = await ApiClient.GetClassContent(input, AppSettings.SecretKey);

                var output = result.Where(row => row.Class == input.ItemClass).ToList();
                return View(output);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.AccommodationController, ex);
            }
        }
    }
}
