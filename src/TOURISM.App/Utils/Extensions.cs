using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;

namespace TOURISM.App.Utils
{
    public static class Extensions
    {
        public static List<IndividualAxiomDTO> FormatAxioms(this List<IndividualAxiomDTO> axioms, string elementClass)
        {
            var validationClass = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes";
            if (elementClass != validationClass)
                return axioms;

            if (axioms == null)
                axioms = Enumerable.Empty<IndividualAxiomDTO>().ToList();

            axioms.Add(new IndividualAxiomDTO()
            {
                Domain = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes",
                Property = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#hasStars",
                PropertyType = "http://www.w3.org/2002/07/owl#DatatypeProperty",
                Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                Range = "http://www.w3.org/2001/XMLSchema#int",
            });

            axioms.Add(new IndividualAxiomDTO()
            {
                Domain = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes",
                Property = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#continent",
                PropertyType = "http://www.w3.org/2002/07/owl#DatatypeProperty",
                Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                Range = "http://www.w3.org/2001/XMLSchema#string",
            });

            axioms.Add(new IndividualAxiomDTO()
            {
                Domain = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes",
                Property = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#awards",
                PropertyType = "http://www.w3.org/2002/07/owl#DatatypeProperty",
                Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                Range = "award",
            });

            axioms.Add(new IndividualAxiomDTO()
            {
                Domain = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes",
                Property = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#offerActivity",
                PropertyType = "http://www.w3.org/2002/07/owl#ObjectProperty",
                Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                Range = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#Activities",
            });

            axioms.Add(new IndividualAxiomDTO()
            {
                Domain = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#AccommodationTypes",
                Property = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#hasFacility",
                PropertyType = "http://www.w3.org/2002/07/owl#ObjectProperty",
                Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                Range = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#Facilities",
            });

            return axioms;
        }
    }
}
