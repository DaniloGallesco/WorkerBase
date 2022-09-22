using ClubeAss.API.Customer.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SerilogConfig
    {
        public static IServiceCollection AddServiceSerilogConfig(this IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public static IApplicationBuilder AddConfigureSerilogConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricherExtensions.EnrichFromRequest);

            app.UseMiddleware<ErrorHandlingMiddleware>();

            return app;
        }
    }
}