using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Abstractions;
using NoteApp.Exceptions;
using NoteApp.Models.Contracts;
using NoteApp.Models.DbSet;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ListOfUsers> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(_mapper.Map<ListOfUsers>(users));
        }

        [AllowAnonymous]
        [HttpGet("by_login")]
        public ActionResult GetUserBy(string login)
        {
            var user = _userRepository.GetUserByLogin(login);

            if (user == null)
            {
                return NotFound(login);
            }
            return Ok(_mapper.Map<UserVm>(user));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<string> Registration(CreateUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);

            var token = _userRepository.Registration(newUser);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login(CreateUserDto dto)
        {
            var loginUser = _mapper.Map<User>(dto);
            var token = _userRepository.Login(loginUser);
            return Ok(token);
        }

        [HttpDelete("logout")]
        public IActionResult Logout(int userId)
        {
            _userRepository.Logout(userId);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UpdateUserDto dto)
        {
            var updatedUser = _mapper.Map<User>((id, dto));
            _userRepository.UpdateUser(updatedUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);

            return NoContent();
        }
    }
}
