using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Appointments.Queries.GetAllAppointmentsByCustomerId;
using hairDresser.Application.Appointments.Queries.GetAppointmentById;
using hairDresser.Application.Appointments.Queries.GetInWorkAppointmentsByCustomerId;
using hairDresser.Application.Customers.Commands.CreateCustomer;
using hairDresser.Application.Customers.Queries.GetAllCustomers;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Application.Employees.Commands.DeleteEmployee;
using hairDresser.Application.Employees.Queries.GetAllEmployees;
using hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate;
using hairDresser.Application.Employees.Queries.GetEmployeesByServices;
using hairDresser.Application.HairServices.Commands.CreateHairService;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.HairServices.Queries.GetHairServicesByIds;
using hairDresser.Application.Interfaces;
using hairDresser.Application.WorkingDays.Commands.CreateWorkingDay;
using hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays;
using hairDresser.Application.WorkingIntervals.Commands.CreateWorkingInterval;
using hairDresser.Application.WorkingIntervals.Queries.GetAllWorkingIntervals;
using hairDresser.Infrastructure;
using hairDresser.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

//???
// Trebuie sa pun Async la numele metodelor din Controllers?
// Nu imi dau seama in care Controller sa pun 'GetEmployeeIntervalsByDate', unde tu dai un employeeId si un date, si primesti inapoi, calculat pe intervalele de lucru ale unui employee si appointment-urile lui, intervalele posibile la care iti poti face un appointment la employee-ul ales (dupa id primit).

/*
 * DB relationships between entities (domain classes):
 * Un Customer poate sa aibe mai multe Appointments dar un Appointment poate avea doar un singur Customer => One-To-Many.
 * Un Employee poate sa aibe mai multe Appointments dar un Appointment poate avea doar un singur Employee => One-To-Many.
 * Un Employee poate sa aibe mai multe HairServices si un HairService poate sa fie la mai multi Employees => Many-To-Many.
 * Un Appointment poate sa aibe mai multe HairServices si un HairService poate sa fie la mai multe Appointments => Many-To-Many.
-----------------------------------------------------------------------------------------------------------------------------
 * Un Employee poate sa aiba mai multe WorkingDays si un WorkingDay poate sa fie la mai multi Employee => Many-To-Many. Observatie, aceasta legatura o fac prin intermediul tabelei WorkingInterval, unde:
 * Un Employee poate sa aiba mai multe WorkingIntervals dar un WorkingInterval nu poate sa fie la mai multi Employees.
 * Un WorkingDay poate sa aiba mai multe WorkingIntervals dar un WorkingInterval nu poate sa aiba mai multe WorkingDays.
*/

bool showMenu = true;

// di = Dependency Injection
var diContainer = new ServiceCollection()
    //Facem legatura cu server-ul nostru din DB.
    //.AddDbContext<DataContext>(options => options.UseSqlServer(@"Server=DESKTOP-BUA6NME;Database=HairDresserDb;Trusted_Connection=True;MultipleActiveResultSets=True;"))
    .AddDbContext<DataContext>()

    // De fiecare data cand vei vedea ca cineva depinde de IHairServiceRepository, creezi o instanta de HairServiceRepository (la fel si pt. restul).
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IHairServiceRepository, HairServiceRepository>()
    .AddScoped<IEmployeeRepository, EmployeeRepository>()
    .AddScoped<IAppointmentRepository, AppointmentRepository>()
    .AddScoped<IWorkingIntervalRepository, WorkingIntervalRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IWorkingDayRepository, WorkingDayRepository>()

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
    Console.WriteLine("02 - GetAppointmentById");
    Console.WriteLine("03 - GetAllAppointmentsByCustomerId");
    Console.WriteLine("04 - GetInWorkAppointmentsByCustomerId");
    //Console.WriteLine("05 - UpdateAppointment");
    //Console.WriteLine("06 - DeleteAppointment");

    Console.WriteLine("\nCRUD Employee:");
    Console.WriteLine("10 - CreateEmployee");
    Console.WriteLine("11 - ReadEmployees");
    Console.WriteLine("12 - GetAllEmpoyeesByServices"); // X
    Console.WriteLine("13 - GetEmployeeIntervalsByDate");
    //Update
    Console.WriteLine("15 - DeleteEmployee");

    Console.WriteLine("\nCRUD Customers:");
    Console.WriteLine("20 - CreateCustomer");
    Console.WriteLine("21 - ReadCustomers");
    //Update
    //Delete

    Console.WriteLine("\nCRUD WorkingIntervals:");
    Console.WriteLine("30 - CreateWorkingInterval");
    Console.WriteLine("31 - ReadWorkingIntervals");
    //Update
    //Delete

    Console.WriteLine("\nCRUD HairServices:");
    Console.WriteLine("40 - CreateHairService");
    Console.WriteLine("41 - ReadHairServices");
    Console.WriteLine("42 - GetHairServicesByIds");
    //Update
    //Delete

    Console.WriteLine("\nCRUD WorkingDay:");
    Console.WriteLine("50 - CreateWorkingDay");
    Console.WriteLine("51 - ReadWorkingDays\n");

    var userInputCase = Console.ReadLine();
    switch (userInputCase)
    {
        case "00":
            {
                Console.WriteLine("Customer Id?");
                var customerId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Employee Id?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Services? Type each number on a new line. Press the ENTER button to stop.");
                var allHairServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allHairServices)
                {
                    Console.WriteLine($"{service.Id} - '{service.Name}', '{service.Duration}', '{service.Price}'");
                }
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

                var command = new CreateAppointmentCommand
                {
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    HairServicesId = hairServicesId,
                    StartDate = start,
                    EndDate = end
                };
                var appointmentId = await mediator.Send(command);

                Console.WriteLine($"Appointment created with the id: '{appointmentId}'");
                return true;
            }
        case "01":
            {
                var customersAppointments = await mediator.Send(new GetAllAppointmentsQuery());
                foreach (var app in customersAppointments)
                {
                    var appointmentHairServices = app.AppointmentHairServices.Select(hairServices => hairServices.HairService.Name);
                    Console.WriteLine($"{app.Id} - customer= '{app.Customer.Name}', employee= '{app.Employee.Name}', start= '{app.StartDate}', end= '{app.EndDate}', hairservices= '{String.Join(", ", appointmentHairServices)}'");
                }
                return true;
            }
        case "02":
            {
                Console.WriteLine("Appointment Id?");
                var appointmentId = Int32.Parse(Console.ReadLine());

                var appointment = await mediator.Send(new GetAppointmentByIdQuery
                {
                    AppointmentId = appointmentId
                });

                var appointmentHairServices = appointment.AppointmentHairServices.Select(hairServices => hairServices.HairService.Name);
                Console.WriteLine($"{appointment.Id} - customer= '{appointment.Customer.Name}', employee= '{appointment.Employee.Name}', start= '{appointment.StartDate}',  end= '{appointment.EndDate}',  hairservices= '{String.Join(", ", appointmentHairServices)}'");

                return true;
            }
        case "03":
            {
                Console.WriteLine("Customer Id?");
                var customerId = Int32.Parse(Console.ReadLine());

                var customerAppointments = await mediator.Send(new GetAllAppointmentsByCustomerIdQuery
                {
                    CustomerId = customerId
                });

                foreach (var appointment in customerAppointments)
                {
                    var appointmentHairServices = appointment.AppointmentHairServices.Select(hairServices => hairServices.HairService.Name);
                    Console.WriteLine($"{appointment.Id} - customer= '{appointment.Customer.Name}', employee= '{appointment.Employee.Name}', start= '{appointment.StartDate}',  end= '{appointment.EndDate}',  hairservices= '{String.Join(", ", appointmentHairServices)}'");
                }
                return true;
            }
        case "04":
            {
                Console.WriteLine("Customer Id?");
                var customerId = Int32.Parse(Console.ReadLine());

                var customerAppointments = await mediator.Send(new GetInWorkAppointmentsByCustomerIdQuery
                {
                    CustomerId = customerId
                });

                foreach (var appointment in customerAppointments)
                {
                    var appointmentHairServices = appointment.AppointmentHairServices.Select(hairServices => hairServices.HairService.Name);
                    Console.WriteLine($"{appointment.Id} - customer= '{appointment.Customer.Name}', employee= '{appointment.Employee.Name}', start= '{appointment.StartDate}',  end= '{appointment.EndDate}',  hairservices= '{String.Join(", ", appointmentHairServices)}'");
                }

                return true;
            }
        case "10":
            {
                Console.WriteLine("\nEmployee Name?");
                var name = Console.ReadLine();

                Console.WriteLine("Specializations? Type each number on a new line. Press the ENTER button to stop.");
                var allServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allServices)
                {
                    Console.WriteLine($"{service.Id} - '{service.Name}', '{service.Duration}', '{service.Price}'");
                }
                var specializationsIds = new List<int>();
                var employeeSpecialization = Console.ReadLine();
                while (employeeSpecialization != "")
                {
                    specializationsIds.Add(Int32.Parse(employeeSpecialization));
                    employeeSpecialization = Console.ReadLine();
                }

                await mediator.Send(new CreateEmployeeComand
                {
                    Name = name,
                    HairServicesIds = specializationsIds
                });
                return true;
            }
        case "11":
            {
                var allEmployees = await mediator.Send(new GetAllEmployeesQuery());
                Console.Write("All employees:\n");
                foreach (var employee in allEmployees)
                {
                    var employeeHairServices = employee.EmployeeHairServices.Select(hairServices => hairServices.HairService.Name);
                    Console.WriteLine($"{employee.Id} - name= '{employee.Name}', hairservices= '{String.Join(", ", employeeHairServices)}'");
                }
                return true;
            }
        case "12":
            {
                Console.WriteLine("Hair services? Type each number on a new line. Press the ENTER button to stop.");
                var allHairServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allHairServices)
                {
                    Console.WriteLine($"'{service.Id}' - '{service.Name}', '{service.Duration}', '{service.Price}'");
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
                    //???Cum pot accesa specializarile unui employee?
                    foreach (var employee in validEmployees)
                    {
                        Console.WriteLine($"{employee.Id} - name= '{employee.Name}', specializations= '?'");
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
                //! Trebuie sa fac o validare sa nu fie un date din trecut. De ex. noi suntem pe data de 16 si el insereaza data de 14 pt. appointment.

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

                await mediator.Send(new DeleteEmployeeCommand
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
                //!!! Trebuie sa ii pun toate workingday in fata ca sa isi aleaga.
                var dayId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Employee Id?");
                //!!! Trebuie sa ii pun toti employee in fata ca sa isi aleaga.
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Start Time? (ex: 09:30:05, 18:00:00, ...)");
                var start = Console.ReadLine();

                Console.WriteLine("End Time?");
                var end = Console.ReadLine();

                await mediator.Send(new CreateWorkingIntervalCommand
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
                var allWorkingIntervals = await mediator.Send(new GetAllWorkingIntervalsQuery());
                foreach (var workingInterval in allWorkingIntervals)
                {
                    Console.WriteLine($"{workingInterval.Id} - '{workingInterval.WorkingDay.Name}', '{workingInterval.Employee.Name}', '{workingInterval.StartTime}', '{workingInterval.EndTime}'");
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
                    Console.WriteLine($"{service.Id} - '{service.Name}', '{service.Duration}', '{service.Price}'");
                }
                return true;
            }
        case "42":
            {
                Console.WriteLine("Hair services? Type each number on a new line. Press the ENTER button to stop.");
                var allHairServices = await mediator.Send(new GetAllHairServicesQuery());
                foreach (var service in allHairServices)
                {
                    Console.WriteLine($"'{service.Id}' - '{service.Name}', '{service.Duration}', '{service.Price}'");
                }

                var hairServicesId = new List<int>();
                var inputService = Console.ReadLine();
                while (inputService != "")
                {
                    hairServicesId.Add(Int32.Parse(inputService));
                    inputService = Console.ReadLine();
                }

                var servicesByIds = await mediator.Send(new GetHairServicesByIdsQuery
                {
                    HairServicesIds = hairServicesId
                });

                Console.WriteLine("All the services based on the selected ids (eu printez doar id-ul si numele, dar sunt toate proprietatile):");
                foreach(var service in servicesByIds)
                {
                    Console.WriteLine(service.Id + " - " + service.Name);
                }
                return true;
            }
        //case "50":
        //    {
        //        Console.WriteLine("Name?");
        //        var nameOfWorkingDay = Console.ReadLine();
        //        await mediator.Send(new CreateWorkingDayCommand
        //        {
        //            Name = nameOfWorkingDay
        //        });
        //        return true;
        //    }
        case "51":
            {
                var allWorkingDays = await mediator.Send(new GetAllWorkingDaysQuery());
                Console.WriteLine("All working days:");
                foreach(var workingDay in allWorkingDays)
                {
                    Console.WriteLine($"{workingDay.Id} - '{workingDay.Name}'");
                }
                return true;
            }
        default:
            return true;
    }
}
