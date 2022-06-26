using DevInSales.Core.Identity.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace DevInSales.Api.Extensions
{
    public static class AuthenticationSetup
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions));
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions[nameof(JwtOptions.SigningCredentials)]));
            
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)]; 
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.Expiration = int.Parse(jwtOptions[nameof(JwtOptions.Expiration)] ?? "0");
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
