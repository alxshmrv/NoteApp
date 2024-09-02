using NoteApp.Abstractions;
using System.Collections.Concurrent;

namespace NoteApp.Services
{
    public class JwtTokensRepository : IJwtTokensRepository
    {
        private readonly ConcurrentDictionary<int, string> _tokens = new();

        public void Update(int userId, string token) => _tokens[userId] = token;

        public bool Verify(int userId, string token) =>
            _tokens.ContainsKey(userId) && _tokens[userId] == token;

        public void Remove(int userId) => _tokens.Remove(userId, out _);
    }
}
