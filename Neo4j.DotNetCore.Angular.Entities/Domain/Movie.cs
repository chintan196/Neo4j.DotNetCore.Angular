using System.Collections.Generic;

namespace Neo4j.DotNetCore.Angular.Entities.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public double AverageVote { get; set; }
        public List<object> Genres { get; set; }
        public long ReleaseYear { get; set; }
        public string Title { get; set; }
    }
}
