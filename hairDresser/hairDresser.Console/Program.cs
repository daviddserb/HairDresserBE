using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Employees.Commands.CreateEmployee;
using hairDresser.Application.Employees.Commands.DeleteEmployeeById;
using hairDresser.Application.Employees.Queries.GetAllEmployees;
using hairDresser.Application.Employees.Queries.GetEmployeeById;
using hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate;
using hairDresser.Application.Employees.Queries.GetEmployeesByServices;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.Interfaces;
using hairDresser.Application.WorkingDays.Commands.CreateWorkingDay;
using hairDresser.Application.WorkingDays.Queries.GetAllWorkingDays;
using hairDresser.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

//Intrebari: ???
// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? Adica suntem pe data de 13 si el sa aiba una pe 14 sa zicem si inca una pe 19? Astfel sa stiu cand le selectez, daca pun o limita sau nu?
//Si tot In functie de asta, la Delete Appointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile pe care le are in viitor (adica in proces) si de acolo sa aleaga pe unul dintre ele pe care sa il stearga, corect?
// La Update Appointment, la ce mai exact sa faca update? Adica, poate sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la Create.

bool showMenu = true;

// di = Dependency Injection
var diContainer = new ServiceCollection()
    // De fiecare data cand vei vedea ca cineva depinde de IHairServiceRepository, creezi o instanta de HairServiceRepository (la fel si pt. restul).
    .AddScoped<IHairServiceRepository, HairServiceRepository>()
    .AddScoped<IEmployeeRepository, EmployeeRepository>()
    .AddScoped<IAppointmentRepository, AppointmentRepository>()
    .AddScoped<IWorkingDayRepository, WorkingDayRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()

    // Adaugam MediatR, care scaneaza toate mesajele (Queries/Commands) si toate handle-urile, de tipul typeof().
    // Cu toate ca noi avem mai multe .AddScoped(), adaugam doar unul dintre ele la typeof() si MediatR le scaneaza pe toate din layer-ul de unde typeof() face parte, adica IHairServiceRepository face parte din Application.
    .AddMediatR(typeof(IHairServiceRepository))

    // ??? Build-uim acest container = ...?
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
    Console.WriteLine("11 - GetAllEmpoyeesByServices");
    Console.WriteLine("12 - GetEmployeeIntervalsByDate"); // ??? Nu stiu daca apartine de Appointment sau de Employee, atunci cand vrei sa iei toate intervalele unui employee dupa ce customer a selectat un date.
    Console.WriteLine("13 - ReadEmployees");
    //Update
    Console.WriteLine("15 - DeleteEmployee");

    //Console.WriteLine("\nCRUD Customers:"); (??? N-am facut nimic CRUD la Customers inca, trebuie acum?)

    Console.WriteLine("\nCRUD WorkingDays:");
    Console.WriteLine("40 - CreateWorkingDay"); //??? am de lucru
    Console.WriteLine("41 - GetWorkingDayByEmployeeIdBynameOfDay");
    Console.WriteLine("42 - ReadWorkingDays");
    //Update
    //Delete

    Console.WriteLine("\nCRUD HairServices:");
    //Create
    Console.WriteLine("51 - ReadHairServices");
    //Update
    //Delete

    var userInputCase = Console.ReadLine();
    switch (userInputCase)
    {
        case "00":
            {
                Console.WriteLine("Under what name do you want to save the appointment?");
                var customerName = Console.ReadLine();

                Console.WriteLine("What is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Type each hair service name on a new line.\nIf you want to stop, press the ENTER button.");
                var hairServicesPickedByCustomer = new List<string>();
                var inputService = Console.ReadLine();
                while (inputService != "")
                {
                    hairServicesPickedByCustomer.Add(inputService);
                    inputService = Console.ReadLine();
                }

                // ??? Aici nu sunt sigur daca trebuia sa scriu intervalul de start si end date din Consola? Sau sa il extrag din intervalele posibile...? Dar atunci trebuie sa fac legatura cu alta metoda si mi-ai spus sa nu fac asa.
                Console.WriteLine("What is the selected interval (start + end date) for the appointment? Type them on a new line.\nFor example:\n8/19/2022 12:00:00 AM\n8/19/2022 2:50:00 PM");
                var start = Console.ReadLine();
                var end = Console.ReadLine();

                await mediator.Send(new CreateAppointmentCommand
                {
                    CustomerName = customerName,
                    EmployeeId = employeeId,
                    HairServices = hairServicesPickedByCustomer,
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
                Console.WriteLine("\nWhat is the name of the employee?");
                var name = Console.ReadLine();

                Console.WriteLine("What are his specializations? Type each one on a new line. Press the ENTER button when you want to stop.");
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
                // ??? Aici input-ul trebuie sa fie de tipul string la hairServices sau de tipul int adica id-ul de la fiecare hairService?
                //Daca raspunsul este da, ceea ce tind sa cred ca o sa fie, de ce?
                Console.WriteLine("Type each hair service name on a new line.\nIf you want to stop, press the ENTER button.");
                var hairServicesPickedByCustomer = new List<string>();
                var inputService = Console.ReadLine();
                while (inputService != "")
                {
                    hairServicesPickedByCustomer.Add(inputService);
                    inputService = Console.ReadLine();
                }

                var validEmployees = await mediator.Send(new GetEmployeesByServicesQuery(hairServicesPickedByCustomer));
                if (!validEmployees.Any())
                {
                    Console.WriteLine("Nobody can help you.");
                }
                else
                {
                    foreach (var employee in validEmployees)
                    {
                        Console.WriteLine(employee.Id + " - " + employee.Name + " - " + employee.Specialization);
                    }
                }
                return true;
            }

        case "12":
            {
                Console.WriteLine("What is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("What is the date, the day in numbers, from this month for you appointment?");
                var date = Int32.Parse(Console.ReadLine());

                // ??? Aici nu sunt sigur daca trebuia sa il intreb direct cat dureaza SAU sa il pun sa aleaga ce hair services vrea si in functie de ce a ales, sa calculez eu cat dureaza.
                Console.WriteLine("How much time, in minutes, for the appointment?");
                var durationInMinutes = Int32.Parse(Console.ReadLine());

                var intervalsForAppointment = await mediator.Send(new GetEmployeeIntervalsByDateQuery
                {
                    EmployeeId = employeeId,
                    Date = date,
                    DurationInMinutes = durationInMinutes
                });

                //Console.WriteLine("\nBack in case 12 -> all valid intervals for appointments: ");
                //foreach (var interval in intervalsForAppointment)
                //{
                //    Console.WriteLine(interval.startDate + " - " + interval.endDate);
                //}
                return true;
            }

        case "13":
            var allEmployees = await mediator.Send(new GetAllEmployeesQuery());
            Console.Write("All employees:\n");
            foreach (var employee in allEmployees)
            {
                Console.WriteLine($"id= '{employee.Id}', name= '{employee.Name}', specialization= '{employee.Specialization}'");
            }
            return true;

        case "15":
            {
                Console.WriteLine("What is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                await mediator.Send(new DeleteEmployeeByIdCommand
                {
                    Id = employeeId
                });
                return true;
            }

        case "40":
            {
                Console.WriteLine("What is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                // ??? Cand cer input-ul de day, trebuie sa fie string (adica Monday, Tuesday, Wednesday) sau int (adica 1 pt. Monday, 2 pt. Tuesday, etc...)?
                Console.WriteLine("What is the name of the day (ex: Monday, ..., Friday?");
                var nameOfDay = Console.ReadLine();

                // ??? Cand cer input-ul de start/end time pt. day, n-am stiut sigur cum sa-l iau, si l-am lasat de tipul string si cumva o sa-l convertesc in TimeSpan, dar conteaza si cum il scrie employee, sa nu provoace erori (chiar daca ofer exemplu). E ok?
                Console.WriteLine("What is the start time? (ex: 09:30:05 or 18:00:00)");
                var start = Console.ReadLine();

                Console.WriteLine("What is the end time?");
                var end = Console.ReadLine();

                await mediator.Send(new CreateWorkingDayQuery
                {
                    EmployeeId = employeeId,
                    NameOfDay = nameOfDay,
                    StartTime = start,
                    EndTime = end
                });
                return true;
            }

        case "41":
            {
                return true;
            }

        case "42":
            {
                var allWorkingDays = await mediator.Send(new GetAllWorkingDaysQuery());
                foreach (var workingDay in allWorkingDays)
                {
                    var employee = await mediator.Send(new GetEmployeeByIdQuery
                    {
                        Id = workingDay.EmployeeId
                    });

                    Console.WriteLine($"nameOfEmployee= '{employee.Name}', nameOfDay= '{workingDay.Name}', start= '{workingDay.StartTime}', end= '{workingDay.EndTime}'");
                }
                return true;
            }

        case "51":
            var allServices = await mediator.Send(new GetAllHairServicesQuery());
            foreach (var service in allServices)
            {
                Console.WriteLine($"name= '{service.Name}', duration= '{service.Duration}', price= '{service.Price}'");
            }
            return true;

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}