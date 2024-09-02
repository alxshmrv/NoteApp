namespace NoteApp.Models.DbSet
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
        

    }
}
