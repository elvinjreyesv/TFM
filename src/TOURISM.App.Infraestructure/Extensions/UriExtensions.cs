using System;

namespace TOURISM.App.Infrastructure.Extensions
{
    public static class UriExtensions
    {
        public static string AddUrlScheme(this string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            var urlResult = !result ? $"http://{url.Trim()}" : url.Trim();

            return string.IsNullOrWhiteSpace(urlResult) ? string.Empty : urlResult;
        }
    }
}
