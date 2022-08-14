using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using TOURISM.App.Models.ViewModels.Shared.Content.Outputs;
using System.Threading.Tasks;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using System.Collections.Generic;

namespace TOURISM.Web.Utils.Services.Abstracts
{
    public interface IApiClientService
    {
        #region Public
        #region Content
        #region GET
        Task<List<OntologyDTO>> GetBaseContent(PublicContentInputDTO dto, string secretKey);
        Task<List<OntologyDTO>> GetClassContent(PublicContentInputDTO dto, string secretKey);
        Task<List<IndividualPropertiesDTO>> GetIndividualContent(PublicContentInputDTO dto, string secretKey);
        Task<List<OntologyDTO>> GetFullOntology(PublicContentInputDTO dto, string secretKey);
        #endregion
        #endregion
        #endregion
    }
}
