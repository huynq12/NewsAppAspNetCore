using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsApp.Models;
using NewsApp.ViewModels;

namespace NewsApp.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
            //khai bao key cho bang trung gian
            builder.Entity<PostCategory>()
                .HasKey(pc => new { pc.PostId, pc.CategoryId });

            builder.Entity<PostCategory>()
                .HasOne(p => p.Post)
                .WithMany(pc => pc.PostCategories)
                .HasForeignKey(c => c.PostId);

            builder.Entity<PostCategory>()
                .HasOne(c=>c.Category)
                .WithMany(pc=>pc.PostCategories)
                .HasForeignKey(p=> p.CategoryId);   
            base.OnModelCreating(builder);
		}

		public DbSet<NewsApp.ViewModels.CategoryDto> CategoryDto { get; set; } = default!;

		public DbSet<NewsApp.ViewModels.CommentDto> CommentDto { get; set; } = default!;
	}
}
