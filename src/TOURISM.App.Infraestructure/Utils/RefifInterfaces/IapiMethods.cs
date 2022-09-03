using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TOURISM.App.Models.ViewModels.Shared;
using TOURISM.App.Models.ViewModels.Shared.Content.Outputs;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;

namespace TOURISM.App.Infrastructure.Utils.RefifInterfaces
{
    public interface IApiMethods
    {
        #region Page Content
        #region GET
        [Get("/api/PageContent?SiteId={siteId}&Lang={lang}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<OntologyDTO>> GetBaseContent(string siteId, string lang, string ipAddress, string token);

        [Get("/api/PageContent/ClassContent?ItemClass={itemClass}&Individual={individual}&SiteId={siteId}&Lang={lang}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<OntologyDTO>> GetClassContent(string itemClass, string individual, string siteId, string lang, string ipAddress, string token);

        [Get("/api/PageContent/IndividualContent?ItemClass={itemClass}&ParentClass={parentClass}&SiteId={siteId}&Lang={lang}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<IndividualPropertiesDTO>> GetIndividualContent(string itemClass, string parentClass, string siteId, string lang, string ipAddress, string token);

        [Get("/api/PageContent/FullOntology?SiteId={siteId}&Lang={lang}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<OntologyDTO>> GetFullOntology(string siteId, string lang, string ipAddress, string token);

        [Get("/api/PageContent/RootEntityChildren?SiteId={siteId}&Lang={lang}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<OntologyDTO>> GetRootEntityChildren(string siteId, string lang, string ipAddress, string token);
        #endregion
        #endregion
    }
}
