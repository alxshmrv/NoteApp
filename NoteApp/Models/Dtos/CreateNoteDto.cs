using NoteApp.Models.DbSet;

namespace NoteApp.Models.Dtos
{
    public record CreateNoteDto(
        string Name,
        Priority Priority,
        string? Description);
}
