using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using NoteApp.Politics;

namespace NoteApp.Services.Extentions
{
    public static class AuthorizationExtention
    {
        public static IServiceCollection AddAuthorizationWithSettings (this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, NoteOwnerRequirementHandler>();
            services.AddHttpContextAccessor();
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder =
                    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                options.AddPolicy("NotesOwner", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new NoteOwnerRequirement());
                });
            });
            return services;
        }

    }
}
