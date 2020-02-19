using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace ThreadsOfFate.Extensions
{
    static class HttpRequestExtensions
    {
        private const string ValueForRemoteIpForLocalhost = "::1";

        public static string GetUserAgent(this HttpRequest request)
        {
            if (!request.Headers.ContainsKey(HeaderNames.UserAgent))
                return null;

            return request.Headers[HeaderNames.UserAgent];
        }

        public static string GetUserIpAddress(this HttpRequest request)
        {
            var ip = request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ValueForRemoteIpForLocalhost.Equals(ip, StringComparison.InvariantCultureIgnoreCase))
                ip = request.HttpContext.Connection.LocalIpAddress.ToString();

            return ip;
        }
    }
}
