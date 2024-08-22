using Microsoft.AspNetCore.Mvc;
using NoteApp.Abstractions;
using NoteApp.Exceptions;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        => _userRepository = userRepository;

        [HttpGet]
        public IEnumerable<User> GetUsers() => _userRepository.GetUsers();

        [HttpGet("by_login")]
        public User? GetUserBy(string login)
            =>_userRepository.GetUserBy(user => user.Login == login.Trim());

        [HttpPost]
        public ActionResult Registration(string login, [FromBody] string password)
        {
            _userRepository.Registration(login.Trim(), password.Trim());
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);

            return NoContent();
        }
    }
}
