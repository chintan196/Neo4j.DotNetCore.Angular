using DotnetCore.Neo4j.Angular.DataAccess;
using DotnetCore.Neo4j.Angular.Entities.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Neo4j.Angular.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<PersonsController> _logger;

        /// <summary>
        /// Person repository
        /// </summary>
        private IPersonRepository _personRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="personRepository">The person repository.</param>
        public PersonsController(ILogger<PersonsController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        /// <summary>
        /// Searches persons.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>ActionResult&lt;List&lt;Dictionary&lt;System.String, System.Object&gt;&gt;&gt;.</returns>
        [HttpGet]
        [Route("SearchByName")]
        public async Task<ActionResult<List<Dictionary<string, object>>>> SearchByName(string searchString)
        {
            _logger.LogInformation($"Person search with search string: {searchString}");
            return await _personRepository.SearchPersonsByName(searchString);
        }


        /// <summary>
        /// Test Post Person
        /// </summary>
        /// <param name="filterObject"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("postPerson")]
        public async Task<ActionResult<Person>> PostPerson([FromBody] Person person)
        {
            _logger.LogInformation("Test post person method");
            return Ok(person);
        }
    }
}
