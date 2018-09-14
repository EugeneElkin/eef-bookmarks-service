﻿// <auto-generated />
using DataWorkShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataWorkShop.Migrations
{
    [DbContext(typeof(BookmarksDbContext))]
    partial class BookmarksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataWorkShop.Entities.Bookmark", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Bookmark");
                });

            modelBuilder.Entity("DataWorkShop.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DataWorkShop.Entities.CategoryBookmark", b =>
                {
                    b.Property<string>("CategoryId");

                    b.Property<string>("BookmarkId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("CategoryId", "BookmarkId");

                    b.HasIndex("BookmarkId");

                    b.ToTable("CategoryBookmark");
                });

            modelBuilder.Entity("DataWorkShop.Entities.CategoryCategory", b =>
                {
                    b.Property<string>("CategoryId");

                    b.Property<string>("ChildCategoryId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("CategoryId", "ChildCategoryId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.HasIndex("ChildCategoryId")
                        .IsUnique();

                    b.ToTable("CategoryCategory");
                });

            modelBuilder.Entity("DataWorkShop.Entities.CategoryBookmark", b =>
                {
                    b.HasOne("DataWorkShop.Entities.Bookmark", "Bookmark")
                        .WithMany()
                        .HasForeignKey("BookmarkId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataWorkShop.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataWorkShop.Entities.CategoryCategory", b =>
                {
                    b.HasOne("DataWorkShop.Entities.Category", "Category")
                        .WithOne()
                        .HasForeignKey("DataWorkShop.Entities.CategoryCategory", "CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataWorkShop.Entities.Category", "ChildCategory")
                        .WithOne()
                        .HasForeignKey("DataWorkShop.Entities.CategoryCategory", "ChildCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
