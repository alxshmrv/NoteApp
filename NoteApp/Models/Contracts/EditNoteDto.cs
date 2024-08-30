using NoteApp.Models.DbSet;

namespace NoteApp.Models.Contracts
{
    public record EditNoteDto(
        string Name,
        string Description,
        bool IsCompleted,
        Priority Priority);
}
