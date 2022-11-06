using ClubeAss.API.Customer.Configurations;
using ClubeAss.Worker.Customer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.Worker
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddServiceDependencyInjectionConfig(Configuration);
            services.AddServiceHealthcheckConfig(Configuration);
            services.AddServiceAutoMapperConfig();
            services.AddDatabaseConfig(Configuration);
            services.AddServiceCircuitBreakerConfig(services.BuildServiceProvider());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.AddConfigureSerilogConfig(env);
            app.AddConfigureHealthcheckConfig(env);
            app.AddConfigurePrometheusConfig(env);
        }
    }
}
