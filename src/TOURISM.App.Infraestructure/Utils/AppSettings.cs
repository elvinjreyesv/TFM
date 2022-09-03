using System;
using System.Collections.Generic;
using System.Text;

namespace TOURISM.App.Infrastructure.Utils
{
    public class AppSettings
    {
        public string Name { get; set; }
        public string SiteId { get; set; }
        public string WebApiBaseUrl { get; set; }
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
        public string TokenRequestUrl { get; set; }
        public string TokenResponseUrl { get; set; }
        public string AesKey { get; set; }
        public string AesIv { get; set; }
        public string OntologyPrefix { get; set; }
        public DocumentationFile DocumentationFile { get; set; }
        public Extensions Extensions { get; set; }
    }

    public class Extensions
    {
        public List<string> Include { get; set; }
    }

    public class DocumentationFile
    {
        public string Url { get; set; }
        public int Version { get; set; }
    }
}
