using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess.Neo4j
{
    /// <summary>
    /// Interface INeo4jDataAccess
    /// Implements the <see cref="IAsyncDisposable" />
    /// </summary>
    /// <seealso cref="IAsyncDisposable" />
    public interface INeo4jDataAccess : IAsyncDisposable
    {
        /// <summary>
        /// Executes the read list asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="returnObjectKey">The return object key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        Task<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object> parameters = null);
        /// <summary>
        /// Executes the read dictionary asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="returnObjectKey">The return object key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Task&lt;List&lt;Dictionary&lt;System.String, System.Object&gt;&gt;&gt;.</returns>
        Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object> parameters = null);
        /// <summary>
        /// Executes the read scalar asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object> parameters = null);
    }
}
