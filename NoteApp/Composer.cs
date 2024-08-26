using NoteApp.Services;
using NoteApp.Services.Extentions;
namespace NoteApp
{
    public static class Composer
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
