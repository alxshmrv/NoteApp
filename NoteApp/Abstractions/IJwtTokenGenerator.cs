using NoteApp.Models.DbSet;

namespace NoteApp.Abstractions
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
