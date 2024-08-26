namespace NoteApp.Models.DbSet
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        // Ограничения: не больше n символов, не содержит символы, без пробелов внутри, !null

        public string Password { get; set; }
        // Ограничения: не больше n символов, без пробелов внутри,  на английском)), !null

    }
}
