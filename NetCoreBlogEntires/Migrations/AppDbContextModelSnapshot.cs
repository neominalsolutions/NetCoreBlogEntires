﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreBlogEntires.Data.Contexts;

namespace NetCoreBlogEntires.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<string>("PostsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TagsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Comment", b =>
                {
                    b.HasOne("NetCoreBlogEntires.Data.Models.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Post", b =>
                {
                    b.HasOne("NetCoreBlogEntires.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("NetCoreBlogEntires.Data.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetCoreBlogEntires.Data.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetCoreBlogEntires.Data.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
