using System.Collections.Generic;

namespace TOURISM.App.Infrastructure.Utils
{
    public class WebApiAppSettings
    {
        public JwtConfig JwtConfig { get; set; }
        public List<SiteInfo> SiteInfos { get; set; }
        public CacheConfiguration CacheConfiguration { get; set; }
    }
    public class JwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SigningKey { get; set; }
    }
    public class SiteInfo
    {
        public string Id { get; set; }
        public string SecretKey { get; set; }
        public string SiteId { get; set; }
        public PublicContent PublicContent { get; set; }
        public OntologyDetails OntologyDetails { get; set; }
    }
    public class PublicContent
    {
        public List<string> Pages { get; set; }
    }
    public class OntologyDetails
    {
        public string OntologyFile { get; set; }
        public string RootEntity { get; set; }
        public List<string> QueryPrefixes { get; set; }
        public string OntologySubClasesPrefix { get; set; }
        public List<string> IndividualPropertyTypes { get; set; }
        public string IndividualClassValidation { get; set; }
        public List<string> IndividualReplacePattern { get; set; }
        public string CommentReplacePattern { get; set; }
    }
    public class CacheConfiguration
    {
        public int AbsoluteExpirationSeconds { get; set; }
        public int SlidingExpirationSeconds { get; set; }
    }
}
