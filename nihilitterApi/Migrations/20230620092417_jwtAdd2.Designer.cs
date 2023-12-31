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
    [Migration("20230620092417_jwtAdd2")]
    partial class jwtAdd2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NihilitterApi.Models.Message", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("FromId")
                        .HasColumnType("bigint");

                    b.Property<string>("MessageBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ToId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("NihilitterApi.Models.Nihil", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("NihilitterApi.Models.Message", b =>
                {
                    b.HasOne("NihilitterApi.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("FromId");

                    b.HasOne("NihilitterApi.Models.User", "To")
                        .WithMany()
                        .HasForeignKey("ToId");

                    b.HasOne("NihilitterApi.Models.User", null)
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("NihilitterApi.Models.User", b =>
                {
                    b.HasOne("NihilitterApi.Models.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NihilitterApi.Models.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
