using NoteApp.Abstractions;

namespace NoteApp.Services
{
    public static class NoteRepositoryExtention
    {
        public static IServiceCollection AddNoteRepository(this IServiceCollection services)
            => services.AddSingleton<INoteRepository, NoteRepository>();
    }
}
