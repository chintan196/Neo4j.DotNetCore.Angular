using DotnetCore.Neo4j.Angular.DataAccess;
using DotnetCore.Neo4j.Angular.Entities.Common;
using DotnetCore.Neo4j.Angular.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Neo4j.Angular.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;

        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository, ILogger<MoviesController> logger)
        {
            _moviesRepository = moviesRepository;
            _logger = logger;
        }

        /// <summary>
        /// Search movies by filter
        /// </summary>
        /// <param name="filterObject">The filter object.</param>
        /// <returns>ActionResult&lt;MoviesListResult&gt;.</returns>
        [HttpPost]
        [Route("searchbyfilter")]
        public async Task<ActionResult<MovieListResult>> SearchMoviesByFilter([FromBody] MoviesFilter filterObject)
        {
            _logger.LogInformation("Getting movies list data by filter");
            return await _moviesRepository.SearchMoviesByFilter(filterObject);
        }
    }
}