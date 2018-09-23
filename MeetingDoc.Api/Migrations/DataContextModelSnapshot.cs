﻿// <auto-generated />
using System;
using MeetingDoc.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeetingDoc.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDraft");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("MeetingTimeId");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("MeetingTimeId");

                    b.ToTable("MeetingAgendas");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgendaUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("MeetingAgendaId");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MeetingAgendaId");

                    b.HasIndex("UserId");

                    b.ToTable("MeetingAgendaUsers");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<byte[]>("FileBase64")
                        .HasColumnType("MediumBlob");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("MeetingAgendaId");

                    b.Property<int>("Ordinal");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("MeetingAgendaId");

                    b.ToTable("MeetingContents");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("MeetingContentId");

                    b.Property<byte[]>("Note")
                        .HasColumnType("MediumBlob");

                    b.Property<string>("NoteHeader");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MeetingContentId");

                    b.HasIndex("UserId");

                    b.ToTable("MeetingNotes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FiscalYear");

                    b.Property<bool>("IsDraft");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Location");

                    b.Property<DateTime>("MeetingDate");

                    b.Property<int>("MeetingTopicId");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("MeetingTopicId");

                    b.ToTable("MeetingTimes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDraft");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("MeetingTypeId");

                    b.Property<string>("Name");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("MeetingTypeId");

                    b.ToTable("MeetingTopics");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDraft");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Name");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("MeetingTypes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("LastName");

                    b.Property<int>("Level");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("Position");

                    b.Property<int>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgenda", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingTime", "MeetingTime")
                        .WithMany()
                        .HasForeignKey("MeetingTimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgendaUser", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingAgenda", "MeetingAgenda")
                        .WithMany("MeetingAgendaUsers")
                        .HasForeignKey("MeetingAgendaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MeetingDoc.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingContent", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingAgenda", "MeetingAgenda")
                        .WithMany()
                        .HasForeignKey("MeetingAgendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingNote", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingContent", "MeetingContent")
                        .WithMany()
                        .HasForeignKey("MeetingContentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MeetingDoc.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTime", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingTopic", "MeetingTopic")
                        .WithMany()
                        .HasForeignKey("MeetingTopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTopic", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingType", "MeetingType")
                        .WithMany()
                        .HasForeignKey("MeetingTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
