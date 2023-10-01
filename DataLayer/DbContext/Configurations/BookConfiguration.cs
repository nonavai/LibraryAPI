using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.DbContext.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> modelBuilder)
    {
        modelBuilder
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<BookGenres>(
                j => j.HasOne(bg => bg.Genre).WithMany(),
                j => j.HasOne(bg => bg.Book).WithMany()
            );
        modelBuilder
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<BookAuthors>(
                j => j.HasOne(bg => bg.Author).WithMany(),
                j => j.HasOne(bg => bg.Book).WithMany()
            );


    }
}