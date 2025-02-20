using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bookify.Core.Models;
namespace Bookify.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // make compoiste key 
        builder.Entity<BookCategory>().HasKey(e => new { e.BookId, e.CategoryId });

        // Book Copy
        builder.HasSequence<int>("SERIALNUMBER");
        builder.Entity<BookCopy>().
        Property(bc => bc.SerialNumber)
        .HasDefaultValueSql("NEXT VALUE FOR SERIALNUMBER ");

        base.OnModelCreating(builder);
    }
}
