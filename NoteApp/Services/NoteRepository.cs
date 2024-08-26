using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using System;


namespace NoteApp.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IUserRepository _userRepository;
        private readonly List<Note> _notes = new();

        public NoteRepository(IUserRepository userRepository, ITimeProvider timeProvider)
        {
            _userRepository = userRepository;
            _timeProvider = timeProvider;
        }
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

        public void AddNote(int userId,
            Note note)
        {
            var owner = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            _notes.Add(new Note
            {
                Id = _notes.Count,
                Name = note.Name,
                IsCompleted = note.IsCompleted,
                Priority = note.Priority,
                CreationDate = note.CreationDate,
                LastModifiedDate = note.CreationDate,
                Owner = owner,
                Description = note.Description
            });

        }

        public void UpdateNote(
            int id,
            int userId,
            Note newNote)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
           var note = TryGetNoteByIdAndThrowIfNotFound(id);
            //note.Id = id;
            //note.Name = note.Name;
           // note.IsCompleted = isCompleted;
            //note.Priority = priority;
           // note.Description = descriprion;
           note.LastModifiedDate = note.CreationDate;
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
