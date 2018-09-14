namespace DataWorkShop
{
    using DataWorkShop.Entities;
    using Microsoft.EntityFrameworkCore;

    public class BookmarksDbContext : DbContext
    {
        public BookmarksDbContext() : base()
        {

        }

        public BookmarksDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<CategoryCategory> CategoryCategories { get; set; }
        public virtual DbSet<CategoryBookmark> CategoryBookmarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CategoryBookmark>()
                .HasKey(e => new { e.CategoryId, e.BookmarkId });
            modelBuilder
                .Entity<CategoryBookmark>()
                .HasOne<Category>(e => e.Category)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder
                .Entity<CategoryBookmark>()
                .HasOne<Bookmark>(e => e.Bookmark)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder
                .Entity<CategoryCategory>()
                .HasKey(e => new { e.CategoryId, e.ChildCategoryId });
            modelBuilder
                .Entity<CategoryCategory>()
                .HasOne<Category>(e => e.Category)
                .WithOne().Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder
                .Entity<CategoryCategory>()
                .HasOne<Category>(e => e.ChildCategory)
                .WithOne().Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }

    }
}
