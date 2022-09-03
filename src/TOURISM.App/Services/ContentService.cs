using TOURISM.App.DataAccess.Repositories.Abstracts;
using TOURISM.App.Services.Abstracts;
using System;
using System.Collections.Generic;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using System.Linq;

namespace TOURISM.App.Services
{
    public class ContentService : IContentService
    {
        private readonly ISettingsService _settingsService;
        private readonly IContentRepository _contentRepository;
        public ContentService(IContentRepository contentRepository, ISettingsService settingsService)
        {
            _contentRepository = contentRepository;
            _settingsService = settingsService;
        }

        public List<OntologyDTO> GetBaseContent()
        {
            var site = _settingsService.SiteInfo();

            var output = _contentRepository.GetOntologySubClasesByParent(site.OntologyDetails.RootEntity);
            return output;
        }
        public List<OntologyDTO> GetRootEntityChildren()
        {
            var site = _settingsService.SiteInfo();

            var output = GetClassContent(site.OntologyDetails.RootEntity, string.Empty, false);
            return output;
        }
        public List<OntologyDTO> GetClassContent(string itemClass, string individual = "", bool includeProperties = true)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();

            var superClasses = _contentRepository.GetOntologySuperClases();
            var superClassesList = superClasses.Select(row => row.Class).ToList();

            output = _contentRepository.GetOntologySubClasesByParent(itemClass);

            if (!string.IsNullOrWhiteSpace(individual))
                individual = $"FILTER( ?individual=<{individual}>)";

            foreach (var item in output)
            {
                var individuals = _contentRepository.GetOntologyIndividuals(item.Class, superClassesList, individual, includeProperties);
                if (individuals != null && individuals.Any())
                    item.Individuals = individuals;
            }

            return output;
        }
        public List<IndividualPropertiesDTO> GetIndividualContent(string individualName, string individualParent)
        {
            var output = Enumerable.Empty<IndividualPropertiesDTO>().ToList();

            try
            {
                var superClasses = _contentRepository.GetOntologySuperClases();
                var superClassesList = superClasses.Select(row => row.Class).ToList();

                output = _contentRepository.GetOntologyIndividuals(individualParent, superClassesList, additionalFilter: $"FILTER( ?individual=<{individualName}>)");
            }
            catch (Exception ex)
            {
            }

            return output;
        }
        public List<OntologyDTO> GetFullOntology()
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();

            try
            {
                var superClasses = _contentRepository.GetOntologySuperClases();
                var allSubClasses = _contentRepository.GetOntologySubClases();

                output = ProcessOntologyElements(superClasses, allSubClasses);

                var superClassesList = superClasses.Select(row => row.Class).ToList();

                foreach (var item in output)
                {
                    var individuals = _contentRepository.GetOntologyIndividuals(item.Class, superClassesList);
                    if (individuals != null && individuals.Any())
                        item.Individuals = individuals;
                }
            }
            catch (Exception ex)
            {
            }
            return output;
        }

        #region Helper
        private static List<OntologyDTO> ProcessOntologyElements(List<OntologyDTO> classes, List<OntologyDTO> subClasses)
        {
            foreach (var item in classes)
            {
                item.Children = subClasses.Where(row => row.Parent == item.Class).ToList();
                foreach (var item1 in item.Children)
                {
                    item1.Children = subClasses.Where(row => row.Parent == item1.Class).ToList();
                    foreach (var item2 in item1.Children)
                    {
                        item2.Children = subClasses.Where(row => row.Parent == item2.Class).ToList();
                        foreach (var item3 in item2.Children)
                            item3.Children = subClasses.Where(row => row.Parent == item3.Class).ToList();
                    }
                }
            }
            return classes;
        }
        #endregion
    }
}
