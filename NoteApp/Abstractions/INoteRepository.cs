using NoteApp.Models.DbSet;
using NoteApp.Models.Vms;

namespace NoteApp.Abstractions
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetNotes(int userId);

        Note? GetNoteBy(int id, int userId);

        void AddNote(int userId,
            Note note);

        void UpdateNote(
            int id,
            int userId,
            Note newNote);

        void DeleteNote(int id, int userId);
    }
}
