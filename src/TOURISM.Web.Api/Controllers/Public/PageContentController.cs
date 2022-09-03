using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using TOURISM.App.Infraestructure.Utils.Constants;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.Enums;
using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;
using TOURISM.App.Models.ViewModels.Shared;
using TOURISM.App.Services.Abstracts;
using TOURISM.Web.Api.Filters.Public;

namespace TOURISM.Web.Api.Controllers.Public
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(PublicSecurityActionFilter), Order = 1)]
    [TypeFilter(typeof(PublicPostSecurityActionFilter), Order = 2)]
    public class PageContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly WebApiAppSettings _settings;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;
        public PageContentController(IContentService contentService, IOptions<WebApiAppSettings> options, IMemoryCache memoryCache)
        {
            _contentService = contentService;
            _settings = options.Value;
            _memoryCache = memoryCache;
            cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_settings.CacheConfiguration.AbsoluteExpirationSeconds),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromSeconds(_settings.CacheConfiguration.SlidingExpirationSeconds)
            };

        }

        [HttpGet]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> Get([FromQuery] ClientTCInternationalizationInputDTO input = null)
        {
            if (!_memoryCache.TryGetValue(CacheKeyConstants.Ontology, out List<OntologyDTO> content))
            {
                content = _contentService.GetBaseContent();
                _memoryCache.Set(CacheKeyConstants.Ontology, content, cacheExpiryOptions);
            }
            return Ok(content);
        }

        [HttpGet("ClassContent")]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> ClassContent([FromQuery] PublicContentInputDTO input)
        {
            var cacheKey = $"{CacheKeyConstants.ClassContent}_{input.ItemClass}_{input.Individual}";
            if (!_memoryCache.TryGetValue(cacheKey, out List<OntologyDTO> content))
            {
                content = _contentService.GetClassContent(input.ItemClass, input.Individual);
                _memoryCache.Set(cacheKey, content, cacheExpiryOptions);
            }
            return Ok(content);
        }

        [HttpGet("IndividualContent")]
        [Produces(typeof(List<IndividualPropertiesDTO>))]
        public async Task<ActionResult<List<IndividualPropertiesDTO>>> IndividualContent([FromQuery] PublicContentInputDTO input)
        {
            var cacheKey = $"{CacheKeyConstants.IndividualContent}_{input.ItemClass}";
            if (!_memoryCache.TryGetValue(cacheKey, out List<IndividualPropertiesDTO> content))
            {
                content = _contentService.GetIndividualContent(input.ItemClass, input.ParentClass);
                _memoryCache.Set(cacheKey, content, cacheExpiryOptions);
            }
            return Ok(content);
        }

        [HttpGet("RootEntityChildren")]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> RootEntityChildren([FromQuery] ClientTCInternationalizationInputDTO input)
        {
            if (!_memoryCache.TryGetValue(CacheKeyConstants.OntologySuperClasses, out List<OntologyDTO> content))
            {
                content = _contentService.GetRootEntityChildren();
                _memoryCache.Set(CacheKeyConstants.OntologySuperClasses, content, cacheExpiryOptions);
            }
            return Ok(content);
        }

        [HttpGet("FullOntology")]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> FullOntology([FromQuery] ClientTCInternationalizationInputDTO input)
        {
            if (!_memoryCache.TryGetValue(CacheKeyConstants.FullOntology, out List<OntologyDTO> content))
            {
                content = _contentService.GetFullOntology();
                _memoryCache.Set(CacheKeyConstants.FullOntology, content, cacheExpiryOptions);
            }
            return Ok(content);
        }
    }
}