using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostsApi.Core.Domain;
using PostsApi.Infrastructure.Extensions;

namespace PostsApi.Infrastructure.Contexts;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
       
        modelBuilder.ApplyEntityTypes(typeof(AppDbContext).Assembly, typeof(IEntityTypeConfiguration<>));
    }
}
