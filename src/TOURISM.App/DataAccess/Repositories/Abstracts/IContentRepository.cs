using System.Collections.Generic;
using System.Threading.Tasks;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;

namespace TOURISM.App.DataAccess.Repositories.Abstracts
{
    public interface IContentRepository
    {
        List<OntologyDTO> GetOntologySuperClases();
        List<OntologyDTO> GetOntologySubClases();
        List<OntologyDTO> GetAnyOntologyElement(string parentClass);
        List<OntologyDTO> GetOntologySubClasesByParent(string parentClass);
        List<IndividualPropertiesDTO> GetOntologyIndividuals(string parentClass, List<string> superClasses, string additionalFilter = "");
    }
}
