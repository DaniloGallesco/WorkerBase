using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prometheus;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Compression;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class AppConfig
    {
        public static IServiceCollection AddServiceAppConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>{
                        new CultureInfo("pt-BR")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddOptions();


            return services;
        }

        public static IApplicationBuilder AddConfigureAppConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(localizeOptions.Value);

            return app;
        }
    }
}