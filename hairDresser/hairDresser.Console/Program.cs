using hairDresser.Application.Appointments.Commands.CreateAppointment;
using hairDresser.Application.Appointments.Queries.GetAllAppointments;
using hairDresser.Application.Employees.Command.CreateEmployee;
using hairDresser.Application.Employees.Command.DeleteEmployeeById;
using hairDresser.Application.Employees.Queries.GetAllEmployees;
using hairDresser.Application.Employees.Queries.GetEmployeeIntervalsForAppointmentByDate;
using hairDresser.Application.Employees.Queries.GetEmployeesByServices;
using hairDresser.Application.HairServices.Queries;
using hairDresser.Application.Interfaces;
using hairDresser.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

//Intrebari: ???
// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? Adica suntem pe data de 13 si el sa aiba una pe 14 sa zicem si inca una pe 19? Astfel sa stiu cand le selectez, daca pun o limita sau nu?
//Si tot In functie de asta, la Delete Appointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile pe care le are in viitor (adica in proces) si de acolo sa aleaga pe unul dintre ele pe care sa il stearga, corect?
// La Update Appointment, la ce mai exact sa faca update? Adica, poate sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la Create.
// GetEmployeeIntervalsForAppointmentByDate nu stiu daca trebuie sa il pun in folderul de Appointments sau de Employees sau altul...?
// Cum pot sa vad aici ce mi-a returnat Handler-ul?

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

// (showMenu) same thing as (showMenu == true).
while (showMenu)
{
    showMenu = await MainMenuAsync();
}

async Task<bool> MainMenuAsync()
{
    Console.WriteLine("                    CRUD Appointment:");
    Console.WriteLine("00 - CreateAppointment");
    Console.WriteLine("01 - ReadAppointments");
    //Console.WriteLine("02 - UpdateAppointment");
    //Console.WriteLine("03 - DeleteAppointment");

    Console.WriteLine("                    CRUD Employee:");
    Console.WriteLine("10 - CreateEmployee");
    Console.WriteLine("11 - GetAllEmpoyeesByServices");
    Console.WriteLine("12 - GetEmployeeIntervalsByDate"); // ??? Nu stiu daca apartine intr-un fel de Appointment sau de Employee
    Console.WriteLine("13 - ReadEmployees");
    //Update
    Console.WriteLine("15 - DeleteEmployee");

    Console.WriteLine("                    CRUD Customers:");

    Console.WriteLine("                    CRUD WorkingDays:");

    Console.WriteLine("                    CRUD HairServices:");
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
                // ??? Aici trebuie input-ul sa fie de tipul string adica hairServiceName sau de tipul int adica id-ul de la fiecare hairServiceName?
                //Daca raspunsul este da (ceea ce cred ca o sa fie):
                //1. ??? De ce?
                //2. !!! Trebuie sa adaug id la clasa din domain HairServices si sa fac si alte modificari peste tot...
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

                Console.WriteLine("\nback in case -> all valid intervals for appointments: ");
                foreach (var interval in intervalsForAppointment)
                {
                    Console.WriteLine(interval.startDate + " - " + interval.endDate);
                }
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

/*
using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// Global things ->
// Variables:
bool showMenu = true;
// Objects:
var appointmentRepo = new AppointmentRepository();
var employeeRepo = new EmployeeRepository();
var hairServiceRepo = new HairServiceRepository();
var workingDayRepo = new WorkingDayRepository();

var appointment = new Appointment();
var employee = new Employee();
var hairServices = new HairService();
var workingDay = new WorkingDay();

// (showMenu) same thing as (showMenu == true).
while (showMenu)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    Console.WriteLine("\n- CRUD Appointment -");
    Console.WriteLine("00 - GetAllHairServices");
    Console.WriteLine("01 - GetAllEmpoyeesForServices");
    Console.WriteLine("02 - GetAvailableTimeSpotsForEmployee");
    Console.WriteLine("03 - CreateAppointment");

    Console.WriteLine("\n- CRUD Employee -");
    Console.WriteLine("10 - AddEmployee");
    Console.WriteLine("11 - GetAllEmployees");
    Console.WriteLine("12 - DeleteEmployee");

    //Console.WriteLine("\n- CRUD WorkingDays -");

    //Console.WriteLine("\n- CRUD HairServices -");

    var userInputCase = Console.ReadLine();
    switch (userInputCase)
    {
        case "00":
            {
                GetAllHairServices();
                return true;
            }
        case "01":
            {
                Console.WriteLine("Type each hair service name on a new line.\nIf you want to stop, press the ENTER button.");
                var hairServicesPickedByCustomer = new List<string>();
                var hairService = Console.ReadLine();
                while (hairService != "")
                {
                    hairServicesPickedByCustomer.Add(hairService);
                    hairService = Console.ReadLine();
                }
                GetAllEmpoyeesForServices(hairServicesPickedByCustomer);
                return true;
            }
        case "02":
            {
                Console.WriteLine("What is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("In what date of this month do you want your appointment?");
                var date = Int32.Parse(Console.ReadLine());

                // ??? Aici nu sunt sigur daca trebuia sa il intreb direct cat dureaza SAU sa il pun sa aleaga ce hair services vrea si in functie de ce a ales, sa calculez eu cat dureaza.
                Console.WriteLine("How much time, in minutes, for the appointment?");
                var durationInMinutes = Int32.Parse(Console.ReadLine());

                GetAvailableTimeSpotsForEmployee(employeeId, date, durationInMinutes);
                return true;
            }
        case "03":
            {
                Console.WriteLine("Under what name do you want to save the appointment?");
                var customerName = Console.ReadLine();

                Console.WriteLine("What is the employeeId?");
                var employeeId = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Type each hair service name on a new line.\nIf you want to stop, press the ENTER button.");
                var hairServicesPickedByCustomer = new List<string>();
                var hairService = Console.ReadLine();
                while (hairService != "")
                {
                    hairServicesPickedByCustomer.Add(hairService);
                    hairService = Console.ReadLine();
                }

                // ??? Aici nu sunt sigur daca trebuia sa scriu intervalul de start si end date din Consola? 
                Console.WriteLine("What is the selected interval (start + end date) for the appointment? Type them on a new line.\nFor example:\n8/19/2022 12:00:00 AM\n8/19/2022 2:50:00 PM");
                var start = Console.ReadLine();
                var end = Console.ReadLine();

                CreateAppointment(customerName, employeeId, hairServicesPickedByCustomer, start, end);
                return true;
            }
        case "10":
            {
                Console.WriteLine("\nWhat is the name of the employee?");
                var employeeName = Console.ReadLine();

                Console.WriteLine("What are his specializations? Type each one on a new line. Press the ENTER button when you want to stop.");
                var employeeSpecializations = new List<string>();
                var specialization = Console.ReadLine();
                while (specialization != "")
                {
                    employeeSpecializations.Add(specialization);
                    specialization = Console.ReadLine();
                }

                AddEmployee(employeeName, employeeSpecializations);
                return true;
            }

        case "11":
            GetAllEmployees();
            return true;

        case "12":
            {
                Console.WriteLine("\nWhat is the id of the employee?");
                var employeeId = Int32.Parse(Console.ReadLine());

                DeleteEmployee(employeeId);
                return true;
            }

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}

void GetAllHairServices()
{
    Console.Write("\nAll the hair services we can offer:\n");
    foreach (var hsr in hairServiceRepo.GetAllHairServices())
    {
        Console.WriteLine($"name= '{hsr.ServiceName}', duration= '{hsr.Duration}', price= '{hsr.Price}'");
    }
}

void GetAllEmpoyeesForServices(List<string> hairServices)
{
    var validEmployees = new List<Employee>();
    foreach (var er in employeeRepo.GetAllEmployees())
    {
        var invariantText = er.Specialization.ToUpperInvariant();
        bool matches = hairServices.All(hspbc => invariantText.Contains(hspbc.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(er);
        }
    }

    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNobody can help you.");
    }
    else
    {
        Console.WriteLine("\nAll the employees that can help you:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
    }
}

void GetAvailableTimeSpotsForEmployee(int employeeId, int date, int durationInMinutes)
{
    var employeeName = employeeRepo.GetEmployee(employeeId).Name;
    Console.WriteLine("employeeName= " + employeeName);

    var appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date);
    Console.WriteLine("appointmentDate= " + appointmentDate);

    var duration = TimeSpan.FromMinutes(durationInMinutes);
    Console.WriteLine("duration from minutes transformed in TimeSpan= " + duration);

    var employeeAppointmentsDates = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("\nAll about the appointments from the selected employee and the selected day:");
    foreach (var ar in appointmentRepo.GetInWorkAppointments(employeeName, appointmentDate))
    {
        employeeAppointmentsDates.Add((ar.StartDate, ar.EndDate));
        Console.WriteLine($"customer= '{ar.CustomerName}', employee= '{ar.EmployeeName}', services= '{ar.HairServiceName}', start= '{ar.StartDate}', end= '{ar.EndDate}'");
    }

    Console.WriteLine("Only the dates:");
    foreach (var ea in employeeAppointmentsDates)
    {
        Console.WriteLine($"start= '{ea.Item1}', end= '{ea.Item2}'");
    }

    var sorted_employeeAppointments = employeeAppointmentsDates.OrderBy(s => s.Item1);
    Console.WriteLine("Only the dates sorted:");
    foreach (var sea in sorted_employeeAppointments)
    {
        Console.WriteLine(sea.Item1 + " - " + sea.Item2);
    }

    var nameOfDay = appointmentDate.ToString("dddd");
    Console.WriteLine($"The name of the day based on the selected date is= '{nameOfDay}'");
    var timeOfDay = workingDayRepo.GetWorkingDay(nameOfDay);
    Console.WriteLine($"start= '{timeOfDay.startTime}', end= '{timeOfDay.endTime}'");
    var timeOfDay_withDate_start = appointmentDate.Add(timeOfDay.startTime);
    var timeOfDay_withDate_end = appointmentDate.Add(timeOfDay.endTime);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(timeOfDay_withDate_start);
    foreach (var sortedApp in sorted_employeeAppointments)
    {
        possibleIntervals.Add(sortedApp.Item1);
        possibleIntervals.Add(sortedApp.Item2);
    }
    possibleIntervals.Add(timeOfDay_withDate_end);

    Console.WriteLine("Possible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    var customerValidIntervals = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("\nCheck for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += duration) <= endOfInterval)
        {
            Console.WriteLine("Valid dates in this interval: " + copy_startOfInterval + " - " + startOfInterval);
            customerValidIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }

    Console.WriteLine("\nAll valid intervals:");
    foreach (var intervals in customerValidIntervals)
    {
        Console.WriteLine($"start= '{intervals.startDate}', end= '{intervals.endDate}'");
    }
}

void CreateAppointment(string customerName, int employeeId, List<string> hairServices, string start, string end) {
    appointment.CustomerName = customerName;

    appointment.EmployeeName = employeeRepo.GetEmployee(employeeId).Name;
    //Console.WriteLine("employeeName= " + appointment.EmployeeName);

    for (int i = 0; i < hairServices.Count; ++i)
    {
        appointment.HairServiceName += hairServices[i];
        if (i != hairServices.Count - 1)
        {
            appointment.HairServiceName += ", ";
        }
    }
    //Console.WriteLine("hairServices= " + appointment.HairServiceName);

    appointment.StartDate = DateTime.Parse(start);
    appointment.EndDate = DateTime.Parse(end);
    //Console.WriteLine("start= " + appointment.StartDate);
    //Console.WriteLine("end= " + appointment.EndDate);

    appointmentRepo.CreateAppointment(appointment);

    Console.WriteLine("The new list of appointments:");
    foreach (var ar in appointmentRepo.GetAllAppointments())
    {
        Console.WriteLine($"customer= '{ar.CustomerName}', employee= '{ar.EmployeeName}', services= '{ar.HairServiceName}', start= '{ar.StartDate}', end= '{ar.EndDate}'");
    }

}

void AddEmployee(string employeeName, List<string> employeeSpecializations)
{
    employee.Name = employeeName;

    Console.WriteLine("What are his specializations? Type each one on a new line:");

    for (int i = 0; i < employeeSpecializations.Count; ++i)
    {
        employee.Specialization += employeeSpecializations[i];
        if (i != employeeSpecializations.Count - 1)
        {
            employee.Specialization += ", ";
        }
    }

    employeeRepo.CreateEmployee(employee);

    // ??? Cand adaug un employee nou, nu ii setez id-ul si va fi valoarea automata (0). Are rost acum sa ii setez si un id sau nu?
    //Intreb pt. ca, atunci cand voi lucra direct cu DB, (cred ca) voi extrage ultimul id si ii cresc valoarea cu 1 ca sa il salvez la employee-ul creat (cel nou).
    //Sau poate nici nu o sa fie nevoie, ca id probabil o sa fie PK si o sa-si creasca el valoarea singur cred.
    Console.WriteLine("The new list of employees:");
    foreach (var er in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
    }
}

void GetAllEmployees()
{
    Console.Write("All employees:\n");
    foreach (var er in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
    }
}

// UpdateEmployee()

void DeleteEmployee(int employeeId)
{
    employee.Id = employeeId;

    employeeRepo.DeleteEmployee(employee.Id);

    Console.WriteLine("The new list of employees:");
    foreach (var er in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"id= '{er.Id}', name= '{er.Name}', specialization= '{er.Specialization}'");
    }
}
*/