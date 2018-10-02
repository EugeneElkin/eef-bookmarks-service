namespace BookmarksAPI.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using BookmarksAPI.Services;
    using DataWorkShop;
    using DataWorkShop.Entities;
    using DataWorkShop.Entities.Structures;
    using Microsoft.EntityFrameworkCore;
    using System;

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
                byte[] passwordHash, passwordSalt;
                UserService.CreatePasswordHash("1234", out passwordHash, out passwordSalt);
                var userId = Guid.NewGuid().ToString();

                context.Users.Add(
                    new User
                    {
                        Id = userId,
                        UserName = "bober@bober.ru",
                        Status = UserStatusType.Active,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    });
                context.SaveChanges();

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
                                       Link = "https://www.rd.com/jokes/",
                                       UserId = userId
                                   },
                                   new Bookmark
                                   {
                                       Name = "Laugh factory",
                                       Link = "http://www.laughfactory.com/jokes",
                                       UserId = userId
                                   }
                                },
                                UserId = userId,
                                Categories = new List<Category> {
                                    new Category {
                                        Name = "Underground",
                                        UserId = userId,
                                        Bookmarks = new List<Bookmark>
                                        {
                                            new Bookmark
                                            {
                                                Name = "Reder's digest",
                                                Link = "https://www.rd.com/jokes/",
                                                UserId = userId
                                            },
                                            new Bookmark
                                            {
                                                Name = "Laugh factory",
                                                Link = "http://www.laughfactory.com/jokes",
                                                UserId = userId
                                            }
                                        },
                                    }
                                }
                            },
                            new Category {
                                Name = "Stories",
                                UserId = userId
                            },
                            new Category {
                                Name = "Anekdots",
                                Bookmarks = new List<Bookmark>
                                {
                                   new Bookmark
                                   {
                                       Name = "Анекдоты",
                                       Link = "https://www.anekdot.ru/",
                                       UserId = userId
                                   },
                                   new Bookmark
                                   {
                                       Name = "Anekdotov.net",
                                       Link = "http://anekdotov.net/anekdot/",
                                       UserId = userId
                                   }
                                },
                                UserId = userId
                            }
                        },
                        Bookmarks = new List<Bookmark>
                        {
                            new Bookmark
                            {
                                Name = "Reading rockets",
                                Description = "Don't know where to place it for a while",
                                Link = "http://www.readingrockets.org/article/25-activities-reading-and-writing-fun",
                                UserId = userId
                            }
                        },
                        UserId = userId
                    });

                context.Categories.Add(
                    new Category
                    {
                        Name = "Great videos",
                        Description = "something to watch on leisure time",
                        Categories = new List<Category> {
                            new Category {
                                Name = "Boom cub compilation",
                                UserId = userId
                            },
                            new Category {
                                Name = "Looper movies facts",
                                UserId = userId
                            },
                            new Category {
                                Name = "Motivation from TOP STORIES",
                                UserId = userId
                            }
                        },
                        UserId = userId
                    });

                context.Categories.Add(
                    new Category
                    {
                        Name = "Education",
                        Description = "Power up your knowledge, <b>dude</b>!",
                        Categories = new List<Category> {
                            new Category {
                                Name = "You Tube videos",
                                UserId = userId
                            },
                            new Category {
                                Name = "Dev Tube videos",
                                UserId = userId
                            },
                            new Category {
                                Name = "Habr articles",
                                UserId = userId
                            }
                        },
                        UserId = userId
                    });

                context.SaveChanges();
            }
        }
    }
}
