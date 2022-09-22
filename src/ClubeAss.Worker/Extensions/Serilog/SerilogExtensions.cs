using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Filters;
using System;

namespace ClubeAss.API.Customer.Base
{
    public static class SerilogExtensions
    {
        public static void AddSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Debug()
                .Enrich.WithProperty("ApplicationName", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .Enrich.WithProperty("ApplicationVersion", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)
                .Enrich.FromLogContext()

                .Enrich.WithEnvironmentUserName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}