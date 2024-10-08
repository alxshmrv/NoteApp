﻿namespace NoteApp.Models.DbSet
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsCompleted { get; set; }
        public User Owner { get; set; } = default!;
        public Priority Priority { get; set; }
    }
}
