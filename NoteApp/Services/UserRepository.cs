using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using NoteApp.Database;

namespace NoteApp.Services
{
    public class UserRepository : IUserRepository
    {

        private readonly NoteAppDbContext _dbContext;

        public UserRepository(NoteAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers() => _dbContext.Users;

        public User? GetUserById(int id)
             => _dbContext.Users.FirstOrDefault(user => user.Id == id);

        public User? GetUserByLogin(string login)
            => _dbContext.Users.FirstOrDefault(user => user.Login == login.Trim());

        public int Registration(User user)
        {
            _dbContext.Users.Add(user);

            _dbContext.SaveChanges();

            return user.Id;
        }

        public void UpdateUser(User user)
        {
            var oldUser = TryGetUserByIdAndThrowIfNotFound(user.Id);
            oldUser.Login = user.Login;
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = TryGetUserByIdAndThrowIfNotFound(id);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User TryGetUserByIdAndThrowIfNotFound(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            return user;
        }
    }
}
