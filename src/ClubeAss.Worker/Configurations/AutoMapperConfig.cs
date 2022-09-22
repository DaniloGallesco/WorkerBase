using AutoMapper;
using ClubeAss.API.Customer.ViewModel.Customer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddServiceAutoMapperConfig(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerResponse, Domain.Customer>();
                cfg.CreateMap<Domain.Customer, CustomerResponse>();

                cfg.CreateMap<CustomerAddRequest, Domain.Customer>();
                cfg.CreateMap<Domain.Customer, CustomerAddRequest>();

                cfg.CreateMap<CustomerUpdateRequest, Domain.Customer>();
                cfg.CreateMap<Domain.Customer, CustomerUpdateRequest>();

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IApplicationBuilder AddConfigureAutoMapperConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}