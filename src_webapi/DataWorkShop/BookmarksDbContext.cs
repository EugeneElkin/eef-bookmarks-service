namespace DataWorkShop
{
    using Microsoft.EntityFrameworkCore;

    public class BookmarksDbContext: DbContext
    {
        public BookmarksDbContext() : base()
        {

        }

        public BookmarksDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

    }
}
