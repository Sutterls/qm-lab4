using lab4_quick.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab4_quick.Infrastructure
{
    public partial class QuickMeetingsContext : DbContext
    {
        public QuickMeetingsContext()
        {
        }

        public QuickMeetingsContext(DbContextOptions<QuickMeetingsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<Invitation> Invitation { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("username");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("meetings_pkey");
                entity.ToTable("meetings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("title");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasColumnType("timestamp")
                    .HasColumnName("start_time");

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasColumnType("timestamp")
                    .HasColumnName("end_time");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Meetings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_meetings_user");
            });

            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("invitations_pkey");
                entity.ToTable("invitations");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MeetingId)
                    .IsRequired()
                    .HasColumnName("meeting_id");

                entity.Property(e => e.InviteeEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("invitee_email");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.Invitations) 
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("fk_invitations_meeting");
            });


            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("reminders_pkey");
                entity.ToTable("reminders");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MeetingId) 
                    .IsRequired()
                    .HasColumnName("meeting_id");

                entity.Property(e => e.ReminderTime)
                    .IsRequired()
                    .HasColumnType("timestamp")
                    .HasColumnName("reminder_time");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.Reminders) 
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("fk_reminders_meeting");
            });
        }
    }
}