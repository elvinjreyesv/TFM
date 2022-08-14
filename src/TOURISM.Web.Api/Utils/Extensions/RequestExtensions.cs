using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TOURISM.Web.Api.Utils.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsOTA(this HttpRequest request)
        {
            return request != null && request.Headers.TryGetValue("Accept", out StringValues value) &&
                   value.ToString().Contains("xml");
        }
    }
}
