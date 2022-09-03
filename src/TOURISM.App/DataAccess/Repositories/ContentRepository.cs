using TOURISM.App.DataAccess.Repositories.Abstracts;
using System;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using TOURISM.App.Models.ViewModels.Public.Content.Outputs;
using System.Collections.Generic;
using System.Linq;
using TOURISM.App.Services.Abstracts;
using TOURISM.App.Infrastructure.Utils;
using Microsoft.Extensions.Options;
using VDS.RDF.Query.Inference;
using Newtonsoft.Json;
using TOURISM.App.Utils;

namespace TOURISM.App.DataAccess.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly RdfXmlParser rdfParser;
        private readonly Graph graph;
        private readonly LeviathanQueryProcessor processor;
        private readonly SparqlQueryParser sparqlParser;
        private readonly ISparqlDataset sparqlDataset;
        private readonly WebApiAppSettings _settings;
        private readonly SiteInfo SiteInfo = default(SiteInfo);
        private readonly string prefixes = string.Empty;
        private readonly string individualPropertyType1 = string.Empty;
        private readonly string individualPropertyType2 = string.Empty;
        private readonly string commentReplacePattern = string.Empty;
        private readonly string individualClassValidation = string.Empty;
        private readonly List<string> individualReplacePattern = new List<string>();
        public ContentRepository(IOptions<WebApiAppSettings> settings)
        {
            rdfParser = new RdfXmlParser(RdfXmlParserMode.DOM);
            graph = new Graph();
            sparqlDataset = new InMemoryDataset(graph);
            processor = new LeviathanQueryProcessor(sparqlDataset);
            sparqlParser = new SparqlQueryParser();

            _settings = settings.Value;
            SiteInfo = _settings.SiteInfos.FirstOrDefault();
            rdfParser.Load(graph, SiteInfo.OntologyDetails.OntologyFile);

            var rdfReasoner = new RdfsReasoner();
            rdfReasoner.Initialise(graph);
            rdfReasoner.Apply(graph);

            var simpleReasoner = new SimpleN3RulesReasoner();
            simpleReasoner.Initialise(graph);
            simpleReasoner.Apply(graph);

            prefixes = string.Join(' ', SiteInfo.OntologyDetails.QueryPrefixes);
            individualReplacePattern = SiteInfo.OntologyDetails.IndividualReplacePattern;
            commentReplacePattern = SiteInfo.OntologyDetails.CommentReplacePattern;
            individualClassValidation = SiteInfo.OntologyDetails.IndividualClassValidation;
            individualPropertyType1 = SiteInfo.OntologyDetails.IndividualPropertyTypes.FirstOrDefault();
            individualPropertyType2 = SiteInfo.OntologyDetails.IndividualPropertyTypes.Skip(1).FirstOrDefault();
        }

        #region Process Ontology
        public List<OntologyDTO> GetOntologySuperClases()
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();

            var queryString = @"SELECT DISTINCT ?class ?comment
              WHERE 
              {
                  {  
                      ?class rdf:type owl:Class . 
                      ?subClass rdf:type owl:Class . 
                      ?subClass rdfs:subClassOf ?class 
                      FILTER NOT EXISTS 
                      {
                          ?class rdfs:subClassOf ?otherSup 
                          FILTER (?otherSup != owl:Thing)
                      } 
                   } 
                   UNION 
                   { 
                         ?class rdf:type owl:Class 
                         FILTER NOT EXISTS { ?subClass rdfs:subClassOf ?class } 
                         FILTER NOT EXISTS { ?class rdfs:subClassOf ?supClass }
                    }
                    OPTIONAL { ?class rdfs:comment ?comment} .
              }";

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                output.Add(new OntologyDTO()
                {
                    Class = item["class"]?.ToString(),
                    Comment = item["comment"]?.ToString(),
                    Parent = "owl:Thing",
                    Children = Enumerable.Empty<OntologyDTO>().ToList(),
                    Individuals = Enumerable.Empty<IndividualPropertiesDTO>().ToList()
                });
            }

            return output;
        }
        public List<OntologyDTO> GetOntologySubClases()
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = SiteInfo.OntologyDetails.OntologySubClasesPrefix;

            var queryString = @"SELECT DISTINCT ?class ?parent ?label ?comment
                 WHERE {
                     { ?class a owl:Class . } UNION { ?individual a ?class . } .
                     OPTIONAL { ?class rdfs:subClassOf ?parent } .
                     OPTIONAL { ?class rdfs:comment ?comment} .
                 } ORDER BY ?class";

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results != null && results.Any())
            {
                output = ParseOntologyClassesResults(results);

                if (output != null && output.Any())
                    output = output.Where(row => !string.IsNullOrWhiteSpace(row.Parent) && !row.Class.Contains(filterClasses)).ToList();
            }

            return output;
        }
        public List<OntologyDTO> GetAnyOntologyElement(string parentClass)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = SiteInfo.OntologyDetails.OntologySubClasesPrefix;

            var queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(parentClass))
            {
                queryString = @"SELECT ?class ?parent ?comment
                  WHERE {
                      ?class rdfs:subClassOf * <MainClass> .
                      OPTIONAL { ?class rdfs:subClassOf ?parent } .
                      OPTIONAL { ?class rdfs:comment ?comment} .
                  } ORDER BY ?class".Replace("MainClass", parentClass);
            }
            else
            {
                queryString = @"SELECT DISTINCT ?class ?parent ?comment
                  WHERE 
                  {
                      {  
                          ?class rdf:type owl:Class . 
                          ?subClass rdf:type owl:Class . 
                          ?subClass rdfs:subClassOf ?class 
                          FILTER NOT EXISTS 
                          {
                              ?class rdfs:subClassOf ?otherSup 
                              FILTER (?otherSup != owl:Thing)
                          } 
                       } 
                       UNION 
                       { 
                             ?class rdf:type owl:Class 
                             FILTER NOT EXISTS { ?subClass rdfs:subClassOf ?class } 
                             FILTER NOT EXISTS { ?class rdfs:subClassOf ?supClass }
                        }
                        OPTIONAL { ?class rdfs:comment ?comment} .
                        BIND(STR(?class) AS ?parent) .
                  }";
            }

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results != null && results.Any())
                output = ParseOntologyClassesResults(results);

            if (output != null && output.Any())
            {
                output = output.Where(row => !string.IsNullOrWhiteSpace(row.Parent) && !row.Class.Contains(filterClasses)).ToList();

                if (!string.IsNullOrWhiteSpace(parentClass))
                    output = output.Where(row => row.Class != parentClass && row.Parent == parentClass).ToList();
                else
                    output.ForEach(row => row.Parent = row.Class == row.Parent ? "owl:Thing" : row.Parent);
            }

            return output;
        }
        public List<OntologyDTO> GetOntologySubClasesByParent(string parentClass)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = SiteInfo.OntologyDetails.OntologySubClasesPrefix;

            var queryString = @"SELECT ?class ?parent ?comment
                 WHERE {
                     ?class rdfs:subClassOf * <MainClass> .
                     OPTIONAL { ?class rdfs:subClassOf ?parent } .
                     OPTIONAL { ?class rdfs:comment ?comment} .
                 } ORDER BY ?class".Replace("MainClass", parentClass);

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results != null && results.Any())
            {
                output = ParseOntologyClassesResults(results);

                if (output != null && output.Any())
                    output = output.Where(row => !string.IsNullOrWhiteSpace(row.Parent)
                        && !row.Class.Contains(filterClasses)).ToList();

                if (SiteInfo.OntologyDetails.RootEntity == parentClass)
                    output = output.Where(row => row.Parent == parentClass && row.Class != parentClass).ToList();
            }

            return output;
        }
        public List<IndividualPropertiesDTO> GetOntologyIndividuals(string parentClass, List<string> superClasses, string additionalFilter="", bool includeProperties = true)
        {
            var output = Enumerable.Empty<IndividualPropertiesDTO>().ToList();

            var queryString = @"SELECT DISTINCT ?class ?parent ?individual ?image ?comment
                  WHERE {
                      { ?class a owl:Class . } UNION { ?individual a ?class . } .
                      ?class rdfs:subClassOf* <MainClass> .
                      OPTIONAL { ?class rdfs:subClassOf ?parent } .
                      ADDITIONAL_FILTER
                  } ORDER BY ?class".Replace("MainClass", parentClass)
                  .Replace("ADDITIONAL_FILTER", additionalFilter);

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results == null || !results.Any())
                return output;

            output = ProcessIndividualProperties(results, superClasses, includeProperties);

            return output;
        }
        #endregion

        #region Helpers
        private List<IndividualAxiomDTO> GetIndividualAxiom(string individualClass, string individual)
        {
            var individualAxioms = Enumerable.Empty<IndividualAxiomDTO>().ToList();

            var replacePattern = individualReplacePattern;
            var propertyType1 = individualPropertyType1;
            var propertyType2 = individualPropertyType2;

            var queryString = @"SELECT DISTINCT ?domain ?parent ?propertyType ?range
                WHERE {    
                <IndividualClass> rdfs:subClassOf* ?parent.
                ?domain rdfs:domain ?parent.
                ?domain rdf:type ?propertyType.
                ?domain  rdfs:range ?range
                FILTER ((?propertyType = <PropertyType1>) 
                    || (?propertyType = <PropertyType2>))
                }".Replace("IndividualClass", individualClass)
                    .Replace("PropertyType1", propertyType1)
                    .Replace("PropertyType2", propertyType2);

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            individualAxioms = ParseIndividualAxiom(results).FormatAxioms(individualClass);

            foreach (var item in individualAxioms)
            {
                var range = item.Range;
                var domain = item.Domain;
                var ontolProperty = item.Property;

                foreach (var rep in replacePattern)
                {
                    var index = replacePattern.IndexOf(rep) + 1;
                    range = range.Replace(rep, "");
                    domain = domain.Replace(rep, "");

                    if (index <= 2)
                        ontolProperty = ontolProperty.Replace(rep, "");
                }

                range = range.ToLower();
                domain = domain.ToLower();

                var axiomValuequery = new AxiomQueryDTO()
                {
                    Condition = $" ?{domain} ontol:{ontolProperty} ?{range}.",
                    QueyItems = range,
                    Property = item.Property,
                    PropertyType = item.PropertyType,
                    Individual = individual,
                    Domain = domain
                };

                var axiomValues = FillAxiomValue(axiomValuequery);

                if (axiomValues != null && axiomValues.Any())
                    item.Values.AddRange(axiomValues);
            }

            individualAxioms = individualAxioms.Where(row => row.Values != null && row.Values.Any()).ToList();

            return individualAxioms;
        }
        private List<AxiomValueDTO> FillAxiomValue(AxiomQueryDTO queyItems)
        {
            var output = Enumerable.Empty<AxiomValueDTO>().ToList();

            var queryString = @"SELECT ?QueryItems
             WHERE { 
                 Condition
                 FILTER ( ?Domain  = <Individual>)
              .}".Replace("QueryItems", queyItems.QueyItems.Trim()).Replace("Condition", queyItems.Condition.Trim())
                     .Replace("Individual", queyItems.Individual.Trim()).Replace("Domain", queyItems.Domain.Trim());

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(query);

            if (results != null && results.Any())
                output = ParseAxiomValue(results, queyItems.QueyItems);

            return output;
        }
        private List<OntologyDTO> ParseOntologyClassesResults(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var replaceComment = commentReplacePattern;

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                output.Add(new OntologyDTO()
                {
                    Class = GetFieldValue(item, "class"),
                    Parent = GetFieldValue(item, "parent"),
                    Comment = GetFieldValue(item, "comment").Replace(replaceComment, ""),
                    Children = Enumerable.Empty<OntologyDTO>().ToList(),
                    Individuals = Enumerable.Empty<IndividualPropertiesDTO>().ToList()
                });
            }
            return output;
        }
        private List<IndividualDTO> ParseIndividuals(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<IndividualDTO>().ToList();

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                var className = GetFieldValue(item, "class");
                if (className.Contains(individualClassValidation))
                    continue;

                output.Add(new IndividualDTO()
                {
                    Class = className,
                    Comment = GetFieldValue(item, "comment"),
                    Image = GetFieldValue(item, "image"),
                    IndividualName = GetFieldValue(item, "individual"),
                    Parent = GetFieldValue(item, "parent"),
                });
            }

            return output;
        }
        private List<IndividualPropertiesDTO> ProcessIndividualProperties(List<SparqlResult> results, List<string> superclasses, bool includeProperties = true)
        {
            var output = Enumerable.Empty<IndividualPropertiesDTO>().ToList();

            var allIndividuals = ParseIndividuals(results);

            var differentIndividuals = allIndividuals.Where(row => !string.IsNullOrWhiteSpace(row.IndividualName))
                .GroupBy(row => row.IndividualName)
                .ToList();

            foreach (var ind in differentIndividuals)
            {
                var items = Enumerable.Empty<PropertyDTO>().ToList();
                if (includeProperties)
                {
                    foreach (var item in ind.ToList())
                    {
                        items.Add(new PropertyDTO()
                        {
                            Class = item.Class,
                            Comment = item.Comment,
                            Image = item.Image,
                            Parent = item.Parent
                        });
                    }
                }
               
                output.Add(new IndividualPropertiesDTO()
                {
                    IndividualName = ind.Key,
                    Properties = items
                });
            }

            if (output == null || !output.Any())
                return output;

            if (includeProperties)
            {
                foreach (var ind in output)
                {
                    foreach (var item in ind.Properties)
                    {
                        var axioms = GetIndividualAxiom(item.Class, ind.IndividualName);

                        if (axioms != null && axioms.Any())
                            axioms = axioms.ToList();

                        item.Axioms = axioms;
                    }
                }

                foreach (var item in output)
                {
                    var axioms = ((item.Properties.SelectMany(row => row.Axioms)).GroupBy(row => row.Property))
                        .Select(row => new IndividualAxiomDTO()
                        {
                            Domain = row.FirstOrDefault().Domain,
                            Property = row.FirstOrDefault().Property,
                            PropertyType = row.FirstOrDefault().PropertyType,
                            Range = row.FirstOrDefault().Range,
                            Values = row.FirstOrDefault().Values
                        }).ToList();

                    item.Properties.ForEach(row => row.Axioms = Enumerable.Empty<IndividualAxiomDTO>().ToList());

                    var parentClassProperty = item.Properties.FirstOrDefault(row => superclasses.Contains(row.Parent));
                    if (parentClassProperty != null)
                        parentClassProperty.Axioms = axioms;
                    else
                        item.Properties.FirstOrDefault().Axioms = axioms;
                }
            }

            output = output.OrderBy(row => row.IndividualName).ToList();
            return output;
        }
        private List<IndividualAxiomDTO> ParseIndividualAxiom(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<IndividualAxiomDTO>().ToList();

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                output.Add(new IndividualAxiomDTO()
                {
                    Domain = GetFieldValue(item, "parent"),
                    Property = GetFieldValue(item, "domain"),
                    PropertyType = GetFieldValue(item, "propertyType"),
                    Values = Enumerable.Empty<AxiomValueDTO>().ToList(),
                    Range = GetFieldValue(item, "range"),
                });
            }

            return output;
        }
        private List<AxiomValueDTO> ParseAxiomValue(List<SparqlResult> results, string queryItems)
        {
            var output = Enumerable.Empty<AxiomValueDTO>().ToList();

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                output.Add(new AxiomValueDTO()
                {
                    Value = GetFieldValue(item, queryItems)
                });
            }

            return output;
        }
        private string GetFieldValue(SparqlResult result, string field)
        {
            var value = string.Empty;
            try
            {
                value = (result[field.Trim()]?.ToString()) ?? string.Empty;
            }
            catch (Exception ex)
            {
                value = string.Empty;
            }

            return value;
        }
        private List<SparqlResult> QueryResults(SparqlQuery query)
        {
            var processQuery = processor.ProcessQuery(query);
            var parsedResults = (processQuery is SparqlResultSet ? processQuery as SparqlResultSet : null)?
                .Results?.Where(row => row.IsGroundResult);

            var output = parsedResults?.ToList();

            return output;
        }
        #endregion
    }
}
