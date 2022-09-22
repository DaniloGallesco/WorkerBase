using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class PrometheusConfig
    {
        public static IServiceCollection AddServicePrometheusConfig(this IServiceCollection services)
        {

            throw new NotImplementedException();
        }

        public static IApplicationBuilder AddConfigurePrometheusConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMetricServer();
            app.UseHttpMetrics();

            return app;
        }
    }
}
