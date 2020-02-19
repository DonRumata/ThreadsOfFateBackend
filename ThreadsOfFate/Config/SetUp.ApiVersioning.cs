using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace ThreadsOfFate.Config
{
    /// <summary>
    /// SetUp
    /// </summary>
    public static partial class SetUp
    {
        /// <summary>
        /// ApiVersioning
        /// </summary>
        public static void ApiVersioning(ApiVersioningOptions options)
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }
    }
}
