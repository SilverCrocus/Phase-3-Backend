using Api.Domain.Posts;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Contexts;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post>? Post { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        // Keys
        modelBuilder.Entity<Post>()
            .HasKey(x => x.Id);

        // Columns
        modelBuilder.Entity<Post>()
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);
        
        modelBuilder.Entity<Post>()
            .Property(x => x.Description)
            .HasMaxLength(255);

        modelBuilder.Entity<Post>()
            .Property(x => x.Content)
            .IsRequired();


        // Relationships


    }
}