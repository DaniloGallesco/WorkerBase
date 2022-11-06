using ClubeAss.Worker.Resilience;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.Worker.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class CircuitBreakerConfig
    {
        public static IServiceCollection AddServiceCircuitBreakerConfig(this IServiceCollection services, IServiceProvider serviceProvider)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(CircuitBreaker.CreatePolicy());


            return services;
        }

        public static IApplicationBuilder AddConfigureCircuitBreakerConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app;
        }
    }
}