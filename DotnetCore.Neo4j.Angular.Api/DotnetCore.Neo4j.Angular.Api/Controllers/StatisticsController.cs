using DotnetCore.Neo4j.Angular.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Neo4j.Angular.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<StatisticsController> _logger;

        /// <summary>
        /// Statistics Repository
        /// </summary>
        private IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository, ILogger<StatisticsController> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>ActionResult&lt;DashboardResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, object>>> Get()
        {
            _logger.LogInformation("Getting movie details");
            return await _statisticsRepository.GetEntityWiseCount();
        }
    }
}
