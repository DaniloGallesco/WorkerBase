using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System;
using System.Linq;

namespace ClubeAss.API.Customer.Base
{
    public static class LogEnricherExtensions
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("Resource", httpContext.GetMetricsCurrentResourceName());
            diagnosticContext.Set("CorrelationId", (string.IsNullOrEmpty(httpContext.Request.Headers["CorrelationId"].FirstOrDefault()) ? Guid.NewGuid().ToString() : httpContext.Request.Headers["CorrelationId"].FirstOrDefault()));
        }

        public static string GetMetricsCurrentResourceName(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            Endpoint endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;

            return endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
        }
    }
}