using Microsoft.AspNetCore.Mvc;
using NoteApp.Controllers;
using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using AutoMapper;
using NoteApp.Models.Vms;
using NoteApp.Models.Dtos;
using NoteApp.Services;
namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly ITimeProvider _timeProvider;
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteController(INoteRepository noteRepository, IMapper mapper, ITimeProvider timeProvider)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _timeProvider = timeProvider;
        }


        [HttpGet("{userId}")]
        public ActionResult<NoteListVm> GetNotes(int userId)
        {
            var userNotes = _noteRepository.GetNotes(userId);
            var mappedUserNotes = _mapper.Map<NoteListVm>(userNotes);
            return Ok(mappedUserNotes);

        }

        [HttpGet("by_id")]
        public ActionResult<DetailedNoteVm> GetNoteBy(int id, int userId)
        {
            var userNote = _noteRepository.GetNoteBy(id, userId);
            var mappedUserNote = _mapper.Map<DetailedNoteVm>(userNote);
            return Ok(mappedUserNote);
        }

        [HttpPost("{userId}")]
        public ActionResult AddNote(int userId, CreateNoteDto createNoteDto)
        {
            var note = _mapper.Map<Note>(createNoteDto);
            _noteRepository.AddNote(userId, note);
            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateNote(
            int id,
            int userId,
            EditNoteDto editNoteDto)
        {
            var note = _mapper.Map<Note>(editNoteDto);
            _noteRepository.UpdateNote(id, userId, note);
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

