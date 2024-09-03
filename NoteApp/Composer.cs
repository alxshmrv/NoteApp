using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NoteApp.Configurations;
using NoteApp.Configurations.Database;
using NoteApp.Database;
using NoteApp.Services;
using NoteApp.Services.Extentions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NoteApp
{
    public static class Composer
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(assembly);
            services.AddFluentValidationAutoValidation();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddAuthentication();
            services.AddAuthorizationWithSettings();

            services.Configure<NoteAppDbConnectionSettings>(
                configuration.GetRequiredSection(
                    nameof(NoteAppDbConnectionSettings)));

            services.AddDbContext<NoteAppDbContext>(options =>
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var settings = scope
                .ServiceProvider
                .GetRequiredService<IOptions<NoteAppDbConnectionSettings>>()
                .Value;
                options.UseNpgsql(settings.ConnectionString);
            });

            return services;
        }
        public static IServiceCollection AddApplicationServices(
    this IServiceCollection services)
        {
            services.AddExceptionHandler<ExceptionHandler>();

            services.AddUserRepository();

            services.AddNoteRepository();

            services.AddTimeProvider();

            services.AddJwtTokenGenerator();

            services.AddJwtTokenRepository();
           

            return services;
        }
    }
}
