using DotnetCore.Neo4j.Angular.DataAccess.Neo4j;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public class StatisticsRepository : IStatisticsRepository
    {
        /// <summary>
        /// The neo4j data access
        /// </summary>
        private INeo4jDataAccess _neo4jDataAccess;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<StatisticsRepository> _logger;

        public StatisticsRepository(INeo4jDataAccess neo4jDataAccess, ILogger<StatisticsRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        public async Task<Dictionary<string, object>> GetEntityWiseCount()
        {
            var data = new Dictionary<string, object>();

            var query = @"Match (m:Movie) RETURN count(distinct m) as totalMovies";

            var result = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);

            data.Add("Movies", result);

            query = @"Match (m:Movie)<-[:ACTED_IN]-(p:Person) RETURN count(distinct p) as totalActors";

            result = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);

            data.Add("Actors", result);

            query = @"Match (m:Movie)<-[:DIRECTED]-(p:Person) RETURN count(distinct p) as totalDirectors";

            result = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);

            data.Add("Directors", result);

            query = @"Match (m:Movie)<-[:PRODUCED]-(p:Person) RETURN count(distinct p) as totalDirectors";

            result = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);

            data.Add("Producers", result);

            return data;
        }
    }
}
