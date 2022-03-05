using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neo4j.DotNetCore.Angular.Entities.Common;
using Neo4j.DotNetCore.Angular.Entities.Domain;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4j.DotNetCore.Angular.DataAccess
{
    public class MovieRepository : IMovieRepository
    {
        private IDriver _driver;
        private ILogger<MovieRepository> _logger;
        private string _database;
        public MovieRepository(IDriver driver, ILogger<MovieRepository> logger, IOptions<ApplicationSettings> appSettingsOptions)
        {
            _driver = driver;
            _logger = logger;
            _database = appSettingsOptions.Value.Neo4jDatabase ?? "neo4j";
        }

        public async Task<List<Movie>> GetMovies(int skip, int limit)
        {
            var session = CreateAsyncSession();

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "skip", skip }, { "limit", limit } };

            try
            {
                var movies = new List<Movie>();

                var query = @"MATCH (m:Movie)<-[act:ACTED_IN]-(a:Actor) RETURN m.title, m.releaseYear, m.genres, m.avgVote
                                    ORDER BY m.releaseYear DESC SKIP $paging.skip LIMIT $paging.limit;";

                var paging = new
                {
                    paging = parameters
                };

                var result = await session.ReadTransactionAsync(async tx =>
                {
                    var res = await tx.RunAsync(query, paging);

                    while (await res.FetchAsync())
                    {
                        var rec = res.Current;

                        var movie = new Movie
                        {
                            Title = rec.Values["m.title"].ToString(),
                            ReleaseYear = (long)(rec.Values["m.releaseYear"] ?? 0),
                            Genres = (List<object>)(rec.Values["m.genres"] ?? null),
                            AverageVote = (double)(rec.Values["m.avgVote"] ?? 0)
                        };

                        movies.Add(movie);
                    }

                    return Task.CompletedTask;
                });

                return movies;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an exception while making database query");
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public IAsyncSession CreateAsyncSession()
        {
            return _driver.AsyncSession(o => o.WithDatabase(_database));
        }
    }
}