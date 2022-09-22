using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class HealthcheckConfig
    {
        public static IServiceCollection AddServiceHealthcheckConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("PGConexao"), name: "Postgres", tags: new string[] { "db", "data" }).ForwardToPrometheus();


            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency                
                opt.AddHealthCheckEndpoint("default api", "/healthcheck"); //map health check api
            })
            .AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder AddConfigureHealthcheckConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Gera o endpoint que retornará os dados utilizados no dashboard
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
            });

            return app;
        }
    }
}