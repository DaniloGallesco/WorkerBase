using System.Diagnostics.CodeAnalysis;

namespace ClubeAss.API.Customer.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class KeyCloakConfig
    {

        //        public static IServiceCollection AddKeyCloakConfig(this IServiceCollection services, IConfiguration configuration)
        //        {
        //#if DEBUG
        //            IdentityModelEventSource.ShowPII = true; //Add this line
        //#endif
        //            //var login = configuration.GetSection("Keycloack").GetSection("ServerLogin").Value;
        //            //var schema = configuration.GetSection("Keycloack").GetSection("Schema").Value;
        //            //var key = Encoding.UTF8.GetBytes("MIICmTCCAYECBgF8STyZXTANBgkqhkiG9w0BAQsFADAQMQ4wDAYDVQQDDAVUZXN0ZTAeFw0yMTEwMDQwMjU2MjBaFw0zMTEwMDQwMjU4MDBaMBAxDjAMBgNVBAMMBVRlc3RlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzSe+895nmW1Cz4JcL9w/Ao9u+Gl6ghgdUVlKZOOCrZeg7EN0qZmVBj+bUKOIuCwsGmfKWiKa8H2J9FD0kaQz7Q3xunOAtc89PrAf/qvswIUsS5SWRmULtzQ8tXME579vrqo6PnObyU1EMZT+qTWWNTcVTGn2WZw4m3gZm5tVYONEf6EdiDWsXwE+SR7YG9XZR9ZbSCZzYi5F1JJJuYXTNjT3WpIYZSijWZwrftFCjli0wlOO+mYMe1EvGT/VqFOht3K3XvBhXZe+bdigNWtfecUs0XFM3FtOub8S13Zure/wI3TS370wWPlN69dXSvI0Xw+UGjFd7fIZtGFsZ336EwIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCIz69rmoNJ0vx0cAnZcEslbrkYD+n/+YejsCZyR6hlEHr9qkAUcy7hU/r/EQLPB33vrb+HJfTIsjldGoiUXbWb1q2EZl41uxbXXwm5ojislHWWNgKol0EfynbtpvsmLBurCpBRZOOfLEogcGt+K5PyBjpazK4hfu/CTMt0xTxxZvyu8Jwz0NLRVEupxp69aYFnPaKtaZ8W3TP/qoPWiz01nth+Uaa3ClmXe1WhVjnyb1ZA2bQ9iQWxnRGO+mOgbih1LmknEU7P76UPwL9XY12EHNnniEbzkIMwDkHNQ77NN7N6MJZMAosMl0k7ubMp9fepK2zOh7ta0lwk+WpDlvSZ");
        //            ////var certificate = new X509Certificate2(Convert.FromBase64String("8d45f580-8612-4f77-8b68-b8bb28256b86"));

        //            //using RSA rsa = RSA.Create();
        //            //rsa.ImportRSAPublicKey(key, out _);

        //            //var securityKey = new SymmetricSecurityKey(key);
        //            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //            //rsa.ImportParameters(
        //            //  new RSAParameters()
        //            //  {
        //            //        Modulus = FromBase64Url("w7Zdfmece8iaB0kiTY8pCtiBtzbptJmP28nSWwtdjRu0f2GFpajvWE4VhfJAjEsOcwYzay7XGN0b-X84BfC8hmCTOj2b2eHT7NsZegFPKRUQzJ9wW8ipn_aDJWMGDuB1XyqT1E7DYqjUCEOD1b4FLpy_xPn6oV_TYOfQ9fZdbE5HGxJUzekuGcOKqOQ8M7wfYHhHHLxGpQVgL0apWuP2gDDOdTtpuld4D2LK1MZK99s9gaSjRHE8JDb1Z4IGhEcEyzkxswVdPndUWzfvWBBWXWxtSUvQGBRkuy1BHOa4sP6FKjWEeeF7gm7UMs2Nm2QUgNZw6xvEDGaLk4KASdIxRQ"),
        //            //      Exponent = FromBase64Url("AQAB")
        //            //  });



        //            //new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keycloack:ClientSecret"])), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)

        //            //var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keycloack:ClientSecret"]));

        //            //services.AddAuthentication(x =>
        //            //{
        //            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //            //    //x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        //            //})
        //            // .AddJwtBearer(x =>
        //            // {
        //            //     x.IncludeErrorDetails = true; // <- great for debugging

        //            //     // Configure the actual Bearer validation
        //            //     x.TokenValidationParameters = new TokenValidationParameters
        //            //     {

        //            //         ValidateIssuerSigningKey = true,
        //            //         IssuerSigningKey = symmetricKey,
        //            //         ValidateAudience = false,
        //            //         //ValidAudience = "TESTE_API",
        //            //         ValidateIssuer = true,
        //            //         ValidIssuer = "http://localhost:5555/auth/realms/TESTE_API",


        //            //         RequireSignedTokens = true,
        //            //         RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
        //            //         ValidateLifetime = true, // <- the "exp" will be validated
        //            //     };
        //            // });
        //            services.AddCors();
        //            services.AddAuthorization();        
        //            return services;
        //        }

        //        public static IApplicationBuilder AddConfigureKeyCloakConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        //        {
        //            //app.UseCors(x => x
        //            //               .AllowAnyOrigin()
        //            //               .AllowAnyMethod()
        //            //               .AllowAnyHeader());

        //            app.UseAuthentication();
        //            app.UseAuthorization();

        //            return app;

        //        }


    }
}
