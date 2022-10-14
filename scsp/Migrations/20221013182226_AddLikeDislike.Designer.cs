﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace scsp.Migrations
{
    [DbContext(typeof(SCSPDataContext))]
    [Migration("20221013182226_AddLikeDislike")]
    partial class AddLikeDislike
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("scsp.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("CommentID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("PostID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("scsp.Models.DislikeComment", b =>
                {
                    b.Property<int>("DislikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CommentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DislikeID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("CommentID");

                    b.ToTable("DislikeComment");
                });

            modelBuilder.Entity("scsp.Models.DislikePost", b =>
                {
                    b.Property<int>("DislikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DislikeID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("PostID");

                    b.ToTable("DislikePost");
                });

            modelBuilder.Entity("scsp.Models.Donation", b =>
                {
                    b.Property<int>("DonationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DonationID");

                    b.HasIndex("UserID");

                    b.ToTable("Donation");
                });

            modelBuilder.Entity("scsp.Models.LikeComment", b =>
                {
                    b.Property<int>("LikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CommentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LikeID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("CommentID");

                    b.ToTable("LikeComment");
                });

            modelBuilder.Entity("scsp.Models.LikePost", b =>
                {
                    b.Property<int>("LikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LikeID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("PostID");

                    b.ToTable("LikePost");
                });

            modelBuilder.Entity("scsp.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FromUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<string>("ToUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MessageID");

                    b.HasIndex("FromUserID");

                    b.HasIndex("ToUserID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("scsp.Models.Photo", b =>
                {
                    b.Property<int>("PhotoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PhotoBLOB")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("PhotoID");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("scsp.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorUserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PhotoID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("PostID");

                    b.HasIndex("AuthorUserID");

                    b.HasIndex("PhotoID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("scsp.Models.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PhotoID")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserID");

                    b.HasIndex("PhotoID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<string>("FollowedByUserID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FollowsUserID")
                        .HasColumnType("TEXT");

                    b.HasKey("FollowedByUserID", "FollowsUserID");

                    b.HasIndex("FollowsUserID");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("scsp.Models.Comment", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("scsp.Models.DislikeComment", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Comment", "Comment")
                        .WithMany("Dislikes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("scsp.Models.DislikePost", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Post", "Post")
                        .WithMany("Dislikes")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("scsp.Models.Donation", b =>
                {
                    b.HasOne("scsp.Models.User", "User")
                        .WithMany("Donations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("scsp.Models.LikeComment", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("scsp.Models.LikePost", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("scsp.Models.Message", b =>
                {
                    b.HasOne("scsp.Models.User", "From")
                        .WithMany("MessagesSent")
                        .HasForeignKey("FromUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.User", "To")
                        .WithMany("MessagesRecieved")
                        .HasForeignKey("ToUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("scsp.Models.Post", b =>
                {
                    b.HasOne("scsp.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.Photo", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoID");

                    b.Navigation("Author");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("scsp.Models.User", b =>
                {
                    b.HasOne("scsp.Models.Photo", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoID");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("scsp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowedByUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("scsp.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowsUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("scsp.Models.Comment", b =>
                {
                    b.Navigation("Dislikes");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("scsp.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Dislikes");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("scsp.Models.User", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("MessagesRecieved");

                    b.Navigation("MessagesSent");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}