using NoteApp.Abstractions;

namespace NoteApp.Services.Extentions
{
    public static class UserRepositoryExtention
    {
        public static IServiceCollection AddUserRepository(this IServiceCollection services)
            => services.AddScoped<IUserRepository, UserRepository>();
    }
}
