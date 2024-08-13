using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RjScrubs.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RjScrubs.Data
{
    // The ApplicationDbContext class is used to interact with the database using Entity Framework Core
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }

        // Configuring the model relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customizing the ASP.NET Identity model
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                // Custom properties or configurations for ApplicationUser can be added here
            });

            // Customizing the Service entity
            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                // Additional configurations
            });

            // Customizing the Booking entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingDate).IsRequired();
               
            });

            // Customizing the Payment entity
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                // Additional configurations
            });

            // Customizing the Notification entity
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Body).HasMaxLength(1000);
                // Additional configurations
            });

            // Customizing the Report entity
            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.ReportType).HasMaxLength(100);
                // Additional configurations
            });

            // Add other custom configurations if needed
        }
    }
}
