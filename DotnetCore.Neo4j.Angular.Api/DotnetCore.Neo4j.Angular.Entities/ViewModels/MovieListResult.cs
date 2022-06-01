using System.Collections.Generic;

namespace DotnetCore.Neo4j.Angular.Entities.ViewModels
{
    public class MovieListResult
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public long Count { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<Dictionary<string, object>> Data { get; set; }
    }
}
