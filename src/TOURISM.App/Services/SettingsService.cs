using Microsoft.Extensions.Options;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TOURISM.App.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly WebApiAppSettings settings;

        public SettingsService(IOptions<WebApiAppSettings> settings)
        {
            this.settings = settings.Value;
        }

        public SiteInfo SiteInfo(string id)
        {
            return settings.SiteInfos.FirstOrDefault(row => row.Id == id);
        }
        public SiteInfo SiteInfo()
        {
            return settings.SiteInfos.FirstOrDefault();
        }

        public SiteInfo InfoSite(string siteId)
        {
            return settings.SiteInfos.FirstOrDefault(row => row.SiteId == siteId);
        }

        public bool IsSiteRegistered(string siteId)
        {
            var isPrincipal = settings.SiteInfos.Any(row => string.Equals(row.SiteId, siteId, StringComparison.InvariantCultureIgnoreCase));
            if (isPrincipal) return false;

            return true;
        }

        public JwtConfig JwtConfig()
        {
            return settings.JwtConfig;
        }
    }
}
