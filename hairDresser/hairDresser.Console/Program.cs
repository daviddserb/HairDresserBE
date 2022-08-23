using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Customers.Commands.CreateCustomer;
using hairDresser.Application.Customers.Queries.GetAllCustomers;
using hairDresser.Application.Days.Commands.CreateDay;
using hairDresser.Application.Days.Queries.GetAllDays;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Application.Employees.Commands.DeleteEmployeeById;
using hairDresser.Application.Employees.Queries.GetAllEmployees;
using hairDresser.Application.Employees.Queries.GetEmployeeById;
using hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate;
using hairDresser.Application.Employees.Queries.GetEmployeesByServices;
using hairDresser.Application.HairServices.Commands.CreateHairService;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.Interfaces;
using hairDresser.Application.WorkingDays.Commands.CreateWorkingDay;
using hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//Intrebari -> ???
// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? De ex. suntem pe data de 13 si el sa aiba un appointment pe data de 14 si inca unul pe data de 19?
//In functie de asta, la functionalitatea de DeleteAppointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile pe care le are in viitor (adica in proces), corect? Si din lista de appointment-uri trebuie sa aleaga pe care vrea sa il stearga, corect?
//Tot in functie de asta, la functionalitatea de UpdateAppointment, ma gandesc ca tot trebuie sa ii dau o lista cu appointment-urile pe care le are in viitor (in proces) si de acolo sa aleaga unul. Dar dupa asta, la ce mai exact sa faca update? Ma gandesc sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la CreateAppointment. Este ok?

// Application -> Employees -> Queries - GetEmployeeIntervalsByDateQuery
// Infrastructure -> EmployeeRepository
// Domain -> Models -> WorkingDay
// In Infrastructure -> Migrari, pt. ca le-am mutat din Domain, la namespace daca schimb din Domain in Infrastructure, am ceva erori la: BuildModel, BuildTargetModel

// Trebuie sa ma gandesc la o relatie cand CreateEmployee. Adica eu ii dau numele si dupa serviciile pe care le face. Dar aici, ma gandesc la 2 optiuni:
//1. Eu in tabela HairServices sa am toate serviciile pe care le vreau ca employee mei sa le faca, nu si altele si atunci employee isi alege din lista ce stie el sa faca.
//2. Cand employee isi adauga serviciile, poate unul din ele nu il am in tabela mea HairServices si atunci cumva ar trebui sa il adaug ca sa ii pot da o durata si un price.
//Cum mai exact sa fac?

bool showMenu = true;

// di = Dependency Injection
var diContainer = new ServiceCollection()
    //
    .AddDbContext<DataContext>(options => options.UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDb;Trusted_Connection=True;MultipleActiveResultSets=True;"))

    // De fiecare data cand vei vedea ca cineva depinde de IHairServiceRepository, creezi o instanta de HairServiceRepository (la fel si pt. restul).
    .AddScoped<IHairServiceRepository, HairServiceRepository>()
    .AddScoped<IEmployeeRepository, EmployeeRepository>()
    .AddScoped<IAppointmentRepository, AppointmentRepository>()
    .AddScoped<IWorkingDayRepository, WorkingDayRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IDayRepository, DayRepository>()

    // Adaugam MediatR, care scaneaza toate mesajele (Queries/Commands) si toate handle-urile, de tipul typeof().
    // Cu toate ca noi avem mai multe .AddScoped(), adaugam doar unul dintre ele la typeof() si MediatR le scaneaza pe toate din layer-ul de unde typeof() face parte, adica IHairServiceRepository face parte din Application.
    .AddMediatR(typeof(IHairServiceRepository))

    // Build-uim acest container ...
    .BuildServiceProvider();
var mediator = diContainer.GetRequiredService<IMediator>();

while (showMenu)
{
    showMenu = await MainMenuAsync();
}

async Task<bool> MainMenuAsync()
{
    Console.WriteLine("\nCRUD Appointment:");
    Console.WriteLine("00 - CreateAppointment");
    Console.WriteLine("01 - ReadAppointments");
    //Console.WriteLine("02 - UpdateAppointment");
    //Console.WriteLine("03 - DeleteAppointment");

    Console.WriteLine("\nCRUD Employee:");
    Console.WriteLine("10 - CreateEmployee");
    Console.WriteLine("11 - ReadEmployees");
    Console.WriteLine("12 - GetAllEmpoyeesByServices");
    Console.WriteLine("13 - GetEmployeeIntervalsByDate");
    //Update
    Console.WriteLine("15 - DeleteEmployee");

    Console.WriteLine("\nCRUD Customers:");
    Console.WriteLine("20 - CreateCustomer");
    Console.WriteLine("21 - ReadCustomers");

    Console.WriteLine("\nCRUD WorkingDays:");
    Console.WriteLine("30 - CreateWorkingDay");
    Console.WriteLine("31 - ReadWorkingDays");
    //Update
    //Delete

    Console.WriteLine("\nCRUD HairServices:");
    Console.WriteLine("40 - CreateHairService");
    Console.WriteLine("41 - ReadHairServices");
    //Update
    //Delete

    Console.WriteLine("\nCRUD Day:");
    Console.WriteLine("50 - CreateDay");
    Console.WriteLine("51 - ReadDays\n");

    var userInputCase = Console.ReadLine();
    switch (userInputCase)
    {
        case "00":
            {
                Console.WriteLine("Customer Name?");
                var customerName = Console.ReadLine();

                Console.WriteLine("Employee Id?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Hair services? Type each number on a new line. Press the ENTER button to stop.");
                var allHairServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allHairServices)
                {
                    Console.WriteLine($"id= '{service.Id}' -> name= '{service.Name}', duration= '{service.Duration}', price= '{service.Price}'");
                }

                // !!! Aici tot id-uri trebuie sa inserez
                var hairServicesId = new List<int>();
                var inputService = Console.ReadLine();
                while (inputService != "")
                {
                    hairServicesId.Add(Int32.Parse(inputService));
                    inputService = Console.ReadLine();
                }

                Console.WriteLine("Start Date (for ex.: 8/26/2022 12:00:00)?");
                var start = Console.ReadLine();
                Console.WriteLine("End Date?");
                var end = Console.ReadLine();

                await mediator.Send(new CreateAppointmentCommand
                {
                    CustomerName = customerName,
                    EmployeeId = employeeId,
                    HairServicesId = hairServicesId,
                    StartDate = start,
                    EndDate = end
                });
                return true;
            }
        case "01":
            {
                var allAppointments = await mediator.Send(new GetAllAppointmentsQuery());
                foreach (var app in allAppointments)
                {
                    Console.WriteLine(app.Id + " - " + app.CustomerName + " - " + app.EmployeeName + " - " + app.HairServices + " - " + app.StartDate + " - " + app.EndDate);
                }
                return true;
            }
        case "10":
            {
                Console.WriteLine("\nEmployee Name?");
                var name = Console.ReadLine();

                // ??? Aici cand vreau sa creez un Employee, cand se adauga specializarile lui, ele trebuie sa fie de tip string, adica nu mai poti cu id (int), corect?
                Console.WriteLine("Specializations? Type each one on a new line. Press the ENTER button to stop.");
                var specializations = new List<string>();
                var skills = Console.ReadLine();
                while (skills != "")
                {
                    specializations.Add(skills);
                    skills = Console.ReadLine();
                }

                await mediator.Send(new CreateEmployeeComand
                {
                    Name = name,
                    Specializations = specializations
                });
                return true;
            }

        case "11":
            {
                var allEmployees = await mediator.Send(new GetAllEmployeesQuery());
                Console.Write("All employees:\n");
                foreach (var employee in allEmployees)
                {
                    Console.WriteLine($"id= '{employee.Id}', name= '{employee.Name}', specialization= '{employee.Specialization}'");
                }
                return true;
            }

        case "12":
            {
                Console.WriteLine("Hair services? Type each number on a new line. Press the ENTER button to stop.");
                var allHairServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allHairServices)
                {
                    Console.WriteLine($"id= '{service.Id}' -> name= '{service.Name}', duration= '{service.Duration}', price= '{service.Price}'");
                }

                var hairServicesId = new List<int>();
                var inputService = Console.ReadLine();
                while (inputService != "")
                {
                    hairServicesId.Add(Int32.Parse(inputService));
                    inputService = Console.ReadLine();
                }

                var validEmployees = await mediator.Send(new GetEmployeesByServicesQuery(hairServicesId));
                if (!validEmployees.Any())
                {
                    Console.WriteLine("Nobody can help you.");
                }
                else
                {
                    Console.WriteLine("All employees who can help you:");
                    foreach (var employee in validEmployees)
                    {
                        Console.WriteLine(employee.Id + " - " + employee.Name + " - " + employee.Specialization);
                    }
                }
                return true;
            }

        case "13":
            {
                Console.WriteLine("Employee Id?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Day of the date, in numbers (ex: 01, 15, ...), from this month?");
                var date = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Time, in minutes?");
                var durationInMinutes = Int32.Parse(Console.ReadLine());

                var validIntervals = await mediator.Send(new GetEmployeeIntervalsByDateQuery
                {
                    EmployeeId = employeeId,
                    Date = date,
                    DurationInMinutes = durationInMinutes
                });

                Console.WriteLine("All valid intervals from the selected employee and in the selected date:");
                foreach (var interval in validIntervals)
                {
                    Console.WriteLine(interval.startDate + " - " + interval.endDate);
                }
                return true;
            }

        case "15":
            {
                Console.WriteLine("Employee Id?");
                var employeeId = Int32.Parse(Console.ReadLine());

                await mediator.Send(new DeleteEmployeeByIdCommand
                {
                    Id = employeeId
                });
                return true;
            }

        case "20":
            {
                Console.WriteLine("Name?");
                var name = Console.ReadLine();

                Console.WriteLine("Username?");
                var username = Console.ReadLine();

                Console.WriteLine("Password?");
                var password = Console.ReadLine();

                Console.WriteLine("Email?");
                var email = Console.ReadLine();

                Console.WriteLine("Phone?");
                var phone = Console.ReadLine();

                Console.WriteLine("Address?");
                var address = Console.ReadLine();

                await mediator.Send(new CreateCustomerCommand
                {
                    Name = name,
                    Username = username,
                    Password = password,
                    Email = email,
                    Phone = phone,
                    Address = address
                });

                return true;
            }

        case "21":
            {
                Console.WriteLine("All customers:");
                var allCustomers = await mediator.Send(new GetAllCustomersQuery());
                foreach (var customer in allCustomers)
                {
                    Console.WriteLine(customer.Id + " - " + customer.Name + " - " + customer.Username + " - " + customer.Password + " - " + customer.Email + " - " + customer.Phone + " - " + customer.Address);
                }
                return true;
            }

        case "30":
            {
                Console.WriteLine("Day Id? (ex: 1 - Monday, 2 - Tuesday, ..., 5 - Friday)");
                var dayId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Employee Id?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Start Time? (ex: 09:30:05, 18:00:00, ...)");
                var start = Console.ReadLine();

                Console.WriteLine("End Time?");
                var end = Console.ReadLine();

                await mediator.Send(new CreateWorkingDayCommand
                {
                    DayId = dayId,
                    EmployeeId = employeeId,
                    StartTime = start,
                    EndTime = end
                });
                return true;
            }

        case "31":
            {
                var allWorkingDays = await mediator.Send(new GetAllWorkingDaysQuery());
                foreach (var workingDay in allWorkingDays)
                {
                    var employee = await mediator.Send(new GetEmployeeByIdQuery
                    {
                        Id = workingDay.EmployeeId
                    });

                    Console.WriteLine($"employeeName= '{employee.Name}', dayId= '{workingDay.DayId}', start= '{workingDay.StartTime}', end= '{workingDay.EndTime}'");
                }
                return true;
            }

        case "40":
            {
                Console.WriteLine("Name?");
                var name = Console.ReadLine();

                Console.WriteLine("Duration in minutes?");
                var durationInMinutes = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Price?");
                var price = Int32.Parse(Console.ReadLine());

                await mediator.Send(new CreateHairServiceCommand
                {
                    Name = name,
                    DurationInMinutes = durationInMinutes,
                    Price = price
                });
                return true;
            }

        case "41":
            {
                var allServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allServices)
                {
                    Console.WriteLine($"name= '{service.Name}', duration= '{service.Duration}', price= '{service.Price}'");
                }
                return true;
            }

        case "50":
            {
                Console.WriteLine("Name?");
                var name = Console.ReadLine();

                await mediator.Send(new CreateDayCommand
                {
                    Name = name
                });
                return true;
            }
        case "51":
            {
                var allDays = await mediator.Send(new GetAllDaysQuery());
                Console.WriteLine("All days:");
                foreach(var day in allDays)
                {
                    Console.WriteLine($"id= '{day.Id}', name= '{day.Name}'");
                }
                return true;
            }

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}