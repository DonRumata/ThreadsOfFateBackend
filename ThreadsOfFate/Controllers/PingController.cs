using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace ThreadsOfFate.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/ping")]
    [Produces("application/json")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class PingController : ControllerBase
    {
        private static readonly string Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /// <summary>
        /// GET: http://localhost:50500/v1.0/ping
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult Pong()
        {
            var result = new
            {
                AssemblyVersion = Version
            };
            return Ok(result);
        }

        /// <summary>
        /// OPTIONS: http://localhost:50500/v1.0/ping
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [MapToApiVersion("1.0")]
        public IActionResult Options()
        {
            return Ok();
        }
    }
}
