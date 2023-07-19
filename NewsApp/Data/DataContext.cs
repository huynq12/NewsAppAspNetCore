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
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<ImageCategory> ImageCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
            //post - category
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

            //image - imagecategory
            builder.Entity<CategoryImages>()
                .HasKey(pc => new { pc.ImageId, pc.CatImageId });

            builder.Entity<CategoryImages>()
                .HasOne(p => p.Image)
                .WithMany(pc => pc.CategoryImages)
                .HasForeignKey(c => c.ImageId);

            builder.Entity<CategoryImages>()
                .HasOne(c => c.Category)
                .WithMany(pc => pc.CategoryImages)
                .HasForeignKey(p => p.CatImageId);


            base.OnModelCreating(builder);
		}

		
	}
}
