using Microsoft.AspNetCore.Mvc;
using NoteApp.Controllers;
using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using AutoMapper;
using NoteApp.Services;
using NoteApp.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using NoteApp.Services.Extentions;
namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteController(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }


        [HttpGet("list_of_notes")]
        [Authorize(Policy = "NotesOwner")]
        public ActionResult<NoteListVm> GetNotes(int userId)
        {
            var userNotes = _noteRepository.GetNotes(userId);
            var mappedUserNotes = _mapper.Map<NoteListVm>(userNotes);
            return Ok(mappedUserNotes);

        }

        [HttpGet("by_id")]
        [Authorize(Policy = "NotesOwner")]
        public ActionResult<DetailedNoteVm> GetNoteBy(int id, int userId)
        {
            var userNote = _noteRepository.GetNoteBy(id, userId);
            var mappedUserNote = _mapper.Map<DetailedNoteVm>(userNote);
            return Ok(mappedUserNote);
        }

        [HttpPost("add_note")]
        public ActionResult<int> AddNote(CreateNoteDto createNoteDto)
        {
            var userId = HttpContext.ExtractUserIdFromClaims()!.Value;
            var note = _mapper.Map<Note>(createNoteDto);    
            
            var noteId = _noteRepository.AddNote(userId, note);

            return Ok(noteId);
        }

        [HttpPut("change_note")]
        [Authorize(Policy = "NotesOwner")]
        public ActionResult UpdateNote(
            int id,
            int userId,
            EditNoteDto editNoteDto)
        {
            var note = _mapper.Map<Note>(editNoteDto);
            _noteRepository.UpdateNote(id, userId, note);
            return Ok();
        }

        [HttpDelete("delete_note")]
        [Authorize(Policy = "NotesOwner")]
        public ActionResult DeleteNote(int id, int userId)
        {
            _noteRepository.DeleteNote(id, userId);
            return Ok();
        }

    }
}

