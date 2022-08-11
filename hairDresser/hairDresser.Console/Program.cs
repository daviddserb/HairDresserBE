using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// Global things ->
// Variables:
bool showMenu = true;
int cnt = 0; // ??? Am nevoie de asta sau tre sa folosesc Console.Clear()? Am incercat dar nu mi-am prea dat seama ce face.
List<string> customerHairServices = new List<string>();
TimeSpan durationHairServices = new TimeSpan(00, 00, 00);
double priceHairServices = 0.0;
int employeeId = 0;
var customerValidIntervals = new List<(DateTime startDate, DateTime endDate)>();
string customerName = "";
// Objects:
var appointmentRepo = new AppointmentRepository();
var employeeRepo = new EmployeeRepository();
var employee = new Employee();
var hairServices = HairService.GenerateHairServices();

// ???
//Intrebari:
// Stiu ca data trecuta am vorbit de Metodele mele din WorkingDays si HairServices si mi-ai zis sa le mut dar a ramas sa vorbim data viitoare. Totusi, nu le fac si lor un Repository
//pentru fiecare? Adica, is destul de sigur ca voi avea cate o tabela in baza de date pt. fiecare in parte.

// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? Adica suntem pe data de 13 si el sa aiba una pe 14 sa zicem si inca una pe 19? Astfel sa stiu
//cand le selectez, daca pun o limita sau nu?
//Si tot In functie de asta, la Delete Appointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile
//pe care le are in viitor (adica in proces) si de acolo sa aleaga pe unul dintre ele pe care sa il stearga?

// La Update Appointment, la ce mai exact sa faca update? Adica, poate sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la Create.

// Ce sa mai fac:
//sa vad cu variabilele globale cum sa mai reduc din ele...? Aici nu stiu exact cand ii bine sa fac variabile globale, si cum sa ma impart cu functiile adica care sa returneze sau care doar sa afiseze.
//in functie de hair service ales, sa adaug timpul si pretul care corespunde din serviciul respectiv, nu sa il introduc eu...
//

// (showMenu) same thing as (showMenu == true).
while (showMenu)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    // Folosesc if() ca sa printez ce ii in el doar o singura data, la start-ul aplicatiei.
    if (cnt++ == 0)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("0 - CRUD Appointment");
        Console.WriteLine("1 - CRUD Employee");
    }

    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "0":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Appointment");
            Console.WriteLine("1 - Read Appointment"); //
            Console.WriteLine("2 - Update Appointment");
            Console.WriteLine("3 - Delete Appointment");

            var userInput0 = Console.ReadLine();
            switch (userInput0)
            {
                case "0":
                    GetAllHairServices();
                    PickHairServices();
                    if (GetAllEmpoyeesForServices())
                    {
                        GetAvailableTimeSpotsForEmployee(PickDateForAppointment());
                        SaveAppointment(SelectDateForAppointment());
                    }
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    return false;
            }
            return true;

        case "1":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Employee");
            Console.WriteLine("1 - Read Employee");
            Console.WriteLine("2 - Update Employee");
            Console.WriteLine("3 - Delete Employee");

            var userInput1 = Console.ReadLine();
            switch (userInput1)
            {
                case "0":
                    AddEmployee();
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    DeleteEmployee();
                    return false;
            }
            return true;

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}

void GetAllHairServices()
{
    Console.Write("All hair services we can offer:\n");
    for (int i = 0; i < hairServices.Count; ++i)
    {
        Console.WriteLine($"{i} - '{hairServices[i].ServiceName}' - '{hairServices[i].Duration}' - '{hairServices[i].Price}'");
    }
}

void PickHairServices()
{
    Console.WriteLine("Type each number that represents the service on a new line.\nIf you want to stop, type anything but the printed numbers.");
    // ??? Aici parcurg for-ul NU ma folosesc de el, adica nu ma folosesc de pozitia pe care sunt, pt. ca nu as avea durata si price corecte.
    for (int i = 0; i <= hairServices.Count - 1; ++i)
    {
        var userInput = Console.ReadLine();
        if (userInput == "0")
        {
            Console.WriteLine("You selected wash.");
            customerHairServices.Add("wash");
            durationHairServices += hairServices[0].Duration;
            priceHairServices += hairServices[0].Price;
        }
        else if (userInput == "1")
        {
            Console.WriteLine("You selected cut.");
            customerHairServices.Add("cut");
            durationHairServices += hairServices[1].Duration;
            priceHairServices += hairServices[1].Price;
        }
        else if (userInput == "2")
        {
            Console.WriteLine("You selected dye.");
            customerHairServices.Add("dye");
            durationHairServices += hairServices[2].Duration;
            priceHairServices += hairServices[2].Price;
        }
        else
        {
            break;
        }
    }
}

bool GetAllEmpoyeesForServices()
{
    var validEmployees = new List<Employee>();
    foreach (var employee in employeeRepo.GetAllEmployees())
    {
        var invariantText = employee.Specialization.ToUpperInvariant();
        bool matches = customerHairServices.All(hs => invariantText.Contains(hs.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(employee);
        }
    }

    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNobody can help you.");
        return false;
    }
    else
    {
        Console.WriteLine("\nAll the employees that can help you. Pick one of them:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
        employeeId = Int32.Parse(Console.ReadLine());
        return true;
    }
}

DateTime PickDateForAppointment()
{
    Console.WriteLine("Type the date of the day for your appointment.");
    var userInput = Console.ReadLine();
    DateTime dateOfAppointment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int32.Parse(userInput));
    return dateOfAppointment;
}

void GetAvailableTimeSpotsForEmployee(DateTime date)
{
    var employeeName = employeeRepo.GetEmployee(employeeId).Name;
    //Console.WriteLine("    (START testing) -> The new list of appointments:");
    //Console.WriteLine(employeeName);
    //Console.WriteLine(date);
    //Console.WriteLine(durationHairServices);
    //Console.WriteLine("    (STOP testing) -> The new list of appointments:");

    var appointments = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("\nAll the appointments from the selected employee and the selected day:");
    foreach (var app in appointmentRepo.GetInWorkAppointments(employeeName, date))
    {
        appointments.Add((app.StartDate, app.EndDate));
        Console.WriteLine(app.CustomerName + " - " + app.EmployeeName + " - " + app.StartDate + " - " + app.EndDate);
    }

    var sortedAppointments = appointments.OrderBy(s => s.startDate);
    Console.WriteLine("All the appointments from the selected employee and the selected day sorted:");
    foreach (var sortedApp in sortedAppointments)
    {
        Console.WriteLine(sortedApp.startDate + " - " + sortedApp.endDate);
    }

    DateTime dayStart = new DateTime();
    var dayFinish = new DateTime();
    foreach (var time in GetTimeByNameOfDay(date))
    {
        dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item1.Hours, time.Item1.Minutes, time.Item1.Seconds);
        dayFinish = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item2.Hours, time.Item2.Minutes, time.Item2.Seconds);
    }
    Console.WriteLine("The working hours: " + dayStart.TimeOfDay + " - " + dayFinish.TimeOfDay);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(dayStart);
    foreach (var sortedApp in sortedAppointments)
    {
        possibleIntervals.Add(sortedApp.startDate);
        possibleIntervals.Add(sortedApp.endDate);
    }
    possibleIntervals.Add(dayFinish);
    Console.WriteLine("\nPossible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    Console.WriteLine("\nCheck for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += durationHairServices) <= endOfInterval)
        {
            Console.WriteLine("Valid dates in this interval: " + copy_startOfInterval + " - " + startOfInterval);

            // Pt. ca vreau sa salvez (start, start + durata), (start + durata, start + durata + durata), (start + durata + durata, start + durata + durata + durata), etc...
            customerValidIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }
}

Tuple<DateTime, DateTime> SelectDateForAppointment()
{
    Console.WriteLine("\nPick an interval for your appointment from this list:");
    for (int i = 0; i < customerValidIntervals.Count; ++i)
    {
        Console.WriteLine(i + " -> " + customerValidIntervals[i].startDate + " - " + customerValidIntervals[i].endDate);
    }
    var userInput = Int32.Parse(Console.ReadLine());
    return Tuple.Create(customerValidIntervals[userInput].startDate, customerValidIntervals[userInput].endDate);
}

void SaveAppointment(Tuple<DateTime, DateTime> pickedInterval)
{
    Console.WriteLine("Under what name you want to save the appointment?");
    customerName = Console.ReadLine();

    var appointment = new Appointment();
    appointment.CustomerName = customerName;
    appointment.EmployeeName = employeeRepo.GetEmployee(employeeId).Name;
    for (int i = 0; i < customerHairServices.Count; ++i)
    {
        appointment.HairServiceName += customerHairServices[i];
        if (i < customerHairServices.Count - 1)
        {
            appointment.HairServiceName += ", ";
        }
    }
    appointment.StartDate = pickedInterval.Item1;
    appointment.EndDate = pickedInterval.Item2;

    appointmentRepo.CreateAppointment(appointment);

    Console.WriteLine("    (START testing) -> The new list of appointments:");
    foreach (var apps in appointmentRepo.GetAllAppointments())
    {
        Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
    }
    Console.WriteLine("    (STOP testing) -> The new list of appointments:");
}

List<(TimeSpan, TimeSpan)> GetTimeByNameOfDay(DateTime date)
{
    var timeOfDay = new List<(TimeSpan startTime, TimeSpan endTime)>();
    var nameOfDay = date.ToString("dddd");
    Console.WriteLine("\nThe name of the day based on the selected date is = " + nameOfDay);

    var workingDays = WorkingDays.GenerateWorkingDays();
    foreach (var w in workingDays)
    {
        if (w.Name == nameOfDay)
        {
            timeOfDay.Add((w.startTime, w.endTime));
        }
    }
    return timeOfDay;
}

void AddEmployee()
{
    Console.WriteLine("What is the name of the employee?");
    var employeeName = Console.ReadLine();

    Console.WriteLine("What are his qualifications? Type each one on a new line:");
    var employeeSpecialization = Console.ReadLine();
    var specializations = new List<string>();
    // userInput2 != "" -> cand se apasa enter.
    for (int i = 0; employeeSpecialization != ""; ++i)
    {
        specializations.Add(employeeSpecialization);
        employeeSpecialization = Console.ReadLine();
    }

    employee.Name = employeeName;
    for (int i = 0; i < specializations.Count; ++i)
    {
        employee.Specialization += specializations[i];
        if (i < specializations.Count - 1)
        {
            employee.Specialization += ", ";
        }
    }

    employeeRepo.CreateEmployee(employee);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}

void DeleteEmployee()
{
    Console.WriteLine("What is the id of the employee?");
    var employeeId = Console.ReadLine();
    employee.Id = Int32.Parse(employeeId);

    employeeRepo.DeleteEmployee(employee.Id);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}

/* COPY OF LAST VERSION THAT DOESN'T WORK (BECAUSE OF GLOBAL VARIABLES):
using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// Global things ->
// Variables:
bool showMenu = true;
bool flag = true; // ??? Am nevoie de asta sau trebuie sa ma folosesc Console.Clear()? Am incercat dar nu mi-am prea dat seama cum functioneaza in totalitate.

TimeSpan durationHairServices = new TimeSpan(00, 00, 00);
double priceHairServices = 0.0;
var customerValidIntervals = new List<(DateTime startDate, DateTime endDate)>();
string customerName = "";
// Objects:
var appointmentRepo = new AppointmentRepository();
var employeeRepo = new EmployeeRepository();
var employee = new Employee();
var hairServices = HairService.GenerateHairServices();

// ???
//Intrebari:
// Stiu ca data trecuta am vorbit de Metodele mele din WorkingDays si HairServices si mi-ai zis sa le mut dar a ramas sa vorbim data viitoare. Totusi, nu le fac si lor un Repository
//pentru fiecare? Adica, is destul de sigur ca voi avea cate o tabela in baza de date pt. fiecare in parte.

// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? Adica suntem pe data de 13 si el sa aiba una pe 14 sa zicem si inca una pe 19? Astfel sa stiu
//cand le selectez, daca pun o limita sau nu?
//Si tot In functie de asta, la Delete Appointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile
//pe care le are in viitor (adica in proces) si de acolo sa aleaga pe unul dintre ele pe care sa il stearga?

// La Update Appointment, la ce mai exact sa faca update? Adica, poate sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la Create.

// Ce sa mai fac:
//Sa vad cu variabilele globale cum sa mai reduc din ele...? Aici nu stiu exact cand ii bine sa fac variabile globale, si cum sa ma impart cu functiile adica care sa returneze sau care doar sa afiseze.

//In functie de hair service ales, sa adaug timpul si pretul care corespunde din serviciul respectiv, nu sa il introduc eu... dar asta depinde de intrebare.
//

// (showMenu) same thing as (showMenu == true).
while (showMenu)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    // Folosesc if() ca sa printez ce ii in el doar o singura data, la start-ul aplicatiei.
    if (flag)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("0 - CRUD Appointment");
        Console.WriteLine("1 - CRUD Employee");
        flag = false;
    }

    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "0":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Appointment");
            Console.WriteLine("1 - Read Appointment");
            Console.WriteLine("2 - Update Appointment");
            Console.WriteLine("3 - Delete Appointment");

            var userInput0 = Console.ReadLine();
            switch (userInput0)
            {
                case "0":
                    GetAllHairServices();

                    // == -1 => nu sunt employee care sa il ajute
                    if (PickEmployee(GetAllEmpoyeesForServices(PickHairServices())) == -1)
                    {
                        GetAvailableTimeSpotsForEmployee(employeeId, PickDateForAppointment());
                        SaveAppointment(SelectDateForAppointment());
                    }
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    return false;
            }
            return true;

        case "1":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Employee");
            Console.WriteLine("1 - Read Employee");
            Console.WriteLine("2 - Update Employee");
            Console.WriteLine("3 - Delete Employee");

            var userInput1 = Console.ReadLine();
            switch (userInput1)
            {
                case "0":
                    AddEmployee();
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    DeleteEmployee();
                    return false;
            }
            return true;

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}

void GetAllHairServices()
{
    Console.Write("All hair services we can offer:\n");
    for (int i = 0; i < hairServices.Count; ++i)
    {
        Console.WriteLine($"{i} - '{hairServices[i].ServiceName}' - '{hairServices[i].Duration}' - '{hairServices[i].Price}'");
    }
}

List<string> PickHairServices()
{
    Console.WriteLine("\nType each number that represents the service on a new line.\nIf you want to stop, just press the Enter button.");
    // ??? Aici parcurg for-ul dar NU ma folosesc de el practic, adica nu ma folosesc de pozitia pe care sunt, pt. ca nu as avea duration si price corecte.
    //O sa vad cum schimb dupa ce vorbesc cu Adina despre metoda din HairServices.
    List<string> customerHairServices = new List<string>();
    for (int i = 0; i <= hairServices.Count - 1; ++i)
    {
        var userInput = Console.ReadLine();
        if (userInput == "0")
        {
            Console.WriteLine("You selected wash.");
            customerHairServices.Add("wash");
            durationHairServices += hairServices[0].Duration;
            priceHairServices += hairServices[0].Price;
        } else if (userInput == "1")
        {
            Console.WriteLine("You selected cut.");
            customerHairServices.Add("cut");
            durationHairServices += hairServices[1].Duration;
            priceHairServices += hairServices[1].Price;
        } else if (userInput == "2")
        {
            Console.WriteLine("You selected dye.");
            customerHairServices.Add("dye");
            durationHairServices += hairServices[2].Duration;
            priceHairServices += hairServices[2].Price;
        } else
        {
            break;
        }
    }
    return customerHairServices;
}

List<Employee> GetAllEmpoyeesForServices(List<string> hairServices)
{
    var validEmployees = new List<Employee>();
    foreach (var employee in employeeRepo.GetAllEmployees())
    {
        // Fac verificarea daca Specializarea unui Employee contine toate Hairservices cerute de un Customer.
        var invariantText = employee.Specialization.ToUpperInvariant();
        bool matches = hairServices.All(hs => invariantText.Contains(hs.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(employee);
        }
    }
    return validEmployees;
}

int PickEmployee(List<Employee> validEmployees) {
    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNo employee can can help you.");
        return -1; // doesn't exist
    }
    else
    {
        Console.WriteLine("\nThis are all the employees that can help you. Pick one of them:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
        return GetEmployeeId();
    }
}

int GetEmployeeId()
{
    var employeeId = Int32.Parse(Console.ReadLine());
    //var employeeName = employeeRepo.GetEmployee(employeeId).Name;
    return employeeId;
}

DateTime PickDateForAppointment()
{
    Console.WriteLine("Type the date of the day for your appointment.");
    var userInput = Console.ReadLine();
    DateTime dateOfAppointment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int32.Parse(userInput));
    return dateOfAppointment;
}

void GetAvailableTimeSpotsForEmployee(int employeeId, DateTime date)
{
    var employeeName = employeeRepo.GetEmployee(employeeId).Name;
    //Console.WriteLine("    (START testing) -> The new list of appointments:");
    //Console.WriteLine(employeeName);
    //Console.WriteLine(date);
    //Console.WriteLine(durationHairServices);
    //Console.WriteLine("    (STOP testing) -> The new list of appointments:");

    var appointments = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("\nAll the appointments from the selected employee and the selected day:");
    foreach (var app in appointmentRepo.GetInWorkAppointments(employeeName, date))
    {
        appointments.Add((app.StartDate, app.EndDate));
        Console.WriteLine(app.CustomerName + " - " + app.EmployeeName + " - " + app.StartDate + " - " + app.EndDate);
    }

    var sortedAppointments = appointments.OrderBy(s => s.startDate);
    Console.WriteLine("All the appointments from the selected employee and the selected day sorted:");
    foreach(var sortedApp in sortedAppointments)
    {
        Console.WriteLine(sortedApp.startDate + " - " + sortedApp.endDate);
    }

    DateTime dayStart = new DateTime();
    var dayFinish = new DateTime();
    foreach (var time in GetTimeByNameOfDay(date))
    {
        dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item1.Hours, time.Item1.Minutes, time.Item1.Seconds);
        dayFinish = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item2.Hours, time.Item2.Minutes, time.Item2.Seconds);
    }
    Console.WriteLine("The working hours: " + dayStart.TimeOfDay + " - " + dayFinish.TimeOfDay);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(dayStart);
    foreach (var sortedApp in sortedAppointments)
    {
        possibleIntervals.Add(sortedApp.startDate);
        possibleIntervals.Add(sortedApp.endDate);
    }
    possibleIntervals.Add(dayFinish);
    Console.WriteLine("\nPossible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    Console.WriteLine("\nCheck for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += durationHairServices) <= endOfInterval)
        {
            Console.WriteLine("Valid dates in this interval: " + copy_startOfInterval + " - " + startOfInterval);

            // Pt. ca vreau sa salvez (start, start + durata), (start + durata, start + durata + durata), (start + durata + durata, start + durata + durata + durata), etc...
            customerValidIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }
}

Tuple<DateTime, DateTime> SelectDateForAppointment() {
    Console.WriteLine("\nPick an interval for your appointment from this list:");
    for (int i = 0; i < customerValidIntervals.Count; ++i)
    {
        Console.WriteLine(i + " -> " + customerValidIntervals[i].startDate + " - " + customerValidIntervals[i].endDate);
    }
    var userInput = Int32.Parse(Console.ReadLine());
    return Tuple.Create(customerValidIntervals[userInput].startDate, customerValidIntervals[userInput].endDate);
}

void SaveAppointment(Tuple<DateTime, DateTime> pickedInterval)
{
    Console.WriteLine("Under what name you want to save the appointment?");
    customerName = Console.ReadLine();

    var appointment = new Appointment();
    appointment.CustomerName = customerName;
    appointment.EmployeeName = employeeRepo.GetEmployee(employeeId).Name;
    for (int i = 0; i < customerHairServices.Count; ++i)
    {
        appointment.HairServiceName += customerHairServices[i];
        if (i < customerHairServices.Count - 1)
        {
            appointment.HairServiceName += ", ";
        }
    }
    appointment.StartDate = pickedInterval.Item1;
    appointment.EndDate = pickedInterval.Item2;

    appointmentRepo.CreateAppointment(appointment);

    Console.WriteLine("    (START testing) -> The new list of appointments:");
    foreach (var apps in appointmentRepo.GetAllAppointments())
    {
        Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
    }
    Console.WriteLine("    (STOP testing) -> The new list of appointments:");
}

List<(TimeSpan, TimeSpan)> GetTimeByNameOfDay(DateTime date)
{
    var timeOfDay = new List<(TimeSpan startTime, TimeSpan endTime)>();
    var nameOfDay = date.ToString("dddd");
    Console.WriteLine("\nThe name of the day based on the selected date is = " + nameOfDay);

    var workingDays = WorkingDays.GenerateWorkingDays();
    foreach (var w in workingDays)
    {
        if (w.Name == nameOfDay)
        {
            timeOfDay.Add((w.startTime, w.endTime));
        }
    }
    return timeOfDay;
}

void AddEmployee()
{
    Console.WriteLine("What is the name of the employee?");
    var employeeName = Console.ReadLine();

    Console.WriteLine("What are his qualifications? Type each one on a new line:");
    var employeeSpecialization = Console.ReadLine();

    var specializations = new List<string>();
    // userInput2 != "" -> cand se apasa enter.
    for (int i = 0; employeeSpecialization != ""; ++i)
    {
        specializations.Add(employeeSpecialization);
        employeeSpecialization = Console.ReadLine();
    }

    employee.Name = employeeName;
    for (int i = 0; i < specializations.Count; ++i)
    {
        employee.Specialization += specializations[i];
        if (i < specializations.Count - 1)
        {
            employee.Specialization += ", ";
        }
    }

    employeeRepo.CreateEmployee(employee);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}

void DeleteEmployee()
{
    Console.WriteLine("What is the id of the employee?");
    var employeeId = Console.ReadLine();
    employee.Id = Int32.Parse(employeeId);

    employeeRepo.DeleteEmployee(employee.Id);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}
*/

/* COPY OF THAT WORKS:
using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// Global things ->
// Variables:
bool showMenu = true;
int cnt = 0; // ??? Am nevoie de asta sau tre sa folosesc Console.Clear()? Am incercat dar nu mi-am prea dat seama ce face.
List<string> customerHairServices = new List<string>();
TimeSpan durationHairServices = new TimeSpan(00, 00, 00);
double priceHairServices = 0.0;
int employeeId = 0;
var customerValidIntervals = new List<(DateTime startDate, DateTime endDate)>();
string customerName = "";
// Objects:
var appointmentRepo = new AppointmentRepository();
var employeeRepo = new EmployeeRepository();
var employee = new Employee();
var hairServices = HairService.GenerateHairServices();

// ???
//Intrebari:
// Stiu ca data trecuta am vorbit de Metodele mele din WorkingDays si HairServices si mi-ai zis sa le mut dar a ramas sa vorbim data viitoare. Totusi, nu le fac si lor un Repository
//pentru fiecare? Adica, is destul de sigur ca voi avea cate o tabela in baza de date pt. fiecare in parte.

// La Read Appointment, are voie un Customer sa aiba 2 progamari in viitor? Adica suntem pe data de 13 si el sa aiba una pe 14 sa zicem si inca una pe 19? Astfel sa stiu
//cand le selectez, daca pun o limita sau nu?
//Si tot In functie de asta, la Delete Appointment, cand un Customer vrea sa dea Delete la un appointment, prima data ar trebui sa-i afisez toate appointment-urile
//pe care le are in viitor (adica in proces) si de acolo sa aleaga pe unul dintre ele pe care sa il stearga?

// La Update Appointment, la ce mai exact sa faca update? Adica, poate sa aiba 3 optiuni: hairservices, employee si date. Astfel, aici ma gandeam cumva sa repet algoritmul de la Create.

// Ce sa mai fac:
//sa vad cu variabilele globale cum sa mai reduc din ele...? Aici nu stiu exact cand ii bine sa fac variabile globale, si cum sa ma impart cu functiile adica care sa returneze sau care doar sa afiseze.
//in functie de hair service ales, sa adaug timpul si pretul care corespunde din serviciul respectiv, nu sa il introduc eu...
//

// (showMenu) same thing as (showMenu == true).
while (showMenu)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    // Folosesc if() ca sa printez ce ii in el doar o singura data, la start-ul aplicatiei.
    if (cnt++ == 0)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("0 - CRUD Appointment");
        Console.WriteLine("1 - CRUD Employee");
    }

    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "0":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Appointment");
            Console.WriteLine("1 - Read Appointment"); //
            Console.WriteLine("2 - Update Appointment");
            Console.WriteLine("3 - Delete Appointment");

            var userInput0 = Console.ReadLine();
            switch (userInput0)
            {
                case "0":
                    GetAllHairServices();
                    PickHairServices();
                    if (GetAllEmpoyeesForServices())
                    {
                        GetAvailableTimeSpotsForEmployee(PickDateForAppointment());
                        SaveAppointment(SelectDateForAppointment());
                    }
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    return false;
            }
            return true;

        case "1":
            Console.WriteLine("Select an option:");
            Console.WriteLine("0 - Create Employee");
            Console.WriteLine("1 - Read Employee");
            Console.WriteLine("2 - Update Employee");
            Console.WriteLine("3 - Delete Employee");

            var userInput1 = Console.ReadLine();
            switch (userInput1)
            {
                case "0":
                    AddEmployee();
                    return false;

                case "1":
                    return false;

                case "2":
                    return false;

                case "3":
                    DeleteEmployee();
                    return false;
            }
            return true;

        // default = cand nu se executa niciun alt case pt. ca input-ul nu corespunde.
        default:
            return true;
    }
}

void GetAllHairServices()
{
    Console.Write("All hair services we can offer:\n");
    for (int i = 0; i < hairServices.Count; ++i)
    {
        Console.WriteLine($"{i} - '{hairServices[i].ServiceName}' - '{hairServices[i].Duration}' - '{hairServices[i].Price}'");
    }
}

void PickHairServices()
{
    Console.WriteLine("Type each number that represents the service on a new line.\nIf you want to stop, type anything but the printed numbers.");
    // ??? Aici parcurg for-ul NU ma folosesc de el, adica nu ma folosesc de pozitia pe care sunt, pt. ca nu as avea durata si price corecte.
    for (int i = 0; i <= hairServices.Count - 1; ++i)
    {
        var userInput = Console.ReadLine();
        if (userInput == "0")
        {
            Console.WriteLine("You selected wash.");
            customerHairServices.Add("wash");
            durationHairServices += hairServices[0].Duration;
            priceHairServices += hairServices[0].Price;
        } else if (userInput == "1")
        {
            Console.WriteLine("You selected cut.");
            customerHairServices.Add("cut");
            durationHairServices += hairServices[1].Duration;
            priceHairServices += hairServices[1].Price;
        } else if (userInput == "2")
        {
            Console.WriteLine("You selected dye.");
            customerHairServices.Add("dye");
            durationHairServices += hairServices[2].Duration;
            priceHairServices += hairServices[2].Price;
        } else
        {
            break;
        }
    }
}

bool GetAllEmpoyeesForServices()
{
    var validEmployees = new List<Employee>();
    foreach (var employee in employeeRepo.GetAllEmployees())
    {
        var invariantText = employee.Specialization.ToUpperInvariant();
        bool matches = customerHairServices.All(hs => invariantText.Contains(hs.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(employee);
        }
    }

    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNobody can help you.");
        return false;
    }
    else
    {
        Console.WriteLine("\nAll the employees that can help you. Pick one of them:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
        employeeId = Int32.Parse(Console.ReadLine());
        return true;
    }
}

DateTime PickDateForAppointment()
{
    Console.WriteLine("Type the date of the day for your appointment.");
    var userInput = Console.ReadLine();
    DateTime dateOfAppointment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int32.Parse(userInput));
    return dateOfAppointment;
}

void GetAvailableTimeSpotsForEmployee(DateTime date)
{
    var employeeName = employeeRepo.GetEmployee(employeeId).Name;
    //Console.WriteLine("    (START testing) -> The new list of appointments:");
    //Console.WriteLine(employeeName);
    //Console.WriteLine(date);
    //Console.WriteLine(durationHairServices);
    //Console.WriteLine("    (STOP testing) -> The new list of appointments:");

    var appointments = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("\nAll the appointments from the selected employee and the selected day:");
    foreach (var app in appointmentRepo.GetInWorkAppointments(employeeName, date))
    {
        appointments.Add((app.StartDate, app.EndDate));
        Console.WriteLine(app.CustomerName + " - " + app.EmployeeName + " - " + app.StartDate + " - " + app.EndDate);
    }

    var sortedAppointments = appointments.OrderBy(s => s.startDate);
    Console.WriteLine("All the appointments from the selected employee and the selected day sorted:");
    foreach(var sortedApp in sortedAppointments)
    {
        Console.WriteLine(sortedApp.startDate + " - " + sortedApp.endDate);
    }

    DateTime dayStart = new DateTime();
    var dayFinish = new DateTime();
    foreach (var time in GetTimeByNameOfDay(date))
    {
        dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item1.Hours, time.Item1.Minutes, time.Item1.Seconds);
        dayFinish = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item2.Hours, time.Item2.Minutes, time.Item2.Seconds);
    }
    Console.WriteLine("The working hours: " + dayStart.TimeOfDay + " - " + dayFinish.TimeOfDay);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(dayStart);
    foreach (var sortedApp in sortedAppointments)
    {
        possibleIntervals.Add(sortedApp.startDate);
        possibleIntervals.Add(sortedApp.endDate);
    }
    possibleIntervals.Add(dayFinish);
    Console.WriteLine("\nPossible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    Console.WriteLine("\nCheck for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += durationHairServices) <= endOfInterval)
        {
            Console.WriteLine("Valid dates in this interval: " + copy_startOfInterval + " - " + startOfInterval);

            // Pt. ca vreau sa salvez (start, start + durata), (start + durata, start + durata + durata), (start + durata + durata, start + durata + durata + durata), etc...
            customerValidIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }
}

Tuple<DateTime, DateTime> SelectDateForAppointment() {
    Console.WriteLine("\nPick an interval for your appointment from this list:");
    for (int i = 0; i < customerValidIntervals.Count; ++i)
    {
        Console.WriteLine(i + " -> " + customerValidIntervals[i].startDate + " - " + customerValidIntervals[i].endDate);
    }
    var userInput = Int32.Parse(Console.ReadLine());
    return Tuple.Create(customerValidIntervals[userInput].startDate, customerValidIntervals[userInput].endDate);
}

void SaveAppointment(Tuple<DateTime, DateTime> pickedInterval)
{
    Console.WriteLine("Under what name you want to save the appointment?");
    customerName = Console.ReadLine();

    var appointment = new Appointment();
    appointment.CustomerName = customerName;
    appointment.EmployeeName = employeeRepo.GetEmployee(employeeId).Name;
    for (int i = 0; i < customerHairServices.Count; ++i)
    {
        appointment.HairServiceName += customerHairServices[i];
        if (i < customerHairServices.Count - 1)
        {
            appointment.HairServiceName += ", ";
        }
    }
    appointment.StartDate = pickedInterval.Item1;
    appointment.EndDate = pickedInterval.Item2;

    appointmentRepo.CreateAppointment(appointment);

    Console.WriteLine("    (START testing) -> The new list of appointments:");
    foreach (var apps in appointmentRepo.GetAllAppointments())
    {
        Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
    }
    Console.WriteLine("    (STOP testing) -> The new list of appointments:");
}

List<(TimeSpan, TimeSpan)> GetTimeByNameOfDay(DateTime date)
{
    var timeOfDay = new List<(TimeSpan startTime, TimeSpan endTime)>();
    var nameOfDay = date.ToString("dddd");
    Console.WriteLine("\nThe name of the day based on the selected date is = " + nameOfDay);

    var workingDays = WorkingDays.GenerateWorkingDays();
    foreach (var w in workingDays)
    {
        if (w.Name == nameOfDay)
        {
            timeOfDay.Add((w.startTime, w.endTime));
        }
    }
    return timeOfDay;
}

void AddEmployee()
{
    Console.WriteLine("What is the name of the employee?");
    var employeeName = Console.ReadLine();

    Console.WriteLine("What are his qualifications? Type each one on a new line:");
    var employeeSpecialization = Console.ReadLine();
    var specializations = new List<string>();
    // userInput2 != "" -> cand se apasa enter.
    for (int i = 0; employeeSpecialization != ""; ++i)
    {
        specializations.Add(employeeSpecialization);
        employeeSpecialization = Console.ReadLine();
    }

    employee.Name = employeeName;
    for (int i = 0; i < specializations.Count; ++i)
    {
        employee.Specialization += specializations[i];
        if (i < specializations.Count - 1)
        {
            employee.Specialization += ", ";
        }
    }

    employeeRepo.CreateEmployee(employee);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}

void DeleteEmployee()
{
    Console.WriteLine("What is the id of the employee?");
    var employeeId = Console.ReadLine();
    employee.Id = Int32.Parse(employeeId);

    employeeRepo.DeleteEmployee(employee.Id);

    Console.WriteLine("    (START testing) -> The new list of employees:");
    foreach (var empl in employeeRepo.GetAllEmployees())
    {
        Console.WriteLine($"'{empl.Name}', '{empl.Specialization}'");
    }
    Console.WriteLine("    (STOP testing) -> The new list of employees:");
}
*/