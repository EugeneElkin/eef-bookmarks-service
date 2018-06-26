namespace DataWorkShop.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;

    public static class BookmarksDbContextExt
    {
        public static void EnsureSeedData(this BookmarksDbContext context)
        {
            // Apply seeding only if tables are empty.
            // Also, we check that there is any migration because seeding can be triggered during "Add-Migration InitialCreate" command.
            if (context.Database.GetAppliedMigrations().Any()
                && !context.Database.GetPendingMigrations().Any())
            {
            }
        }
    }
}
