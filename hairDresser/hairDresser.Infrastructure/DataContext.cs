﻿using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hairDresser.Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        // Explicit empty constructor needed to create objects when making migrations
        public DataContext() { }

        // Required to allow EFC to inject the configuration
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<HairService> HairServices => Set<HairService>();
        public DbSet<WorkingInterval> WorkingIntervals => Set<WorkingInterval>();
        public DbSet<Review> Reviews => Set<Review>();

        // Even if the many-to-many connection table is automatically created in the DB, if you set it here it will help you to access it.
        public DbSet<EmployeeHairService> EmployeesHairServices => Set<EmployeeHairService>();
        public DbSet<AppointmentHairService> AppointmentsHairServices => Set<AppointmentHairService>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HairService>()
                .HasIndex(hairService => hairService.Name)
                .IsUnique();

            // Configuring Foreign Keys With Fluent API (with Collections in the User class).
            builder.Entity<Appointment>()
                .HasOne(u => u.Customer)
                .WithMany(app => app.CustomerAppointments)
                .HasForeignKey(u => u.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Appointment>()
                .HasOne(u => u.Employee)
                .WithMany(app => app.EmployeeAppointments)
                .HasForeignKey(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure one-to-one relationship: Appointments with Reviews, so the ReviewId from the Appointments table needs to be unique.
            builder.Entity<Appointment>()
                .HasIndex(a => a.ReviewId)
                .IsUnique();
        }
    }
}
