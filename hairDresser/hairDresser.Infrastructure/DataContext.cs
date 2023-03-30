using hairDresser.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<HairService> HairServices => Set<HairService>();
        public DbSet<WorkingInterval> WorkingIntervals => Set<WorkingInterval>();
        public DbSet<WorkingDay> WorkingDays => Set<WorkingDay>();
        public DbSet<Review> Reviews { get; set; }

        // Even if the many-to-many connection table is automatically created in the DB, if you set it here it will help you to access it.
        public DbSet<EmployeeHairService> EmployeesHairServices => Set<EmployeeHairService>();
        public DbSet<AppointmentHairService> AppointmentsHairServices => Set<AppointmentHairService>();

        //! If I run the application in:
        //- Console => trebuie sa decomentez metoda OnConfiguring().
        //- Presentation (API) => trebuie sa comentez metoda OnConfiguring() pt. ca altfel am erori de tipul "Multiple database connections".
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder
        //        .UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<HairService>().HasIndex(hairService => hairService.Name).IsUnique();

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
