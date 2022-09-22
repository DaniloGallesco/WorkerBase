using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static IServiceCollection AddServiceSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {

            var schema = configuration.GetSection("Keycloack").GetSection("Schema").Value;

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                // To Enable authorization using Swagger (JWT)  
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });

                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "API Core - Customer",
                        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                        Description = "Api - Customer",
                        TermsOfService = new Uri("https://www.google.com.br/"),
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Api Customer",
                            Email = string.Empty,
                            Url = new Uri("https://www.google.com.br/"),
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "© Copyright ClubeAss. Todos os Direitos Reservados.",
                            Url = new Uri("https://www.google.com.br/"),
                        }
                    });
            });

            return services;
        }

        public static IApplicationBuilder AddConfigureSwaggerConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = "api/swagger";
            });

            return app;
        }
    }
}