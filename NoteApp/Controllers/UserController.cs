using Microsoft.AspNetCore.Mvc;
using NoteApp.Exeptions;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public static readonly List<User> Users = new();
        [HttpGet]
        public List<User> GetUsers() => Users;
        [HttpPost]
        public ActionResult Registration(string login, [FromBody] string password)
        {
            // проверку бы на повторяющийся логин
            Users.Add(new User
            {
                Id = Users.Count,
                Login = login.Trim(),
                Password = password.Trim()
            });
            return Ok();
        }
        public static User TryGetUserAndThrowIfNotFound(int id)
        {
            var user = Users.FirstOrDefault(user => user.Id == id);
            if (user is null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }
    }
}
