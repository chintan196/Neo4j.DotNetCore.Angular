using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Searches the name of the persons.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.Collections.Generic.List&lt;System.Collections.Generic.Dictionary&lt;string, object&gt;&gt;&gt;.</returns>
        Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString);

        /// <summary>
        /// Gets the person types.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetPersonTypes();
    }
}
