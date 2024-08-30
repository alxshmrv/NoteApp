using NoteApp.Models.DbSet;

namespace NoteApp.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User? GetUserById(int id);
        User? GetUserByLogin(string login);

        int Registration(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);
        User TryGetUserByIdAndThrowIfNotFound(int id);
    }
}
