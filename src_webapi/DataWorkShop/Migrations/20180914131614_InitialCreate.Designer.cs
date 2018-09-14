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
    [Migration("20180914131614_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataWorkShop.Entities.Bookmark", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("DataWorkShop.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataWorkShop.Entities.Bookmark", b =>
                {
                    b.HasOne("DataWorkShop.Entities.Category", "Category")
                        .WithMany("Bookmarks")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("DataWorkShop.Entities.Category", b =>
                {
                    b.HasOne("DataWorkShop.Entities.Category", "Parent")
                        .WithMany("Categories")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
