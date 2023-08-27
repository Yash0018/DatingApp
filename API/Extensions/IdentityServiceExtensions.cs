using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        // Extension method to add identity-related services to the IServiceCollection.
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Add authentication services with JWT Bearer as the default scheme.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Configure token validation parameters for JWT Bearer authentication.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,

                        // Set the issuer signing key using a symmetric security key derived from the provided TokenKey.
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),

                        // Disable issuer validation (not checking the token issuer's validity).
                        ValidateIssuer = false,

                        // Disable audience validation (not checking the intended audience of the token).
                        ValidateAudience = false,
                    };
                });
            return services;
        }
    }
}
