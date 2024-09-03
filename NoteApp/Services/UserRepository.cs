using NoteApp.Exceptions;
using NoteApp.Abstractions;
using NoteApp.Models.DbSet;
using NoteApp.Database;

namespace NoteApp.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IJwtTokensRepository _jwtTokensRepository;

        private readonly NoteAppDbContext _dbContext;

        public UserRepository(NoteAppDbContext dbContext, IJwtTokensRepository jwtTokensRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _jwtTokensRepository = jwtTokensRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public IEnumerable<User> GetUsers() => _dbContext.Users;

        public User? GetUserById(int id)
             => _dbContext.Users.FirstOrDefault(user => user.Id == id);

        public User? GetUserByLogin(string login)
            => _dbContext.Users.FirstOrDefault(user => user.Login == login.Trim());

        public string Registration(User user)
        {
            _dbContext.Users.Add(user);

            _dbContext.SaveChanges();

            var token = _jwtTokenGenerator.GenerateToken(user);
            _jwtTokensRepository.Update(user.Id, token);

            return token;
        }

        public string Login(User user)
        {
            var logUser = _dbContext.Users.FirstOrDefault(u => u.Login == user.Login);
            if (logUser != null)
            {
                throw new ArgumentException(nameof(user));
            }
            if (logUser.Password != user.Password)
            {
                throw new ArgumentException(nameof(logUser));
            }
            var token = _jwtTokenGenerator.GenerateToken(logUser);
            _jwtTokensRepository.Update(logUser.Id, token);

            return token;
        }

        public void Logout(int userId)
        {
            _jwtTokensRepository.Remove(userId);
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
