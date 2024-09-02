using NoteApp.Abstractions;

namespace NoteApp.Services.Extentions
{
    public static class JwtTonkenGeneratorExtention
    {
        public static IServiceCollection AddJwtTokenGenerator(this IServiceCollection services)
    => services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

    }
}
