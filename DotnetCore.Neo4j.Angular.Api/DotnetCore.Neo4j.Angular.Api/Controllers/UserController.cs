using DotnetCore.Neo4j.Angular.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Neo4j.Angular.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>ActionResult&lt;User&gt;.</returns>
        [HttpGet]
        public ActionResult<User> Get()
        {
            return new User { UserName = HttpContext?.User?.Identity?.Name };
        }
    }
}
