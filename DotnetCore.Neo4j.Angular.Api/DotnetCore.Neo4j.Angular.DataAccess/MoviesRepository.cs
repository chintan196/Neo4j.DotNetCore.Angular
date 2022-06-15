﻿using DotnetCore.Neo4j.Angular.DataAccess.Neo4j;
using DotnetCore.Neo4j.Angular.Entities.Common;
using DotnetCore.Neo4j.Angular.Entities.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public class MoviesRepository : IMoviesRepository
    {
        /// <summary>
        /// The neo4j data access
        /// </summary>
        private INeo4jDataAccess _neo4jDataAccess;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<MoviesRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesRepository"/> class.
        /// </summary>
        /// <param name="neo4jDataAccess">The neo4j data access.</param>
        /// <param name="logger">The logger.</param>
        public MoviesRepository(INeo4jDataAccess neo4jDataAccess, ILogger<MoviesRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        /// <summary>
        /// Searches the movies by filter.
        /// </summary>
        /// <param name="filterObject">The filter object.</param>
        /// <returns>Task&lt;MovieListResult&gt;.</returns>
        public async Task<MovieListResult> SearchMoviesByFilter(MoviesFilter filterObject)
        {
            var result = new MovieListResult();

            result.Data = await PopulateMoviesByFilter
                (filterObject);
            result.Count = await GetCountByFilter(filterObject);

            return result;
        }

        /// <summary>
        /// Populates the movies by filter.
        /// </summary>
        /// <param name="filterObject">The filter object.</param>
        /// <returns>List&lt;Dictionary&lt;System.String, System.Object&gt;&gt;.</returns>
        private async Task<List<Dictionary<string, object>>> PopulateMoviesByFilter(MoviesFilter filterObject)
        {
            string query; IDictionary<string, object> parameters;

            (query, parameters) = PrepareMovieSearchArgumentsByFilter(filterObject);

            var movies = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "m", parameters);
            return movies;
        }

        /// <summary>
        /// Gets the count by filter.
        /// </summary>
        /// <param name="filterObject">The filter object.</param>
        /// <returns>System.Int64.</returns>
        private async Task<long> GetCountByFilter(MoviesFilter filterObject)
        {
            string query; IDictionary<string, object> parameters;

            (query, parameters) = PrepareMovieSearchArgumentsByFilter(filterObject, true);

            var count = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query, parameters);

            return count;
        }

        /// <summary>
        /// Prepares the movie search arguments by filter.
        /// </summary>
        /// <param name="filterObject">The filter object.</param>
        /// <param name="countOnly">if set to <c>true</c> [count only].</param>
        /// <returns>Tuple&lt;System.String, IDictionary&lt;System.String, System.Object&gt;&gt;.</returns>
        private Tuple<string, IDictionary<string, object>> PrepareMovieSearchArgumentsByFilter(MoviesFilter filterObject, bool countOnly = false)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>();

            var match = @"Match (m:Movie)<-[:PRODUCED]-(prod:Person)
                            OPTIONAL MATCH (m)<-[:DIRECTED]-(dir:Person) 
                            OPTIONAL MATCH (m)<-[:WROTE]-(w:Person) ";

            var filter = new StringBuilder(@" WHERE 1 = 1 ");

            if (filterObject.Director != null && !string.IsNullOrWhiteSpace(filterObject.Director?.Name))
            {                
                filter.Append(" AND toUpper(dir.name) CONTAINS toUpper($director)");
                parameters.Add("director", filterObject.Director.Name);
            }

            if (filterObject.Writer != null && !string.IsNullOrWhiteSpace(filterObject.Writer?.Name))
            {
                filter.Append(" AND toUpper(w.name) CONTAINS toUpper($writer)");
                parameters.Add("writer", filterObject.Writer.Name);
            }

            if (filterObject.Producer != null && !string.IsNullOrWhiteSpace(filterObject.Producer?.Name))
            {
                filter.Append(" AND toUpper(prod.name) CONTAINS toUpper($producer)");
                parameters.Add("producer", filterObject.Producer.Name);
            }

            if (filterObject.Released != 0)
            {
                filter.Append(" AND m.released = $released ");
                parameters.Add("released", filterObject.Released);
            }

            if (!string.IsNullOrWhiteSpace(filterObject.Title))
            {
                filter.Append(" AND toUpper(m.title) CONTAINS toUpper($title) ");
                parameters.Add("title", filterObject.Title);
            }

            var withClause = @" WITH m, collect(distinct dir.name) as dirNames, collect(distinct w.name) as wNames, collect(distinct prod.name) as prodNames ";

            var results = countOnly ? " RETURN COUNT(m) " : @" RETURN m { title: m.title, tagLine: m.tagline, released: m.released,
                                                                director: apoc.text.join(dirNames, ', '),
                                                                producer: apoc.text.join(prodNames, ', '),
                                                                writer: apoc.text.join(wNames, ', ') }  ";

            var orderBy = filterObject.SortByField;

            var format = countOnly ? string.Empty : $" ORDER BY m.{orderBy} {filterObject.SortOrder} SKIP $skip  LIMIT $limit; ";

            parameters.Add("skip", (filterObject.PageSize == 0 ? 25 : filterObject.PageSize) * filterObject.CurrentPage);

            parameters.Add("limit", filterObject.PageSize == 0 ? 25 : filterObject.PageSize);

            var query = $" {match} {filter} {withClause} {results} {format} ";

            return Tuple.Create(query, parameters);
        }
    }
}
