using System;

namespace Neo4j.DotNetCore.Angular.Entities.Common
{
    public class ApplicationSettings
    {
        public Uri Neo4jConnection { get; set; }
        public string Neo4jUser { get; set; }
        public string Neo4jPassword { get; set; }
        public string Neo4jDatabase { get; set; }
    }
}
