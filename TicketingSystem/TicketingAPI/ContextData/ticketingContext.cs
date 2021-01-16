using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TicketingAPI.DatabaseModels;

#nullable disable

namespace TicketingAPI.ContextData
{
    public partial class ticketingContext : DbContext
    {
        public ticketingContext()
        {
        }

        public ticketingContext(DbContextOptions<ticketingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Organizator> Organizators { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<EventSeats> EventSeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Host=localhost;Database=ticketing;Username=Tadmin;Password=qwerty");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("administrators");

                entity.Property(e => e.AdministratorId).HasColumnName("administrator_id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.OrganizatorId).HasColumnName("organizator_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Organizator)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.OrganizatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_administrator_organizator");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_administrator_user");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.AdministratorId).HasColumnName("administrator_id");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("begin_date");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("finish_date");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizatorId).HasColumnName("organizator_id");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("update_date");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_event_administrator");

                entity.HasOne(d => d.Organizator)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.OrganizatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_event_organizator");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_event_place");
            });

            modelBuilder.Entity<Organizator>(entity =>
            {
                entity.ToTable("organizators");

                entity.Property(e => e.OrganizatorId).HasColumnName("organizator_id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("places");

                entity.HasIndex(e => new { e.Name, e.OrganizatorId }, "places_name_organizator_id_key")
                    .IsUnique();

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizatorId).HasColumnName("organizator_id");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_place_creator");

                entity.HasOne(d => d.Organizator)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.OrganizatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_place_organizator");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seats");

                entity.HasIndex(e => new { e.SectionId, e.SeatRow, e.SeatNumber }, "seats_section_id_seat_row_seat_number_key")
                    .IsUnique();

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.SeatNumber).HasColumnName("seat_number");

                entity.Property(e => e.SeatRow).HasColumnName("seat_row");

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_seat_section");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("sections");

                entity.HasIndex(e => new { e.PlaceId, e.Name }, "sections_place_id_name_key")
                    .IsUnique();

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_section_place");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => new { e.EventId, e.SeatId }, "tickets_event_id_seat_id_key")
                    .IsUnique();

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ticket_event");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ticket_seat");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ticket_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "users_login_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("email");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("insert_date");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<EventSeats>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("EventSeats");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id");

                entity.Property(e => e.SectionId)
                    .HasColumnName("section_id");

                entity.Property(e => e.SectionName)
                    .HasColumnName("section_name");

                entity.Property(e => e.SeatId)
                    .HasColumnName("seat_id");

                entity.Property(e => e.SeatRow)
                    .HasColumnName("seat_row");

                entity.Property(e => e.SeatNumber)
                    .HasColumnName("seat_number");

                entity.Property(e => e.IsSeatFree)
                    .HasColumnName("is_seat_free");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
