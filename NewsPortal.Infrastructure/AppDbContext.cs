using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Models;

namespace NewsPortal.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Article>()
                .Property(a => a.Title)
                .IsRequired();
            modelBuilder.Entity<Article>()
                .HasIndex(a => a.Slug)
                .IsUnique();
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();
        }
    }
}
