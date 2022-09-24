using ClubeAss.Application;
using ClubeAss.Domain.Interface.Application;
using ClubeAss.Domain.Interface.Repository;
using ClubeAss.Domain.Worker;
using ClubeAss.Repository.Ef;
using ClubeAss.Worker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddServiceDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            #region Application
            services.AddTransient<ICustomerApplication, CustomerApplication>();
            #endregion

            #region Repository
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            #endregion


            #region Worker
            services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();
            #endregion

            return services;
        }

        public static IApplicationBuilder AddConfigureDependencyInjectionConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}