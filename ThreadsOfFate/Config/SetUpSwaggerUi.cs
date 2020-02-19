using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace ThreadsOfFate.Config
{
    public static partial class SetUp
    {
        /// <summary>
        /// SwaggerUi
        /// </summary>
        public static void SwaggerUi(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("../swagger/v1.0/swagger.json", "Threads Of Fate Backend API");
            options.RoutePrefix = "help";
        }
    }
}
