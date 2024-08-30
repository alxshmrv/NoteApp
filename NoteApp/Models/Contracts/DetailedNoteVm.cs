namespace NoteApp.Models.Contracts
{
    public record DetailedNoteVm(
        string Name,
        string Description,
        bool IsCompleted);
}
