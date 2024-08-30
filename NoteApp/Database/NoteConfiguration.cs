using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Models.DbSet;
using System.Transactions;

namespace NoteApp.Database
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.Id);

            builder.Property(note => note.Name).HasMaxLength(256);

            builder.Property(note => note.Description).HasMaxLength(2560);

        }
    }
}
