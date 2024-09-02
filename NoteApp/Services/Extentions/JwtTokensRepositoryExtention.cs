using NoteApp.Abstractions;

namespace NoteApp.Services.Extentions
{
    public static class JwtTokensRepositoryExtention
    {
        public static IServiceCollection AddJwtTokenRepository(this IServiceCollection services)
        => services.AddSingleton<IJwtTokensRepository, JwtTokensRepository>();

    }
}
