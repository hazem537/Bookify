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
    public  DbSet<Category> Categories { get;set;}
    // protected override void OnModelCreating(ModelBuilder builder){

    //     base.OnModelCreating(builder);
    // }
}
