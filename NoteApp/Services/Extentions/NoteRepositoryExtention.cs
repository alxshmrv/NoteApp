using NoteApp.Abstractions;

namespace NoteApp.Services.Extentions
{
    public static class NoteRepositoryExtention
    {
        public static IServiceCollection AddNoteRepository(this IServiceCollection services)
            => services.AddSingleton<INoteRepository, NoteRepository>();
    }
}
