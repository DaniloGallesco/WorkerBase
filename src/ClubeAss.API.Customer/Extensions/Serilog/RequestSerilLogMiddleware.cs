using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System.Linq;
using System.Threading.Tasks;

namespace ClubeAss.API.Customer.Base
{
    public class RequestSerilLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSerilLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("UserName", context?.User?.Identity?.Name ?? "anônimo"))
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                return _next.Invoke(context);
            }
        }

        private string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Correlation-Id", out StringValues correlationId);
            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
    }
}