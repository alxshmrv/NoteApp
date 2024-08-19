using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Controllers;
using NoteApp.Exceptions;
namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        public static readonly List<Note> Notes = new();
        [HttpGet]
        public List<Note> GetNotes() => Notes;
        [HttpPost]
        public ActionResult AddNote(
            string name,
            bool isCompleted,
            int userId,
            Priority priority,
            string? descriprion = null)
        {
            var owner = UserController.TryGetUserAndThrowIfNotFound(userId);
            Notes.Add(new Note
            {
                Id = Notes.Count,
                Name = name.Trim(),
                IsCompleted = isCompleted,
                Priority = priority,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Owner = owner,
                Description = descriprion
            });
            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateNote(
            int id,
            string name,
            bool isCompleted,
            Priority priority,
            string? descriprion = null)
        {
            var noteForUpdate = TryGetNoteAndThrowIfNotFound(id);
            noteForUpdate.Name = name.Trim();
            noteForUpdate.IsCompleted = isCompleted;
            noteForUpdate.Priority = priority;
            noteForUpdate.Description = descriprion;
            noteForUpdate.LastModifiedDate = DateTime.Now;
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteNote(int id)
        {
            var noteForDelete = TryGetNoteAndThrowIfNotFound(id);
            Notes.Remove(noteForDelete);
            return NoContent();
        }
        public static Note TryGetNoteAndThrowIfNotFound(int id)
        {
            var note = Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                throw new NoteNotFoundException(id);
            }
            return note;
        }
    }
}
