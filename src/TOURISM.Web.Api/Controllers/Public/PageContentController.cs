using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;
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
        public PageContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> Get([FromQuery] ClientTCInternationalizationInputDTO input = null)
        {
            var content = _contentService.GetBaseContent();
            return Ok(content);
        }

        [HttpGet("ClassContent")]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> ClassContent([FromQuery] PublicContentInputDTO input)
        {
            var content = _contentService.GetClassContent(input.ItemClass);
            return Ok(content);
        }

        [HttpGet("IndividualContent")]
        [Produces(typeof(List<IndividualPropertiesDTO>))]
        public async Task<ActionResult<List<IndividualPropertiesDTO>>> IndividualContent([FromQuery] PublicContentInputDTO input)
        {
            var content = _contentService.GetIndividualContent(input.ItemClass, input.ParentClass);
            return Ok(content);
        }

        [HttpGet("FullOntology")]
        [Produces(typeof(List<OntologyDTO>))]
        public async Task<ActionResult<List<OntologyDTO>>> FullOntology([FromQuery] ClientTCInternationalizationInputDTO input)
        {
            var content = _contentService.GetFullOntology();
            return Ok(content);
        }
    }
}