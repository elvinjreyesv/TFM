using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TestOwl.Entities;
using TestOwl.Model;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing.Formatting;

namespace TestOwl
{
    internal class Program
    {
        const string OntologyFile = @"C:\Users\user\OneDrive\3- ESTUDIAR\7- SEVILLA\MATERIAS\OTROS\TFM\Code\Tourism Ontology\TestOwl\Data\rdf_tourism.rdf";
        const string RootEntity = "http://www.semanticweb.org/user/ontologies/2022/0/tourism#Destination";
        static void Main(string[] args)
        {
            ParsingRdfXml();

            //SerializeOwlXml();

            Console.ReadLine();
        }

        #region OWL to XML
        public static void SerializeOwlXml()
        {
            string xmlRoute = @"C:\Users\user\OneDrive\3- ESTUDIAR\7- SEVILLA\MATERIAS\OTROS\TFM\Code\Tourism Ontology\TestOwl\Data\owl_tourism_complete.owl";
            string xml = File.ReadAllText(xmlRoute);

            var serializer = new XmlSerializer(typeof(Ontology));
            Ontology ontologyElements;
            using (TextReader reader = new StringReader(xml))
            {
                ontologyElements = (Ontology)serializer.Deserialize(reader);
            }

            var replaceString = new Dictionary<string, string>()
            {
                { "http://www.semanticweb.org/user/ontologies/2022/0/tourism#", "" },
                { "http://www.semanticweb.org/user/ontologies/2022/0/", "" },
                { "http://www.w3.org/2001/XMLSchema#", "" },
                { "http://www.w3.org/2000/01/rdf-schema#","" },
                { "http://schema.org/image", "image" },
                { @"\u0026", "&" },
                { @"\u00E1", "á" },
                { @"\u003C", "<" },
                { @"\u003E", ">" },
                { @"\u2019", "'" },
            };

            string jsonString = JsonConvert.SerializeObject(ontologyElements);
            foreach (var item in replaceString)
                jsonString = jsonString.Replace(item.Key, item.Value);

            ontologyElements = JsonConvert.DeserializeObject<Ontology>(jsonString);
        }
        #endregion

        #region RDF Parsing
        public static void ParsingRdfXml()
        {
            var rdfParser = new RdfXmlParser(RdfXmlParserMode.DOM);
            var graph = new Graph();
            rdfParser.Load(graph, OntologyFile);

            ISparqlDataset sparqlDataset = new InMemoryDataset(graph);
            var processor = new LeviathanQueryProcessor(sparqlDataset);
            var sparqlParser = new SparqlQueryParser();

            var prefixes = new List<string>()
            {
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>",
                "PREFIX owl: <http://www.w3.org/2002/07/owl#>",
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>",
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>",
                "PREFIX ontol: <http://www.semanticweb.org/user/ontologies/2022/0/tourism#>",
                "PREFIX schema: <http://schema.org/>"
            };

            GetBaseContent(processor, sparqlParser, string.Join(' ', prefixes));
        }

        #region Generate Pages Content
        public static List<OntologyDTO> GetBaseContent(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes)
        {
            var output = GetOntologySubClasesByParent(processor, sparqlParser, string.Join(' ', prefixes), RootEntity);
            return output;
        }

        public static List<OntologyDTO> GetClassContent(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, string itemClass)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();

            var superClasses = GetOntologySuperClases(processor, sparqlParser, string.Join(' ', prefixes));
            var superClassesList = superClasses.Select(row => row.Class).ToList();

            output = GetOntologySubClasesByParent(processor, sparqlParser, string.Join(' ', prefixes), itemClass);

            foreach (var item in output)
            {
                var individuals = GetOntologyIndividuals(processor, sparqlParser, string.Join(' ', prefixes), item.Class, superClassesList);
                if (individuals != null && individuals.Any())
                    item.Individuals = individuals;
            }

            return output;
        }

        public static List<OntologyDTO> GetFullOntology(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();

            try
            {
                var superClasses = GetOntologySuperClases(processor, sparqlParser, string.Join(' ', prefixes));
                var allSubClasses = GetOntologySubClases(processor, sparqlParser, string.Join(' ', prefixes));

                output = ProcessOntologyElements(superClasses, allSubClasses);

                var superClassesList = superClasses.Select(row => row.Class).ToList();

                foreach (var item in output)
                {
                    var individuals = GetOntologyIndividuals(processor, sparqlParser, string.Join(' ', prefixes), item.Class, superClassesList);
                    if (individuals != null && individuals.Any())
                        item.Individuals = individuals;
                }

                string jsonString = JsonConvert.SerializeObject(output);
            }
            catch (Exception ex)
            {
            }
            return output;
        }
        #endregion

        #region Process Ontology
        public static List<OntologyDTO> GetOntologySuperClases(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes)
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
            var results = QueryResults(processor, query);

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
        public static List<OntologyDTO> GetOntologySubClases(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = "http://www.w3.org";

            var queryString = @"SELECT DISTINCT ?class ?parent ?label ?comment
                WHERE {
                    { ?class a owl:Class . } UNION { ?individual a ?class . } .
                    OPTIONAL { ?class rdfs:subClassOf ?parent } .
                    OPTIONAL { ?class rdfs:comment ?comment} .
                } ORDER BY ?class";

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(processor, query);

            if (results !=null && results.Any())
            {
                output = ParseOntologyClassesResults(results);

                if (output != null && output.Any())
                    output = output.Where(row => !string.IsNullOrWhiteSpace(row.Parent) && !row.Class.Contains(filterClasses)).ToList();
            }

            return output;
        }
        public static List<OntologyDTO> GetAnyOntology_Class_SubClass(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, string parentClass)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = "http://www.w3.org";

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
            var results = QueryResults(processor, query);

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
        public static List<OntologyDTO> GetOntologySubClasesByParent(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, string parentClass)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var filterClasses = "http://www.w3.org";

            var queryString = @"SELECT ?class ?parent ?comment
                WHERE {
                    ?class rdfs:subClassOf * <MainClass> .
                    OPTIONAL { ?class rdfs:subClassOf ?parent } .
                    OPTIONAL { ?class rdfs:comment ?comment} .
                } ORDER BY ?class".Replace("MainClass", parentClass);

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(processor, query);

            if (results !=null && results.Any())
            {
                output = ParseOntologyClassesResults(results);

                if (output != null && output.Any())
                    output = output.Where(row => !string.IsNullOrWhiteSpace(row.Parent)
                        && !row.Class.Contains(filterClasses) && row.Class != parentClass
                        && row.Parent == parentClass).ToList();
            }

            return output;
        }
        public static List<IndividualPropertiesDTO> GetOntologyIndividuals(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, string parentClass, List<string> superClasses)
        {
            var output = Enumerable.Empty<IndividualPropertiesDTO>().ToList();

            var queryString = @"SELECT DISTINCT ?class ?parent ?individual ?image ?comment
                 WHERE {
                     { ?class a owl:Class . } UNION { ?individual a ?class . } .
                     ?class rdfs:subClassOf* <MainClass> .
                     OPTIONAL { ?class rdfs:subClassOf ?parent } .
                 } ORDER BY ?class".Replace("MainClass", parentClass);

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(processor, query);

            if (results == null || !results.Any())
                return output;

            output = ProcessIndividualProperties(processor, sparqlParser, prefixes, results, superClasses);

            return output;
        }
        public static List<IndividualAttributeDTO> GetIndividualAttribute(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, string individualClass, string individual)
        {
            var individualAttributes = Enumerable.Empty<IndividualAttributeDTO>().ToList();

            var replacePattern = new List<string>()
            {
                "http://www.semanticweb.org/user/ontologies/2022/0/tourism#",
                "http://www.w3.org/2001/XMLSchema#",
                "has",
                "is",
                "only"
            };

            var propertyType1 = "http://www.w3.org/2002/07/owl#ObjectProperty";
            var propertyType2 = "http://www.w3.org/2002/07/owl#DatatypeProperty";

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
            var results = QueryResults(processor, query);

            if (results !=null && results.Any())
            {
                individualAttributes = ParseIndividualAttribute(results);

                foreach (var item in individualAttributes)
                {
                    var range = item.Range;
                    var domain = item.Domain;
                    var ontolProperty = item.Property;

                    foreach(var rep in replacePattern)
                    {
                        var index = replacePattern.IndexOf(rep) + 1;
                        range = range.Replace(rep, "");
                        domain = domain.Replace(rep, "");
                        
                        if(index <= 2)
                            ontolProperty = ontolProperty.Replace(rep, "");
                    }

                    range = range.ToLower();
                    domain = domain.ToLower();

                    var attributeValuequery = new AttributeQueryDTO()
                    {
                        Condition = $" ?{domain} ontol:{ontolProperty} ?{range}.",
                        QueyItems = range,
                        Property = item.Property,
                        PropertyType = item.PropertyType,
                        Individual = individual,
                        Domain = domain
                    };

                    var attributeValues = FillAttributeValue(processor, sparqlParser, string.Join(' ', prefixes), attributeValuequery);
                    
                    if(attributeValues != null && attributeValues.Any())
                        item.Value.AddRange(attributeValues);
                }

                individualAttributes = individualAttributes.Where(row => row.Value != null && row.Value.Any()).ToList();
            }

            return individualAttributes;
        }
        public static List<AttributeValueDTO> FillAttributeValue(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, AttributeQueryDTO queyItems)
        {
            var output = Enumerable.Empty<AttributeValueDTO>().ToList();

            var queryString = @"SELECT ?QueryItems
                    WHERE { 
                        Condition
                        FILTER ( ?Domain  = <Individual>)
                     .}".Replace("QueryItems", queyItems.QueyItems.Trim()).Replace("Condition", queyItems.Condition.Trim())
                     .Replace("Individual", queyItems.Individual.Trim()).Replace("Domain", queyItems.Domain.Trim());

            var query = sparqlParser.ParseFromString(string.Concat(prefixes, " ", queryString));
            var results = QueryResults(processor, query);

            if(results !=null && results.Any())
                output = ParseAttributeValue(results, queyItems.QueyItems);

            return output;
        }
        #endregion

        #region Helpers
        public static List<OntologyDTO> ProcessOntologyElements(List<OntologyDTO> classes, List<OntologyDTO> subClasses)
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
        public static List<OntologyDTO> ParseOntologyClassesResults(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<OntologyDTO>().ToList();
            var replaceComment = "^^http://www.w3.org/2000/01/rdf-schema#Literal";

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
        public static List<IndividualDTO> ParseIndividuals(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<IndividualDTO>().ToList();

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                var className = GetFieldValue(item, "class");
                if (className.Contains("http://www.w3.org/2002/07/owl#"))
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
        public static List<IndividualPropertiesDTO> ProcessIndividualProperties(LeviathanQueryProcessor processor, SparqlQueryParser sparqlParser, string prefixes, List<SparqlResult> results, List<string> superclasses)
        {
            var output = Enumerable.Empty<IndividualPropertiesDTO>().ToList();

            var allIndividuals = ParseIndividuals(results);

            var differentIndividuals = allIndividuals.Where(row => !string.IsNullOrWhiteSpace(row.IndividualName))
                .GroupBy(row => row.IndividualName)
                .ToList();

            foreach (var ind in differentIndividuals)
            {
                var items = Enumerable.Empty<PropertyDTO>().ToList();
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

                output.Add(new IndividualPropertiesDTO()
                {
                    IndividualName = ind.Key,
                    Properties = items
                });
            }

            if (output == null || !output.Any())
                return output;

            foreach (var ind in output)
            {
                foreach (var item in ind.Properties)
                {
                    var attributes = GetIndividualAttribute(processor, sparqlParser, string.Join(' ', prefixes), item.Class, ind.IndividualName);

                    if (attributes != null && attributes.Any())
                        attributes = attributes.ToList();

                    item.Attributes = attributes;
                }
            }

            foreach (var item in output)
            {
                var attributes = ((item.Properties.SelectMany(row => row.Attributes)).GroupBy(row => row.Property))
                    .Select(row => new IndividualAttributeDTO()
                    {
                        Domain = row.FirstOrDefault().Domain,
                        Property = row.FirstOrDefault().Property,
                        PropertyType = row.FirstOrDefault().PropertyType,
                        Range = row.FirstOrDefault().Range,
                        Value = row.FirstOrDefault().Value
                    }).ToList();

                item.Properties.ForEach(row => row.Attributes = Enumerable.Empty<IndividualAttributeDTO>().ToList());

                var parentClassProperty = item.Properties.FirstOrDefault(row => superclasses.Contains(row.Parent));
                if (parentClassProperty != null)
                    parentClassProperty.Attributes = attributes;
                else
                    item.Properties.FirstOrDefault().Attributes = attributes;
            }

            output = output.OrderBy(row => row.IndividualName).ToList();

            string jsonString = JsonConvert.SerializeObject(output);

            return output;
        }
        public static List<IndividualAttributeDTO> ParseIndividualAttribute(List<SparqlResult> results)
        {
            var output = Enumerable.Empty<IndividualAttributeDTO>().ToList();

            if (results == null || !results.Any())
                return output;

            foreach (SparqlResult item in results)
            {
                output.Add(new IndividualAttributeDTO()
                {
                    Domain = GetFieldValue(item, "parent"),
                    Property = GetFieldValue(item, "domain"),
                    PropertyType = GetFieldValue(item, "propertyType"),
                    Value = Enumerable.Empty<AttributeValueDTO>().ToList(),
                    Range = GetFieldValue(item, "range"),
                });
            }

            return output;
        }
        public static List<AttributeValueDTO> ParseAttributeValue(List<SparqlResult> results, string queryItems)
        {
            var output = Enumerable.Empty<AttributeValueDTO>().ToList();

            if (results == null || !results.Any())
                return output;
            
            foreach (SparqlResult item in results)
            {
                output.Add(new AttributeValueDTO()
                {
                    Value = GetFieldValue(item, queryItems)
                });
            }
               
            return output;
        }
        public static string GetFieldValue(SparqlResult result, string field)
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
        public static List<SparqlResult> QueryResults(LeviathanQueryProcessor processor, SparqlQuery query)
        {
            var processQuery = processor.ProcessQuery(query);
            var parsedResults = (processQuery is SparqlResultSet ? processQuery as SparqlResultSet : null)?
                .Results?.Where(row => row.IsGroundResult);

            var output = parsedResults?.ToList();

            return output;
        }
        #endregion
        #endregion
    }
}
