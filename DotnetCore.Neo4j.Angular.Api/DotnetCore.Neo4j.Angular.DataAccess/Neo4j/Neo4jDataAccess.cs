using DotnetCore.Neo4j.Angular.Entities.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess.Neo4j
{
    /// <summary>
    /// Class Neo4jDataAccess.
    /// Implements the <see cref="INeo4jDataAccess" />
    /// </summary>
    /// <seealso cref="INeo4jDataAccess" />
    public class Neo4jDataAccess : INeo4jDataAccess
    {
        /// <summary>
        /// The session
        /// </summary>
        private IAsyncSession _session;
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<Neo4jDataAccess> _logger;
        /// <summary>
        /// The database
        /// </summary>
        private string _database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Neo4jDataAccess"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="appSettingsOptions">The application settings options.</param>
        public Neo4jDataAccess(IDriver driver, ILogger<Neo4jDataAccess> logger, IOptions<ApplicationSettings> appSettingsOptions)
        {
            _logger = logger;
            _database = appSettingsOptions.Value.Neo4jDatabase ?? "neo4j";
            _session = driver.AsyncSession(o => o.WithDatabase(_database));
        }

        /// <summary>
        /// Execute read list as an asynchronous operation.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="returnObjectKey">The return object key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
        {
            return await ExecuteReadTransactionAsync<string>(query, returnObjectKey, parameters);
        }

        /// <summary>
        /// Execute read dictionary as an asynchronous operation.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="returnObjectKey">The return object key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
        {
            return await ExecuteReadTransactionAsync<Dictionary<string, object>>(query, returnObjectKey, parameters);
        }

        /// <summary>
        /// Execute read scalar as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null)
        {
            try
            {
                parameters = parameters == null ? new Dictionary<string, object>() : parameters;

                var result = await _session.ReadTransactionAsync(async tx =>
                {
                    T scalar = default(T);

                    var res = await tx.RunAsync(query, parameters);

                    scalar = (await res.SingleAsync())[0].As<T>();

                    return scalar;
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an exception while making database query");
                throw;
            }
        }

        /// <summary>
        /// Execute write transaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A Task that returns a scalar upon completion, either return a boolean or a single system data type value</returns>
        public async Task<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null)
        {
            try
            {
                parameters = parameters == null ? new Dictionary<string, object>() : parameters;

                var result = await _session.WriteTransactionAsync(async tx =>
                {
                    T scalar = default(T);

                    var res = await tx.RunAsync(query, parameters);

                    scalar = (await res.SingleAsync())[0].As<T>();

                    return scalar;
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an exception while making database query");
                throw;
            }
        }

        /// <summary>
        /// Execute read transaction as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="returnObjectKey">The return object key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A Task representing the asynchronous operation that returns List<T> on completion.</returns>
        private async Task<List<T>> ExecuteReadTransactionAsync<T>(string query, string returnObjectKey, IDictionary<string, object>? parameters)
        {
            try
            {                
                parameters = parameters == null ? new Dictionary<string, object>() : parameters;

                var result = await _session.ReadTransactionAsync(async tx =>
                {
                    var data = new List<T>();

                    var res = await tx.RunAsync(query, parameters);

                    var records = await res.ToListAsync();

                    data = records.Select(x => (T)x.Values[returnObjectKey]).ToList();

                    return data;
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an exception while making database query");
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources asynchronously.
        /// </summary>
        /// <returns>ValueTask.</returns>
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _session.CloseAsync();
        }
    }
}
