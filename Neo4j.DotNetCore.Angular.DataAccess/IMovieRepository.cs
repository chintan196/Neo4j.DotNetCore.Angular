using Neo4j.DotNetCore.Angular.Entities.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4j.DotNetCore.Angular.DataAccess
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies(int skip, int limit);
    }
}