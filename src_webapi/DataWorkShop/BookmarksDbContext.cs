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
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If category was deleted all child bookmarks will be deleted cascade
            modelBuilder.Entity<Category>()
                .HasMany<Bookmark>(b => b.Bookmarks)
                .WithOne(c => c.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // It is impossible to do the same for child categories because migration system complains about cyclic relation
            // In that case cascade deletion of child categories must be done manually
        }

    }
}
