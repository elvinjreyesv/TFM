using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TOURISM.App.DataAccess;
using TOURISM.App.Services.Abstracts;

namespace TOURISM.Web.Api.Utils.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var injectedRequestStream = new MemoryStream();

            var requestTime = DateTime.UtcNow;
            var stopWatch = Stopwatch.StartNew();
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            var responseBodyContent = string.Empty;
            var requestBody = string.Empty;

            var exceptionText = string.Empty;

            try
            {
                requestBody = await FormatRequest(context, injectedRequestStream);
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    var response = context.Response;
                    response.Body = responseBody;
                    await _next.Invoke(context);

                    responseBodyContent = await ReadResponseBody(response);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                responseBodyContent += $"\n Exception {ex.Message} {ex.StackTrace} {ex.Source}";
            }
            finally
            {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();
            }
        }

        private async Task<string> FormatRequest(HttpContext context, MemoryStream injectedRequestStream)
        {
            var requestBody = string.Empty;
            using (var bodyReader = new StreamReader(context.Request.Body))
            {
                var bodyAsText = await bodyReader.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(bodyAsText) == false)
                {
                    requestBody = $"{context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString} {bodyAsText}";
                }

                var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                injectedRequestStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = injectedRequestStream;
            }

            return requestBody;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        public static string LocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "::1";
        }
    }
}
