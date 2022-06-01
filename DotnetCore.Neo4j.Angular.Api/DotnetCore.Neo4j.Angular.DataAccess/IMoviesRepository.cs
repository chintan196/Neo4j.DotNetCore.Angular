using DotnetCore.Neo4j.Angular.Entities.Common;
using DotnetCore.Neo4j.Angular.Entities.ViewModels;
using System.Threading.Tasks;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public interface IMoviesRepository
    {
        Task<MovieListResult> SearchMoviesByFilter(MoviesFilter filterObject);
    }
}
