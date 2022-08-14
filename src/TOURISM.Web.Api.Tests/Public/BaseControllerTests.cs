using Refit;
using TOURISM.App.Infrastructure.Utils.RefifInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TOURISM.Web.Api.Tests.Public
{
    public class BaseControllerTests
    {
        public IApiMethods ApiClient => RestService.For<IApiMethods>(GlobalSetUp.TestServer.CreateClient());
        public string SecretKey => GlobalSetUp.SecretKey;
    }
}
