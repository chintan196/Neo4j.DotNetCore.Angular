using DotnetCore.Neo4j.Angular.Api;
using NLog.Web;

// NLog: setup the logger 
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();

    var startup = new Startup(builder.Configuration);

    // Manually call ConfigureServices()
    startup.ConfigureServices(builder.Services);

    logger.Debug("Init-Start");

    var app = builder.Build();

    startup.Configure(app, app.Environment);

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Something went wrong while starting the application!");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads
    NLog.LogManager.Shutdown();
}