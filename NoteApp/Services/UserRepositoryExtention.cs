using NoteApp.Abstractions;

namespace NoteApp.Services
{
    public static class UserRepositoryExtention
    {
        public static IServiceCollection AddUserRepository(this IServiceCollection services)
            => services.AddSingleton<IUserRepository, UserRepository>();
    }
}
