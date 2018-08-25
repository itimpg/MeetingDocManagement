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
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<int?>("TimeId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("TimeId");

                    b.ToTable("MeetingAgendas");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AgendaId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<byte[]>("File");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsRemoved");

                    b.Property<int>("Ordinal");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("AgendaId");

                    b.ToTable("MeetingContents");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContentId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Note");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("MeetingNotes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FiscalYear");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Location");

                    b.Property<DateTime>("MeetingDate");

                    b.Property<int?>("TopicId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("MeetingTimes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Name");

                    b.Property<int?>("TypeId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("MeetingTopics");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsRemoved");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("MeetingTypes");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

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

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingAgenda", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingTime", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingContent", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingAgenda", "Agenda")
                        .WithMany()
                        .HasForeignKey("AgendaId");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingNote", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId");

                    b.HasOne("MeetingDoc.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTime", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingTopic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("MeetingDoc.Api.Models.MeetingTopic", b =>
                {
                    b.HasOne("MeetingDoc.Api.Models.MeetingType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
