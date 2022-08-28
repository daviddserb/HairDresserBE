using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure
{
    public class DataContext : DbContext
    {
        //public DataContext() 
        //{
        //    this.ChangeTracker.LazyLoadingEnabled = false;
        //}

        // !!! S-ar putea cand lucrez pe consola, sa trebuiasca sa comentez acest constructor.
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<HairService> HairServices => Set<HairService>();
        public DbSet<WorkingInterval> WorkingIntervals => Set<WorkingInterval>();
        public DbSet<WorkingDay> WorkingDays => Set<WorkingDay>();

        // Chiar daca tabelul de legatura (many-to-many) iti este creat automat in DB, daca ii faci si DbSet te ajuta, adica il poti accesa.
        public DbSet<EmployeeHairService> EmployeesHairServices => Set<EmployeeHairService>();
        public DbSet<AppointmentHairService> AppointmentsHairServices => Set<AppointmentHairService>();

        //!!! Am conectarea cu connection string-ul de la DB si in Program, diContainer, dar daca sterg OnConfiguring cu string connection-ul am ceva erori.
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder
                .UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
            //optionBuilder.UseLazyLoadingProxies(true);
        }
    }
}
