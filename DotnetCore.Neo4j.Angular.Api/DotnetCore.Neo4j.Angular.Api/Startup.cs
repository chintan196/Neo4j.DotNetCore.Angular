using DotnetCore.Neo4j.Angular.DataAccess;
using DotnetCore.Neo4j.Angular.Entities.Common;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace DotnetCore.Neo4j.Angular.Api
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The allowed origins
        /// </summary>
        string _allowedOrigins = "_allowedOrigins";

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Configuration.GetValue<string>("ApplicationName"),
                    Description = "API for Project Sherlock"
                });
            });

            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddHttpContextAccessor();

            services.AddCors(o => o.AddPolicy(_allowedOrigins, builder =>
            {
                builder.WithOrigins(Configuration.GetValue<string>("AllowedCorsOrigins").Split(";"))
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(o => {
                o.SuppressModelStateInvalidFilter = true;
            });

            var appSettings = new ApplicationSettings();
            Configuration.GetSection("ApplicationSettings").Bind(appSettings);

            services.RegisterDataAccessDependencies(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseCors(_allowedOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
