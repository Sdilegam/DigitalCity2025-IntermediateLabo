﻿// <auto-generated />
using System;
using IntermediateLab_Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntermediateLab_Backend.Infrastructure.Migrations
{
    [DbContext(typeof(LaboContext))]
    [Migration("20250327143240_Adding tournament seed")]
    partial class Addingtournamentseed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntermediateLab_Backend.Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Elo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("Salt")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Salt")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Members", t =>
                        {
                            t.HasCheckConstraint("CK_MEMBER_ELO", "Elo BETWEEN 0 AND 3000");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1999, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Elo = 1500,
                            Email = "test@gmail.com",
                            Gender = 0,
                            Password = "��O)�a��=o�bz�\n٠į8����P����\n'�:�Q-n��h��p�Ɋ����>A��",
                            Role = 0,
                            Salt = new Guid("00000000-0000-0000-0000-000000000000"),
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("IntermediateLab_Backend.Domain.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Categories")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrRound")
                        .HasColumnType("int");

                    b.Property<DateTime>("InscriptionsEndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWomenOnly")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LatestUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("MaxPlayerAmount")
                        .HasColumnType("int");

                    b.Property<int?>("MaxPlayerElo")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayerAmount")
                        .HasColumnType("int");

                    b.Property<int?>("MinPlayerElo")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tournaments", t =>
                        {
                            t.HasCheckConstraint("CK_TOURNAMENT_END_DATE", "InscriptionsEndDate >= CreationDate");

                            t.HasCheckConstraint("CK_TOURNAMENT_MAX_ELO", "MaxPlayerElo BETWEEN 0 AND 3000");

                            t.HasCheckConstraint("CK_TOURNAMENT_MAX_PLAYERS", "MaxPlayerAmount BETWEEN 2 AND 32");

                            t.HasCheckConstraint("CK_TOURNAMENT_MIN_ELO", "MinPlayerElo BETWEEN 0 AND MaxPlayerElo");

                            t.HasCheckConstraint("CK_TOURNAMENT_MIN_PLAYERS", "MinPlayerAmount BETWEEN 2 AND MaxPlayerAmount");
                        });

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Categories = 1,
                            CreationDate = new DateTime(2025, 3, 27, 15, 32, 39, 351, DateTimeKind.Local).AddTicks(6132),
                            CurrRound = 0,
                            InscriptionsEndDate = new DateTime(2025, 4, 21, 15, 32, 39, 355, DateTimeKind.Local).AddTicks(2161),
                            IsWomenOnly = false,
                            LatestUpdate = new DateTime(2025, 3, 27, 15, 32, 39, 355, DateTimeKind.Local).AddTicks(1499),
                            Location = "Bruxelles",
                            MaxPlayerAmount = 10,
                            MaxPlayerElo = 3000,
                            MinPlayerAmount = 2,
                            MinPlayerElo = 100,
                            Name = "Tournament From Seed",
                            Status = 0
                        });
                });

            modelBuilder.Entity("MemberTournament", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("int");

                    b.HasKey("PlayersId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("MemberTournament");
                });

            modelBuilder.Entity("MemberTournament", b =>
                {
                    b.HasOne("IntermediateLab_Backend.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntermediateLab_Backend.Domain.Entities.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
