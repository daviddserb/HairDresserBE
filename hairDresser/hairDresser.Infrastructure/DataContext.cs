using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(){}

        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions){}

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<HairService> HairServices => Set<HairService>();
        public DbSet<WorkingDay> WorkingDays => Set<WorkingDay>();
        public DbSet<Day> Days => Set<Day>();

        //!!! Nu ma folosesc de OnConfiguring momentan, pt. ca am mutat string-ul de legatura in fila de Program.cs din Console.
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder
                .UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                //.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }) // ??? am nevoie de asta?
                //.EnableSensitiveDataLogging(); // ??? am nevoie de asta?
        }
    }
}
