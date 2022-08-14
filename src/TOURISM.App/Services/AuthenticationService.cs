using AutoMapper;
using TOURISM.App.DataAccess.Repositories.Abstracts;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.Enums;
using TOURISM.App.Models.ViewModels.Shared;
using TOURISM.App.Services.Abstracts;
using System;
using System.Threading.Tasks;

namespace TOURISM.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISettingsService _settingsService;
        public AuthenticationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public AppResponse<bool> IsPageRequest<T>(T value, string id, string clientToken) where T : class
        {
            try
            {
                if (value == null || string.IsNullOrWhiteSpace(clientToken))
                    return AppResponse.Create(EAppResponse.InvalidInput, false);

                var secretKey = string.Empty;
                var info = _settingsService.InfoSite(id);
                if (info == null)
                    return AppResponse.Create(EAppResponse.InvalidInput, false);
                secretKey = info.SecretKey;

                var serverToken = TokenEncrypter.Encrypt(value, secretKey);
                if (serverToken != clientToken)
                    return AppResponse.Create(EAppResponse.InvalidToken, false);

                return AppResponse.Create(EAppResponse.Success, true);

            }
            catch (Exception ex)
            {
                return AppResponse.Create(EAppResponse.InvalidInput, false);
            }

        }
    }
}
