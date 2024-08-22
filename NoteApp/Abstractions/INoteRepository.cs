using NoteApp.Models;

namespace NoteApp.Abstractions
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes(int userId);

        Note? GetNoteBy(Predicate<Note> predicate, int userId);

        void AddNote(string name,
            bool isCompleted,
            int userId,
            Priority priority,
            string? descriprion = null);

        void UpdateNote(
            int id,
            int userId,
            string name,
            bool isCompleted,
            Priority priority,
            string? descriprion = null);

        void DeleteNote(int id, int userId);
    }
}
