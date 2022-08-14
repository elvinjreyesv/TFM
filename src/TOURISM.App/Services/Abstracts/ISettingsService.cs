using TOURISM.App.Infrastructure.Utils;

namespace TOURISM.App.Services.Abstracts
{
    public interface ISettingsService
    {
        SiteInfo SiteInfo(string businessId);
        SiteInfo SiteInfo();
        JwtConfig JwtConfig();
        bool IsSiteRegistered(string siteId);
        public SiteInfo InfoSite(string siteId);
    }
}
