using Microsoft.EntityFrameworkCore;
using NoteApp.Models.DbSet;
using System.Reflection;
namespace NoteApp.Database
{
    public class NoteAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
