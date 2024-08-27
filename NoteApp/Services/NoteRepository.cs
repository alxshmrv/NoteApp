using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using System;
using NoteApp.Models.Vms;


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
            var notes = _notes.Where(n => n.Owner.Id == userId);
            return notes;
        }

        public Note? GetNoteBy(int id, int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            var note = TryGetNoteByIdAndThrowIfNotFound(id);
            return note;
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
            note.Id = newNote.Id;
            note.Name = newNote.Name.Trim();
            note.IsCompleted = newNote.IsCompleted;
            note.Priority = newNote.Priority;
            note.Description = newNote.Description;
            note.LastModifiedDate = newNote.LastModifiedDate;
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
