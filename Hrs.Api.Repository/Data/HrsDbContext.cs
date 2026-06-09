using Microsoft.EntityFrameworkCore;

namespace Hrs.Api.Repository.Data;

public class HrsDbContext : DbContext 
{
    public HrsDbContext(DbContextOptions<HrsDbContext> options)
        : base(options)
    {
    }
    //public DbSet<Author> Author { get; set; }
}
