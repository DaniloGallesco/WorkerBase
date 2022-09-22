using ClubeAss.API.Customer.Validators.Customer;
using ClubeAss.API.Customer.ViewModel.Customer;
using ClubeAss.Application;
using ClubeAss.Domain.Interface.Application;
using ClubeAss.Domain.Interface.Repository;
using ClubeAss.Repository.Ef;
using FluentValidation;
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
            //services.AddScoped<IDbSession, DbSession>();
            //services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(configuration.GetConnectionString("PgConexao")));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            #endregion

            #region FluentValidator            
            services.AddScoped<IValidator<CustomerAddRequest>, CustomerAddRequestValidator>();
            services.AddScoped<IValidator<CustomerDeleteRequest>, CustomerDeleteRequestValidator>();
            services.AddScoped<IValidator<CustomerGetRequest>, CustomerGetRequestValidator>();
            services.AddScoped<IValidator<CustomerUpdateRequest>, CustomerUpdateRequestValidator>();
            #endregion

            return services;
        }

        public static IApplicationBuilder AddConfigureDependencyInjectionConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}