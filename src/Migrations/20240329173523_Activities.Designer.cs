﻿// <auto-generated />
using System;
using ConferenceService.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConferenceService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240329173523_Activities")]
    partial class Activities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConferenceService.DBContext.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("activity")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("activities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            activity = 1,
                            description = "Доклад, 35 - 45 минут"
                        },
                        new
                        {
                            Id = 2,
                            activity = 3,
                            description = "Дискуссия / круглый стол, 40-50 минут"
                        },
                        new
                        {
                            Id = 3,
                            activity = 2,
                            description = "Мастеркласс, 1-2 часа"
                        });
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.Application", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Outline")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("author")
                        .HasColumnType("uuid");

                    b.Property<int>("typeId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("typeId");

                    b.ToTable("applications");
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.SubmittedApplication", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<Guid>("applicationid")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("sumbittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("applicationid");

                    b.ToTable("submittedApplications");
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("currentApplicationid")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("currentApplicationid");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.Application", b =>
                {
                    b.HasOne("ConferenceService.DBContext.Models.Activity", "type")
                        .WithMany()
                        .HasForeignKey("typeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("type");
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.SubmittedApplication", b =>
                {
                    b.HasOne("ConferenceService.DBContext.Models.Application", "application")
                        .WithMany()
                        .HasForeignKey("applicationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("application");
                });

            modelBuilder.Entity("ConferenceService.DBContext.Models.User", b =>
                {
                    b.HasOne("ConferenceService.DBContext.Models.Application", "currentApplication")
                        .WithMany()
                        .HasForeignKey("currentApplicationid");

                    b.Navigation("currentApplication");
                });
#pragma warning restore 612, 618
        }
    }
}
