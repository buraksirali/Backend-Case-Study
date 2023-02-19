using Backend_Case_Study.Contexts;
using Backend_Case_Study.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Backend_Case_Study.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddDatabaseServices(WebApplicationBuilder builder) {
            var connectionString = builder.Configuration.GetSection("Database:Postgre:ConnectionString").Value;
            builder.Services.AddEntityFrameworkNpgsql().AddDbContextFactory<TableContext>(opt =>
                opt.UseNpgsql(connectionString),
                ServiceLifetime.Singleton
            );
            builder.Services.TryAddSingleton<DbHelper>();
            builder.Services.AddHostedService<DbHelper>();
            return builder;
        }

        public static WebApplicationBuilder AddLoggingAndExceptionHandler(WebApplicationBuilder builder)
        {
            builder.Services.TryAddSingleton<ILoggerFactory, LoggerFactory>();
            builder.Services.TryAddSingleton(typeof(ILogger<>), typeof(Logger<>));
            return builder;
        }
    }
}
