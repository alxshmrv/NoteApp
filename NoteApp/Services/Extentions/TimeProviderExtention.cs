using NoteApp.Abstractions;

namespace NoteApp.Services.Extentions
{
    public static class TimeProviderExtention
    {
        public static IServiceCollection AddTimeProvider(this IServiceCollection services)
            => services.AddTransient<ITimeProvider, TimeProvider>();
    }
}
