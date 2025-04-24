using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using InfraStructure.Data.Mappings;
using Domain.SeedWorks;

namespace InfraStructure.Data.Context
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            _ = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
