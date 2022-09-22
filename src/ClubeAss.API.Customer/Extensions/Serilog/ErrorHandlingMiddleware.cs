using ClubeAss.Domain.Commands;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClubeAss.API.Customer.Base
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, "Erro não tratado");

            var options = new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
            };

            var result = JsonSerializer.Serialize(new BaseResponse(HttpStatusCode.InternalServerError, null, new List<string> { exception.Message }), options);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            return context.Response.WriteAsync(result);
        }
    }
}