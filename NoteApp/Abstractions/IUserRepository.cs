using NoteApp.Models.DbSet;

namespace NoteApp.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User? GetUserById(int id);
        User? GetUserByLogin(string login);

        string Registration(User user);

        string Login(User user);
        void Logout(int userId);

        void UpdateUser(User user);

        void DeleteUser(int id);
        User TryGetUserByIdAndThrowIfNotFound(int id);
    }
}
