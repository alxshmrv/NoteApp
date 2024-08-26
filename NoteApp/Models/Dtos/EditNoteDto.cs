using NoteApp.Models.DbSet;

namespace NoteApp.Models.Dtos
{
    public record EditNoteDto(
        string Name,
        string Description,
        bool IsCompleted,
        Priority Priority);
}
