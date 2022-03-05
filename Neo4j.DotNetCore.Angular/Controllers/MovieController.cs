using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4j.DotNetCore.Angular.DataAccess;
using Neo4j.DotNetCore.Angular.Entities.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4j.DotNetCore.Angular.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController
    {
        private ILogger<MovieController> _logger;
        private IMovieRepository _movieRepository;
        public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get(int skip, int limit)
        {
            _logger.LogInformation("Getting movie details");
            return await _movieRepository.GetMovies(skip,limit);
        }
    }
}
