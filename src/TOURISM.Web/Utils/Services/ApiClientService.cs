using Microsoft.Extensions.Options;
using Refit;
using TOURISM.Web.Utils.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Infrastructure.Utils.RefifInterfaces;
using TOURISM.App.Models.Enums;
using TOURISM.App.Models.ViewModels.Shared.Content.Outputs;
using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;

namespace TOURISM.Web.Utils.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly AppSettings _appSettings;

        public ApiClientService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public IApiMethods Rest => RestService.For<IApiMethods>(_appSettings.WebApiBaseUrl);
        public IApiMethods RestAuth(string token) => RestService.For<IApiMethods>(_appSettings.WebApiBaseUrl, new RefitSettings()
        {
            AuthorizationHeaderValueGetter = () => Task.FromResult(token)
        });
        public IApiMethods RestAuthXML(string token) => RestService.For<IApiMethods>(_appSettings.WebApiBaseUrl, new RefitSettings()
        {
            AuthorizationHeaderValueGetter = () => Task.FromResult(token),
            ContentSerializer = new XmlContentSerializer()
        });

        #region Content
        public async Task<List<OntologyDTO>> GetBaseContent(PublicContentInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetBaseContent(dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
        }
        public async Task<List<OntologyDTO>> GetClassContent(PublicContentInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetClassContent(dto.ItemClass, dto.Individual, dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
        }
        public async Task<List<IndividualPropertiesDTO>> GetIndividualContent(PublicContentInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetIndividualContent(dto.ItemClass, dto.ParentClass, dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
        }
        public async Task<List<OntologyDTO>> GetFullOntology(PublicContentInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetFullOntology(dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
        }
        public async Task<List<OntologyDTO>> GetRootEntityChildren(PublicContentInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetRootEntityChildren(dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
        }
        #endregion
    }
}
