using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.DbContext.Configurations;

public class BookLoanConfiguration : IEntityTypeConfiguration<BookLoan>
{
    public void Configure(EntityTypeBuilder<BookLoan> modelBuilder)
    {
        modelBuilder
            .HasOne(bl => bl.User)
            .WithMany(u => u.BookLoans)
            .HasForeignKey(bl => bl.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(bl => bl.Book)
            .WithMany(b => b.BookLoans)
            .HasForeignKey(bl => bl.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}