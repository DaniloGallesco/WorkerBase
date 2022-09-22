using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Splunk;
using Splunk.Configurations;

namespace ClubeAss.API.Customer.Configurations
{
    public static class SplunkConfig
    {
        public static IApplicationBuilder AddConfigureSplunkConfig(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            SplunkLoggerConfiguration result = new SplunkLoggerConfiguration()
            {
                HecConfiguration = new HECConfiguration()
                {
                    SplunkCollectorUrl = "https://prd-p-y5wu7.splunkcloud.com:8088/services/collector/raw",
                    BatchIntervalInMilliseconds = 5000,
                    BatchSizeCount = 100,
                    ChannelIdType = HECConfiguration.ChannelIdOption.None,
                    Token = "7af4ce6c-fc64-41e8-a3f4-27d0d6483ef7"
                },
                SocketConfiguration = new SocketConfiguration()
                {
                    HostName = "localhost",
                    Port = 8111
                }
            };

            loggerFactory.AddHECRawSplunkLogger(result);
            return app;
        }
    }
}

