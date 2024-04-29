using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.ProductAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            // Parse the ApiSettings Section into a variable to demonstrate 'GetSection' method
            var settingsSection = builder.Configuration.GetSection("ApiSettings");


            // Retrieve JWT secret, issuer, and audience from configuration
            var secret = settingsSection.GetValue<string>("Secret");
            var issuer = settingsSection.GetValue<string>("Issuer");
            var audience = settingsSection.GetValue<string>("Audience");

            // Convert the secret into a byte array
            var key = Encoding.ASCII.GetBytes(secret);

            // Add JWT authentication services to the DI container
            builder.Services.AddAuthentication(x =>
            {
                // Specify the default authentication scheme for both authentication and challenge responses
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                // Configure the JWT bearer options
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // Enable validation of the JWT signature using the specified key
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // Validate the JWT issuer (if specified) against the expected issuer
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    // Validate the JWT audience (if specified) against the expected audience
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }
    }
}
