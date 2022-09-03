using NUnit.Framework;
using TOURISM.App.Infrastructure.Utils;
using TOURISM.App.Models.ViewModels.Public.Content.Inputs;
using System.Threading.Tasks;

namespace TOURISM.Web.Api.Tests.Public
{
    [TestFixture]
    public class ContentControllerTests : BaseControllerTests
    {
        [Test, Category("Integration: Public/Content")]
        public async Task Content_Returns_Ok()
        {
            var dto = new PublicContentInputDTO()
            {
                SiteId = "001",
                IpAddressClient = "10.0.0.2",
                Lang = "ENG"
            };

            dto.Token = TokenEncrypter.Encrypt(dto, SecretKey);
            var response = await ApiClient.GetClassContent("http://www.semanticweb.org/user/ontologies/2022/0/tourism#Destination", "", dto.SiteId, dto.Lang, dto.IpAddressClient, dto.Token);
            Assert.IsNotNull(response);
        }
    }
}
