using NoteApp.Models;
using NoteApp.Exceptions;
using NoteApp.Abstractions;


namespace NoteApp.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly IUserRepository _userRepository;
        public NoteRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private readonly List<Note> _notes = new();
        public IEnumerable<Note> GetNotes(int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            return _notes;
        }

        public Note? GetNoteBy(Predicate<Note> predicate, int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            return _notes.FirstOrDefault(note => predicate(note));
        }

        public void AddNote(
            string name,
            bool isCompleted,
            int userId,
            Priority priority,
            string? descriprion = null)
        {
            var owner = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            _notes.Add(new Note
            {
                Id = _notes.Count,
                Name = name.Trim(),
                IsCompleted = isCompleted,
                Priority = priority,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Owner = owner,
                Description = descriprion
            });

        }

        public void UpdateNote(
            int id,
            int userId,
            string name,
            bool isCompleted,
            Priority priority,
            string? descriprion = null)
        {
            // Нет проверки на то, что юзер именно свою заметку редачит?
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            var note = TryGetNoteByIdAndThrowIfNotFound(id);
            note.Id = id;
            note.Name = name;
            note.IsCompleted = isCompleted;
            note.Priority = priority;
            note.Description = descriprion;
        }

        public void DeleteNote(int id, int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            var note = TryGetNoteByIdAndThrowIfNotFound(id);
            _notes.Remove(note);
        }

        private Note TryGetNoteByIdAndThrowIfNotFound(int id)
        {
            var note = _notes.FirstOrDefault(note => note.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException(id);
            }
            return note;
        }

    }
}
