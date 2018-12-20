﻿// <auto-generated />
using DiscordBotVS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiscordBotVS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181216205850_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("Features.lib.Levels.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CurrentLevel");

                    b.Property<double>("CurrentXp");

                    b.Property<double>("MaxLevel");

                    b.Property<int>("UserId");

                    b.Property<double>("XpMax");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Features.lib.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("DiscordId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Features.lib.Levels.Level", b =>
                {
                    b.HasOne("Features.lib.Users.User", "User")
                        .WithOne("UserLevel")
                        .HasForeignKey("Features.lib.Levels.Level", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
