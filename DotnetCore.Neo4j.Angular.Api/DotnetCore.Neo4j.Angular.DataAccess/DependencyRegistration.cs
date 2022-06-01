using DotnetCore.Neo4j.Angular.DataAccess.Neo4j;
using DotnetCore.Neo4j.Angular.Entities.Common;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver;

namespace DotnetCore.Neo4j.Angular.DataAccess
{
    public static class DependencyRegistration
    {
        /// <summary>
        /// Registers the data access dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="settings">The settings.</param>
        public static void RegisterDataAccessDependencies(this IServiceCollection services, ApplicationSettings settings)
        {
            services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));
            services.AddScoped<INeo4jDataAccess, Neo4jDataAccess>();
            services.AddTransient<IMoviesRepository, MoviesRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
        }
    }
}
