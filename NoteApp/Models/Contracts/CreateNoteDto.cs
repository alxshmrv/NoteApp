using NoteApp.Models.DbSet;

namespace NoteApp.Models.Contracts
{
    public record CreateNoteDto(
        string Name,
        Priority Priority,
        string? Description);
}
