using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;

namespace NoteApp.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ITimeProvider _timeProvider;

        private readonly List<User> _users = new();

        public UserRepository(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public IEnumerable<User> GetUsers() => _users;

        public User? GetUserBy(Predicate<User> predicate)
        => _users.FirstOrDefault(user => predicate(user));

        public void Registration(string login, string password) =>
            _users.Add(new User
            {
                Id = _users.Count,
                Login = login,
                Password = password
            });

        public void UpdateUser(int id, string login, string password)
        {
            var user = TryGetUserByIdAndThrowIfNotFound(id);
            user.Login = login;
            user.Password = password;
        }

        public void DeleteUser(int id)
        {
            var user = TryGetUserByIdAndThrowIfNotFound(id);
            _users.Remove(user);
        }

        public User TryGetUserByIdAndThrowIfNotFound(int id)
        {
            var user = _users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }
    }
}
