using ClubeAss.Repository.Ef.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class EFDatabaseConfig
    {

        public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<BaseContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PgConexao")));

            services.AddScoped<BaseContext>();
        }
    }
}
