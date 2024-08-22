using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Controllers;
using NoteApp.Exceptions;
using NoteApp.Abstractions;
namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(INoteRepository noteRepository)
            => _noteRepository = noteRepository;


        [HttpGet("{userId}")]
        public IEnumerable<Note> GetNotes(int userId) => _noteRepository.GetNotes(userId);

        [HttpGet("by_name")]
        public Note? GetNoteBy(string name, int userId)
        {

            return _noteRepository.GetNoteBy(note => note.Name == name.Trim(),
                userId);
        }

        [HttpPost("{userId}")]
        public ActionResult AddNote(
            string name,
            bool isCompleted,
            int userId,
            Priority priority,
            string? descriprion = null)
        {
            _noteRepository.AddNote(name.Trim(), isCompleted, userId, priority, descriprion);
            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateNote(
            int id,
            int userId,
            string name,
            bool isCompleted,
            Priority priority,
            string? descriprion = null)
        {
            _noteRepository.UpdateNote(id, userId, name, isCompleted, priority, descriprion);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteNote(int id, int userId)
        {
            _noteRepository.DeleteNote(id, userId);
            return NoContent();
        }

    }
}

