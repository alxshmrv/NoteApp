using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using System;
using NoteApp.Models.Contracts;
using NoteApp.Database;


namespace NoteApp.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly NoteAppDbContext _dbContext;

        public NoteRepository(IUserRepository userRepository, NoteAppDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public IEnumerable<Note> GetNotes(int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            var notes = _dbContext.Notes.Where(n => n.Owner.Id == userId);
            return notes;
        }

        public Note? GetNoteBy(int id, int userId)
        {
            _ = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            var note = TryGetNoteByIdAndThrowIfNotFound(id);
            return note;
        }

        public int AddNote(int userId,
            Note note)
        {
            var owner = _userRepository.TryGetUserByIdAndThrowIfNotFound(userId);
            note.Owner = owner;
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
            return note.Id;

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
            _dbContext.Notes.Remove(note);
        }

        private Note TryGetNoteByIdAndThrowIfNotFound(int id)
        {
            var note = _dbContext.Notes.FirstOrDefault(note => note.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException(id);
            }
            return note;
        }

    }
}
