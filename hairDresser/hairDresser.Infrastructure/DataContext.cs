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

        // Chiar daca tabelul de legatura (many-to-many) iti este creat automat in DB, daca ii faci si DbSet te ajuta, adica il poti accesa.
        public DbSet<EmployeeHairService> EmployeesHairServices => Set<EmployeeHairService>();
        public DbSet<AppointmentHairService> AppointmentsHairServices => Set<AppointmentHairService>();

        // !!! Daca rulez aplicatia in Console, trebuie sa decomentez metoda OnConfiguring().
        // !!! Daca rulez aplicatia in Presentation (API), trebuie sa comentez metoda OnConfiguring() pt. ca altfel am erori de tipul "Multiple database connections".
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder
        //        .UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // ??? Nu stiu ce ii, a aparut in timp ce scriam metoda...

            builder.Entity<Customer>().HasIndex(customer => customer.Username).IsUnique();

            builder.Entity<HairService>().HasIndex(hairService => hairService.Name).IsUnique();
        }
    }
}
