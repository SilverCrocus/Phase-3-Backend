using Api.Domain.Post;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Contexts;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post>? Post { get; set; }
}