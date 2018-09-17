namespace DataWorkShop.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using DataWorkShop.Entities;
    using Microsoft.EntityFrameworkCore;

    public static class BookmarksDbContextExt
    {
        public static void EnsureSeedData(this BookmarksDbContext context)
        {
            // Apply seeding only if tables are empty.
            // Also, we check that there is any migration because seeding can be triggered during "Add-Migration InitialCreate" command.
            if (context.Database.GetAppliedMigrations().Any()
                && !context.Database.GetPendingMigrations().Any()
                && !context.Categories.Any()
                && !context.Bookmarks.Any())
            {
                context.Categories.Add(
                    new Category
                    {
                        Name = "Fun reading",
                        Description = "something that is cool to read",
                        Categories = new List<Category> {
                            new Category {
                                Name = "Jokes",
                                Bookmarks = new List<Bookmark>
                                {
                                   new Bookmark
                                   {
                                       Name = "Reder's digest",
                                       Link = "https://www.rd.com/jokes/"
                                   },
                                   new Bookmark
                                   {
                                       Name = "Laugh factory",
                                       Link = "http://www.laughfactory.com/jokes"
                                   }
                                }
                            },
                            new Category {
                                Name = "Stories"
                            },
                            new Category {
                                Name = "Anekdots",
                                Bookmarks = new List<Bookmark>
                                {
                                   new Bookmark
                                   {
                                       Name = "Анекдоты",
                                       Link = "https://www.anekdot.ru/"
                                   },
                                   new Bookmark
                                   {
                                       Name = "Anekdotov.net",
                                       Link = "http://anekdotov.net/anekdot/"
                                   }
                                }
                            }
                        },
                        Bookmarks = new List<Bookmark>
                        {
                            new Bookmark
                            {
                                Name = "Reading rockets",
                                Description = "Don't know where to place it for a while",
                                Link = "http://www.readingrockets.org/article/25-activities-reading-and-writing-fun"
                            }
                        }
                    });

                context.Categories.Add(
                    new Category
                    {
                        Name = "Great videos",
                        Description = "something to watch on leisure time",
                        Categories = new List<Category> {
                            new Category {
                                Name = "Boom cub compilation"
                            },
                            new Category {
                                Name = "Looper movies facts"
                            },
                            new Category {
                                Name = "Motivation from TOP STORIES"
                            }
                        }
                    });

                context.Categories.Add(
                    new Category
                    {
                        Name = "Education",
                        Description = "Power up your knowledge, <b>dude</b>!",
                        Categories = new List<Category> {
                            new Category {
                                Name = "You Tube videos"
                            },
                            new Category {
                                Name = "Dev Tube videos"
                            },
                            new Category {
                                Name = "Habr articles"
                            }
                        }
                    });

                context.SaveChanges();
            }
        }
    }
}
