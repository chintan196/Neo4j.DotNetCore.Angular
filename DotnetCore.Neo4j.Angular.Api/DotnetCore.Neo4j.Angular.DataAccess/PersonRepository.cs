using DotnetCore.Neo4j.Angular.DataAccess.Neo4j;
using DotnetCore.Neo4j.Angular.Entities.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        /// <summary>
        /// The neo4j data access
        /// </summary>
        private INeo4jDataAccess _neo4jDataAccess;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger<PersonRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="neo4jDataAccess">The neo4j data access.</param>
        /// <param name="logger">The logger.</param>
        public PersonRepository(INeo4jDataAccess neo4jDataAccess, ILogger<PersonRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        /// <summary>
        /// Searches the name of the person.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>List&lt;Dictionary&lt;System.String, System.Object&gt;&gt;.</returns>
        public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
        {
            var query = @"MATCH (p:Person) WHERE toUpper(p.name) CONTAINS toUpper($searchString) RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "searchString", searchString } };

            var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "p", parameters);

            return persons;
        }

        /// <summary>
        /// Adds a new person
        /// </summary>
        /// <param name="person">Person object</param>
        /// <returns>Boolean to indicate of person save was successful or not.</returns>
        public async Task<bool> AddPerson(Person person)
        {
            if (person != null && !string.IsNullOrWhiteSpace(person.Name))
            {
                var query = @"MERGE (p:Person {name: $name}) ON CREATE SET p.born = $born ON MATCH SET p.born = $born, p.updatedAt = timestamp() RETURN true";
                IDictionary<string, object> parameters = new Dictionary<string, object> { { "name", person.Name }, { "born", person.Born ?? 0 } };
                return await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);
            }
            else
            {
                throw new System.ArgumentNullException(nameof(person), "Person must not be null");
            }
        }

        /// <summary>
        /// Gets the person types.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetPersonTypes()
        {
            var reasons = new List<string> { "Actor", "Director", "Producer", "Reviewer", "Writer", "Follower" };
            reasons.Sort();
            return reasons;
        }
    }
}
