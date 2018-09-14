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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
