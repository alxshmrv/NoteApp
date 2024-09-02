using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Abstractions;
using NoteApp.Configurations;
using System.Security.Claims;
using System.Text;

namespace NoteApp.Services.Extentions
{
    public static class AuthenticationExtantion
    {

        public static IServiceCollection AddAuth(
               this IServiceCollection services,
             IConfiguration configuration)
        {
            JwtSettings jwtSettings = new();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddAuthentication(defaultScheme:
                JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true, 
                        ValidateAudience = true, 
                        ValidateLifetime = true, 
                        ValidateIssuerSigningKey = true, 
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context => 
                        {
                            var tokensRepository =
                                context.HttpContext
                                    .RequestServices
                                    .GetRequiredService<IJwtTokensRepository>();
                            var userId = context.Principal?.FindFirst(
                                ClaimTypes.NameIdentifier)?.Value;
                            if (
                                userId is null
                                || context.SecurityToken.ValidTo < DateTime.UtcNow
                                || !tokensRepository.Verify(
                                    int.Parse(userId),
                                    context.SecurityToken.UnsafeToString()))
                            {
                                context.Fail("Unauthorized");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }
}

