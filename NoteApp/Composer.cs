using Microsoft.EntityFrameworkCore;
using NoteApp.Database;
using NoteApp.Services;
using NoteApp.Services.Extentions;
namespace NoteApp
{
    public static class Composer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<NoteAppDbContext>(options =>
            {
            options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Son123456;Database=NoteApp");
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

            return services;
        }
    }
}
