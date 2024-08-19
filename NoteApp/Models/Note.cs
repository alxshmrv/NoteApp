namespace NoteApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Ограничения: не больше n символов, !null
        public string? Description { get; set; }
        // Ограничения: не больше n символов
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsCompleted { get; set; }
        public User Owner { get; set; }
        public Priority Priority { get; set; }
    }
}
