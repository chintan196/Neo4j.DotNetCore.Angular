using System;

namespace DotnetCore.Neo4j.Angular.Entities.Common
{
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the neo4j connection.
        /// </summary>
        /// <value>The neo4j connection.</value>
        public Uri Neo4jConnection { get; set; }
        /// <summary>
        /// Gets or sets the neo4j user.
        /// </summary>
        /// <value>The neo4j user.</value>
        public string Neo4jUser { get; set; }
        /// <summary>
        /// Gets or sets the neo4j password.
        /// </summary>
        /// <value>The neo4j password.</value>
        public string Neo4jPassword { get; set; }
        /// <summary>
        /// Gets or sets the neo4j database.
        /// </summary>
        /// <value>The neo4j database.</value>
        public string Neo4jDatabase { get; set; }
    }
}
