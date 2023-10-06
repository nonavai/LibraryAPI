using DataAccess.Entities;
using DataLayer.DbContext.Configurations;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.DbContext;

public class LibraryContext : Microsoft.EntityFrameworkCore.DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookLoan> BookLoans { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<BookGenres> BookGenres { get; set; }
    public DbSet<BookAuthors> BookAuthors { get; set; }
    

    private string DbPath;

    public LibraryContext()
    { 
        
    }
    public LibraryContext(DbContextOptions<LibraryContext> options, IConfiguration configuration) : base(options)
    {
        DbPath = configuration.GetConnectionString("DataBase");
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookLoanConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookLoanConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies();
        options.UseSqlServer(DbPath);
    }
}