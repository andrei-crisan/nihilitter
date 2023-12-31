﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NihilitterApi.Models;

#nullable disable

namespace nihilitterApi.Migrations
{
    [DbContext(typeof(NihilContext))]
    [Migration("20230621101839_friendship2")]
    partial class friendship2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NihilitterApi.Models.Friend", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("FriendId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("NihilitterApi.Models.Message", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("FromId")
                        .HasColumnType("bigint");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ToId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ToId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.HasIndex("ToId1");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("NihilitterApi.Models.Nihil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("NihilItems");
                });

            modelBuilder.Entity("NihilitterApi.Models.User", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("UserFriend", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("FriendId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("UserFriend");
                });

            modelBuilder.Entity("NihilitterApi.Models.Message", b =>
                {
                    b.HasOne("NihilitterApi.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("NihilitterApi.Models.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NihilitterApi.Models.User", "To")
                        .WithMany()
                        .HasForeignKey("ToId1");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("NihilitterApi.Models.Nihil", b =>
                {
                    b.HasOne("NihilitterApi.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserFriend", b =>
                {
                    b.HasOne("NihilitterApi.Models.Friend", null)
                        .WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NihilitterApi.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NihilitterApi.Models.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
