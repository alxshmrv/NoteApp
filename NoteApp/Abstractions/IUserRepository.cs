using NoteApp.Models;

namespace NoteApp.Abstractions
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User? GetUserBy(Predicate<User> predicate);

        void Registration(string login, string password);

        void UpdateUser(int id, string login, string password);

        void DeleteUser(int id);
        User TryGetUserByIdAndThrowIfNotFound(int id);
    }
}
