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
            //var formatter = new MessageTemplateTextFormatter(
            //    "{\"event\":\"aaaaa\"}",
            //    formatProvider: null);

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

                .WriteTo.EventCollector(
                            "https://prd-p-y5wu7.splunkcloud.com:8088",
                            "7af4ce6c-fc64-41e8-a3f4-27d0d6483ef7", "/services/collector/raw"
                            )

                //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticsearchSettings:uri"]))
                //{
                //    AutoRegisterTemplate = true,
                //    IndexFormat = "indexlogs",
                //    ModifyConnectionSettings = x => x.BasicAuthentication(configuration["ElasticsearchSettings:username"], configuration["ElasticsearchSettings:password"])
                //})
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                //.WriteTo.Console()
                .CreateLogger();
        }
    }
}