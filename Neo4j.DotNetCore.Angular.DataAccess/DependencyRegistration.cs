using Microsoft.Extensions.DependencyInjection;
using Neo4j.DotNetCore.Angular.Entities.Common;
using Neo4j.Driver;

namespace Neo4j.DotNetCore.Angular.DataAccess
{
    public static class DependencyRegistration
    {
        public static void RegisterDataAccessDependencies(this IServiceCollection services, ApplicationSettings settings)
        {
            services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));
            services.AddTransient<IMovieRepository, MovieRepository>();
        }
    }
}
