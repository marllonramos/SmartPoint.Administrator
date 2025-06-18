using Serilog;
using Serilog.Settings.Configuration;
using ILogger = Serilog.ILogger;

namespace SmartPoint.Administrator.Api.Extensions
{
    public static class LoggingConfig
    {
        public static WebApplicationBuilder UseLogging(this WebApplicationBuilder builder)
        {
            ILogger? logger = null;
            bool dispose = false;
            builder.Host.UseSerilog(logger, dispose);

            return builder;
        }

        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration, new ConfigurationReaderOptions
                {
                    SectionName = "LoggingSettings"
                })
                .Enrich.FromLogContext()
                .CreateLogger();

            return services;
        }
    }
}
