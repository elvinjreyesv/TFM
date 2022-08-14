using System.Collections.Generic;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using TOURISM.App.Models.ViewModels.Shared;

namespace TOURISM.App.Services.Abstracts
{
    public interface IContentService
    {
        List<OntologyDTO> GetBaseContent();
        List<OntologyDTO> GetClassContent(string itemClass);
        List<IndividualPropertiesDTO> GetIndividualContent(string individualName, string individualParent);
        List<OntologyDTO> GetFullOntology();
    }
}
