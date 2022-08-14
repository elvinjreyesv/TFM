using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TOURISM.Web.Api.Tests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        public static TestServer TestServer = null;
        public static string SecretKey = null;


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var webHostBuilder = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        var a = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent;
                        config
                            .SetBasePath(a?.FullName)
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables();
                    })
                ;

            GlobalSetUp.TestServer = new TestServer(webHostBuilder);
            SecretKey = "4D9B704F8838481885ED00DBAB269704BEC6AA285F264D5F9E00C40A13B7D8F2C001F473A6954AB9AEB8B318E2B5507DE6A93682540149CEB5DDA18946685580";
        }
    }
}
