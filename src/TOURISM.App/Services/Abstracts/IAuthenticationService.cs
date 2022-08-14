using System;
using System.Collections.Generic;
using System.Text;
using TOURISM.App.Models.ViewModels.Shared;

namespace TOURISM.App.Services.Abstracts
{
    public interface IAuthenticationService
    {
        AppResponse<bool> IsPageRequest<T>(T value, string clientId, string clientToken) where T : class;
    }
}
