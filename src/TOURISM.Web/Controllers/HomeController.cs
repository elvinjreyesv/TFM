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
using System.Linq;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using TOURISM.Web.Utils.Extensions;
using TOURISM.App.Infrastructure.Utils.Constants;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime;
using TOURISM.App.Infraestructure.Utils.Constants;
using TOURISM.App.Infrastructure.Extensions;

namespace TOURISM.Web.Controllers
{
    public class HomeController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;
        public HomeController(IOptions<AppSettings> settings, IApiClientService apiClient, ILogService logService, ILogger<HomeController> logger, IMemoryCache memoryCache) : base(settings, apiClient, logService)
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

        [HttpGet("BaseChildren")]
        public async Task<JsonResult> BaseChildren()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CacheKeyConstants.BaseChildren, out List<ChildrenOutputDTO> output))
                {
                    var response = ApiClient.GetRootEntityChildren(InputParameter, AppSettings.SecretKey);
                    output = Enumerable.Empty<ChildrenOutputDTO>().ToList();

                    foreach (var item in response.Result)
                    {
                        foreach (var city in item.Individuals)
                        {
                            output.Add(new ChildrenOutputDTO()
                            {
                                Parent = item.Parent.ReplaceOntology(),
                                Class = item.Class.ReplaceOntology(),
                                City = city.IndividualName.ReplaceOntology()
                            });
                        }
                    }

                    output = output.GroupBy(row => row.City).Select(row => new ChildrenOutputDTO()
                    {
                        City = row.Key,
                        Class = row.FirstOrDefault().Class,
                        Parent = row.FirstOrDefault().Parent
                    }).ToList();

                    _memoryCache.Set(CacheKeyConstants.BaseChildren, output, cacheExpiryOptions);
                }
                return Json(AppResponseAjax.Create(true, new { response = output }));
            }
            catch (Exception ex)
            {
                return Json(AppResponseAjax.Create(false, new { response = "" }));
            }
        }

        public IActionResult Ontology()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string returnUrl, string newLang)
        {
            returnUrl = returnUrl.Replace("&amp;", "&");
            if (string.IsNullOrWhiteSpace(newLang) || newLang == CultureTwoLetterName)
                return Json(AppResponseAjax.CreateTarget(returnUrl, false));

            CultureTwoLetterName = newLang;
            return Json(AppResponseAjax.CreateTarget(returnUrl, false));
        }
    }
}
