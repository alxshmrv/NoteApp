using NoteApp.Models.DbSet;

namespace NoteApp.Abstractions
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes(int userId);

        Note? GetNoteBy(Predicate<Note> predicate, int userId);

        void AddNote(int userId,
            Note note);

        void UpdateNote(
            int id,
            int userId,
            Note newNote);

        void DeleteNote(int id, int userId);
    }
}
