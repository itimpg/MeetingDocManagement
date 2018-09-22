using MeetingDoc.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingDoc.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<MeetingTopic> MeetingTopics { get; set; }
        public DbSet<MeetingTime> MeetingTimes { get; set; }
        public DbSet<MeetingAgenda> MeetingAgendas { get; set; }
        public DbSet<MeetingContent> MeetingContents { get; set; }
        public DbSet<MeetingNote> MeetingNotes { get; set; }
        public DbSet<MeetingAgendaUser> MeetingAgendaUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MeetingContent>().Property(x => x.FileBase64)
                .HasColumnType("MediumBlob");
        }
    }
}