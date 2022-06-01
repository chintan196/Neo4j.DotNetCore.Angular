using DotnetCore.Neo4j.Angular.Entities.Domain;

namespace DotnetCore.Neo4j.Angular.Entities.Common
{
    public class MoviesFilter
    {
        /// <summary>
        /// Gets or sets the Director.
        /// </summary>
        /// <value>The director.</value>
        public Person Director { get; set; }
        
        /// <summary>
        /// Gets or sets the Producer.
        /// </summary>
        /// <value>The producer.</value>
        public Person Producer { get; set; }

        /// <summary>
        /// Gets or sets the Writer.
        /// </summary>
        /// <value>The writer.</value>
        public Person Writer { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        /// <value>The movie release year.</value>
        public int Released { get; set; }

        /// <summary>
        /// Gets or sets the movie title.
        /// </summary>
        /// <value>The movie title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Gets or sets the sort by field.
        /// </summary>
        /// <value>The sort by field.</value>
        public string SortByField { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public string SortOrder { get; set; }
    }
}
