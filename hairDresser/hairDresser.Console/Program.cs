using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// global variables
bool showMenu = true;
int cnt = 0; // ??? am nevoie de asta sau tre sa folosesc Console.Clear()?
List<string> hairServices = new List<string>();
TimeSpan durationHairServices = new TimeSpan(00, 00, 00);
int employeeId = 0;
var validIntervals = new List<(DateTime startDate, DateTime endDate)>();
string customerName = "";

while (showMenu) // (showMenu) == (showMenu == true)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    if (cnt++ == 0)
    {
        Console.WriteLine("What you wanna do?");
        Console.WriteLine("1 - Create Appointment");
        Console.WriteLine("2 - Read App");
        Console.WriteLine("3 - Update App");
        Console.WriteLine("4 - Delete App");
    }

    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            GetAllHairServices();
            PickHairServices();
            GetAllEmpoyeesForServices(hairServices);
            GetAvailableTimeSpotsForEmployee(employeeId, PickDateForAppointment(), durationHairServices);
            SaveAppointment(SelectDateForAppointment(validIntervals), employeeId, customerName, hairServices);
            return true;
        case "2":
            return false;
        case "3":
            return true;
        case "4":
            return true;
        default:
            return true;
    }
}

void GetAllHairServices()
{
    foreach (var s in HairService.GenerateHairServices())
    {
        Console.WriteLine($"'{s.ServiceName}' - '{s.Duration}' - '{s.Price}'");
    }
}

void PickHairServices()
{
    Console.WriteLine("\nWhat hair services you want? Type each one on a new line.");
    Console.WriteLine("1 - wash");
    Console.WriteLine("2 - cut");
    Console.WriteLine("3 - dye");
    Console.WriteLine("0 - exit");
    for (int i = 0; i <= HairService.GenerateHairServices().Count(); ++i)
    {
        var userInput = Console.ReadLine();
        if (userInput == "1")
        {
            Console.WriteLine("You selected wash.");
            hairServices.Add("wash");
            durationHairServices += new TimeSpan(00, 30, 00);
        } else if (userInput == "2")
        {
            Console.WriteLine("You selected cut.");
            hairServices.Add("cut");
            durationHairServices += new TimeSpan(01, 00, 00);
        } else if (userInput == "3")
        {
            Console.WriteLine("You selected dye.");
            hairServices.Add("dye");
            durationHairServices += new TimeSpan(01, 30, 00);
        } else
        {
            break;
        }
    }
}

void GetAllEmpoyeesForServices(List<string> hairServices)
{
    var validEmployees = new List<Employee>();

    foreach (var employee in new EmployeeRepository().GetAllEmployees())
    {
        var invariantText = employee.Specialization.ToUpperInvariant();
        bool matches = hairServices.All(hs => invariantText.Contains(hs.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(employee);
        }
    }

    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNobody can help you.");
    }
    else
    {
        Console.WriteLine("\nSelect which employee you want:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
        employeeId = Int32.Parse(Console.ReadLine());
    }
}

DateTime PickDateForAppointment()
{
    Console.WriteLine("In what day do you want the appointment?");
    var userInput = Console.ReadLine();
    DateTime dateOfAppointment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int32.Parse(userInput));
    return dateOfAppointment;
}

void GetAvailableTimeSpotsForEmployee(int employeeId, DateTime date, TimeSpan durationHairServices)
{
    Console.WriteLine("-> GetAvailableTimeSpotsForEmployee");
    Console.WriteLine(" --- ");
    var employeeName = new EmployeeRepository().GetEmployeeById(employeeId).Name;
    Console.WriteLine(employeeName);
    Console.WriteLine(date);
    Console.WriteLine(durationHairServices);
    Console.WriteLine(" --- ");

    var appointments = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("All appointments from the selected customer and the selected day:");
    foreach (var app in new AppointmentRepository().GetInWorkAppointmentsByEmployeeNameAndDate(employeeName, date))
    {
        appointments.Add((app.StartDate, app.EndDate));
        Console.WriteLine(app.CustomerName + " - " + app.EmployeeName + " - " + app.StartDate + " - " + app.EndDate);
    }

    var sortedAppointments = appointments.OrderBy(s => s.startDate);
    Console.WriteLine("All appointments from the selected customer and the selected day sorted:");
    foreach(var sortedApp in sortedAppointments)
    {
        Console.WriteLine(sortedApp.startDate + " - " + sortedApp.endDate);
    }

    // initialize just to don't get error
    DateTime dayStart = new DateTime();
    var dayFinish = new DateTime();
    foreach (var time in GetTimeByNameOfDay(date))
    {
        dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item1.Hours, time.Item1.Minutes, time.Item1.Seconds);
        dayFinish = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item2.Hours, time.Item2.Minutes, time.Item2.Seconds);
    }
    Console.WriteLine("time start + finish:");
    Console.WriteLine(dayStart + " - " + dayFinish);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(dayStart);
    foreach (var sortedApp in sortedAppointments)
    {
        possibleIntervals.Add(sortedApp.startDate);
        possibleIntervals.Add(sortedApp.endDate);
    }
    possibleIntervals.Add(dayFinish);
    Console.WriteLine("Possible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    Console.WriteLine("Check for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += durationHairServices) <= endOfInterval)
        {
            Console.WriteLine("good date:");
            Console.WriteLine(startOfInterval);

            // vreau sa salvez (start, start + durata), (start + durata, start + durata + durata), (start + durata + durata, start + durata + durata + durata), etc...
            validIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }

    //SelectDateForAppointment(validIntervals);
}

Tuple<DateTime, DateTime> SelectDateForAppointment(List<(DateTime startDate, DateTime endDate)>  validIntervals) {
    Console.WriteLine("Pick an interval for your appointment from this list:");
    for (int i = 0; i < validIntervals.Count; ++i)
    {
        Console.WriteLine(i + " -> " + validIntervals[i].startDate + " - " + validIntervals[i].endDate);
    }
    var userInput = Int32.Parse(Console.ReadLine());
    return Tuple.Create(validIntervals[userInput].startDate, validIntervals[userInput].endDate);
}

// employeeId, customerName, hairServices
void SaveAppointment(Tuple<DateTime, DateTime> pickedInterval, int employeeId, string customerName, List<string> hairServices)
{
    Console.WriteLine("Under what name you want to save the appointment?");
    customerName = Console.ReadLine();

    Console.WriteLine("--- start for testing ---");
    Console.WriteLine("pickedInterval= " + pickedInterval.Item1 + " - " + pickedInterval.Item2);
    Console.WriteLine("employeeId= " + employeeId);
    Console.WriteLine("customerName= " + customerName);
    Console.WriteLine("hairsevices:");
    foreach(var services in hairServices)
    {
        Console.WriteLine(services);
    }
    Console.WriteLine("--- end for testing ---");

    var app = new Appointment();
    app.CustomerName = customerName;
    app.EmployeeName = new EmployeeRepository().GetEmployeeById(employeeId).Name;
    for (int i = 0; i < hairServices.Count; ++i)
    {
        app.HairServiceName += hairServices[i];
        if (i < hairServices.Count - 1)
        {
            app.HairServiceName += ", ";
        }
    }
    app.StartDate = pickedInterval.Item1;
    app.EndDate = pickedInterval.Item2;
    new AppointmentRepository().CreateAppointment(app);

    // ??? Cum sa imi parsez aici obiectul, ca sa vad cum s-au salvat lucrurile in el? Nu ma ajuta, dar de curiozitate.
    // Asa am incercat dar erau ceva erori, adica nu puteam sa printez employeeName ci employeeId si nu inteleg de ce si proprietatile de startDate si endDate nici nu le gasea.
    //Console.WriteLine("@@@@@@@@@@@@@@@            merg prin obiectul in care am salvat:");
    //foreach(var ap in app.GetType().GetProperties())
    //{
    //    Console.WriteLine(ap.GetValue(customerName) + " - " + ap.GetValue(employeeId) + " - " + ap.GetValue(hairServices));
    //}

    // ??? Nu imi vad appointment-ul salvat in lista.
    Console.WriteLine("--- start for testing ---");
    Console.WriteLine("new list of appointments:");
    foreach (var apps in new AppointmentRepository().GetAllAppointments())
    {
        Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
    }
    Console.WriteLine("--- end for testing ---");
}

List<(TimeSpan, TimeSpan)> GetTimeByNameOfDay(DateTime date)
{
    Console.WriteLine("-> GetTimeByNameOfDay");
    var timeOfDay = new List<(TimeSpan startTime, TimeSpan endTime)>();
    var nameOfDay = date.ToString("dddd");
    Console.WriteLine("nameOfDay= " + nameOfDay);

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

/* COPY OF WORKING LAST VERSION:
using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// global variables
bool showMenu = true;
int cnt = 0; // ??? am nevoie de asta sau tre sa folosesc Console.Clear()?
List<string> hairServices = new List<string>();
TimeSpan durationHairServices = new TimeSpan(00, 00, 00);
int employeeId = 0;
var validIntervals = new List<(DateTime startDate, DateTime endDate)>();
string customerName = "";

while (showMenu) // (showMenu) == (showMenu == true)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    if (cnt++ == 0)
    {
        Console.WriteLine("What you wanna do?");
        Console.WriteLine("1 - Create Appointment");
        Console.WriteLine("2 - Read App");
        Console.WriteLine("3 - Update App");
        Console.WriteLine("4 - Delete App");
    }

    var userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            GetAllHairServices();
            PickHairServices();
            GetAllEmpoyeesForServices(hairServices);
            GetAvailableTimeSpotsForEmployee(employeeId, PickDateForAppointment(), durationHairServices);
            SaveAppointment(SelectDateForAppointment(validIntervals), employeeId, customerName, hairServices);
            return true;
        case "2":
            return false;
        case "3":
            return true;
        case "4":
            return true;
        default:
            return true;
    }
}

void GetAllHairServices()
{
    foreach (var s in HairService.GenerateHairServices())
    {
        Console.WriteLine($"'{s.ServiceName}' - '{s.Duration}' - '{s.Price}'");
    }
}

void PickHairServices()
{
    Console.WriteLine("\nWhat hair services you want? Type each one on a new line.");
    Console.WriteLine("1 - wash");
    Console.WriteLine("2 - cut");
    Console.WriteLine("3 - dye");
    Console.WriteLine("0 - exit");
    for (int i = 0; i <= HairService.GenerateHairServices().Count(); ++i)
    {
        var userInput = Console.ReadLine();
        if (userInput == "1")
        {
            Console.WriteLine("You selected wash.");
            hairServices.Add("wash");
            durationHairServices += new TimeSpan(00, 30, 00);
        } else if (userInput == "2")
        {
            Console.WriteLine("You selected cut.");
            hairServices.Add("cut");
            durationHairServices += new TimeSpan(01, 00, 00);
        } else if (userInput == "3")
        {
            Console.WriteLine("You selected dye.");
            hairServices.Add("dye");
            durationHairServices += new TimeSpan(01, 30, 00);
        } else
        {
            break;
        }
    }
}

void GetAllEmpoyeesForServices(List<string> hairServices)
{
    var validEmployees = new List<Employee>();

    foreach (var employee in new EmployeeRepository().GetAllEmployees())
    {
        var invariantText = employee.Specialization.ToUpperInvariant();
        bool matches = hairServices.All(hs => invariantText.Contains(hs.ToUpperInvariant()));
        if (matches)
        {
            validEmployees.Add(employee);
        }
    }

    if (validEmployees.Count() == 0)
    {
        Console.WriteLine("\nNobody can help you.");
    }
    else
    {
        Console.WriteLine("\nSelect which employee you want:");
        for (int i = 0; i < validEmployees.Count(); ++i)
        {
            Console.WriteLine(validEmployees[i].Id + " - " + validEmployees[i].Name);
        }
        employeeId = Int32.Parse(Console.ReadLine());
    }
}

DateTime PickDateForAppointment()
{
    Console.WriteLine("In what day do you want the appointment?");
    var userInput = Console.ReadLine();
    DateTime dateOfAppointment = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Int32.Parse(userInput));
    return dateOfAppointment;
}

void GetAvailableTimeSpotsForEmployee(int employeeId, DateTime date, TimeSpan durationHairServices)
{
    Console.WriteLine("-> GetAvailableTimeSpotsForEmployee");
    Console.WriteLine(" --- ");
    var employeeName = new EmployeeRepository().GetEmployeeById(employeeId).Name;
    Console.WriteLine(employeeName);
    Console.WriteLine(date);
    Console.WriteLine(durationHairServices);
    Console.WriteLine(" --- ");

    var appointments = new List<(DateTime startDate, DateTime endDate)>();
    Console.WriteLine("All appointments from the selected customer and the selected day:");
    foreach (var app in new AppointmentRepository().GetInWorkAppointmentsByEmployeeNameAndDate(employeeName, date))
    {
        appointments.Add((app.StartDate, app.EndDate));
        Console.WriteLine(app.CustomerName + " - " + app.EmployeeName + " - " + app.StartDate + " - " + app.EndDate);
    }

    var sortedAppointments = appointments.OrderBy(s => s.startDate);
    Console.WriteLine("All appointments from the selected customer and the selected day sorted:");
    foreach(var sortedApp in sortedAppointments)
    {
        Console.WriteLine(sortedApp.startDate + " - " + sortedApp.endDate);
    }

    // initialize just to don't get error
    DateTime dayStart = new DateTime();
    var dayFinish = new DateTime();
    foreach (var time in GetTimeByNameOfDay(date))
    {
        dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item1.Hours, time.Item1.Minutes, time.Item1.Seconds);
        dayFinish = new DateTime(DateTime.Now.Year, DateTime.Now.Month, date.Day, time.Item2.Hours, time.Item2.Minutes, time.Item2.Seconds);
    }
    Console.WriteLine("time start + finish:");
    Console.WriteLine(dayStart + " - " + dayFinish);

    var possibleIntervals = new List<DateTime>();
    possibleIntervals.Add(dayStart);
    foreach (var sortedApp in sortedAppointments)
    {
        possibleIntervals.Add(sortedApp.startDate);
        possibleIntervals.Add(sortedApp.endDate);
    }
    possibleIntervals.Add(dayFinish);
    Console.WriteLine("Possible intervals:");
    foreach (var intervals in possibleIntervals)
    {
        Console.WriteLine(intervals);
    }

    Console.WriteLine("Check for valid intervals:");
    for (int i = 0; i < possibleIntervals.Count - 1; i += 2)
    {
        var startOfInterval = possibleIntervals[i];
        var copy_startOfInterval = startOfInterval;
        var endOfInterval = possibleIntervals[i + 1];
        Console.WriteLine("startOfInterval= " + startOfInterval);
        Console.WriteLine("endOfInterval= " + endOfInterval);

        while ((startOfInterval += durationHairServices) <= endOfInterval)
        {
            Console.WriteLine("good date:");
            Console.WriteLine(startOfInterval);

            // vreau sa salvez (start, start + durata), (start + durata, start + durata + durata), (start + durata + durata, start + durata + durata + durata), etc...
            validIntervals.Add((copy_startOfInterval, startOfInterval));
            copy_startOfInterval = startOfInterval;
        }
    }

    //SelectDateForAppointment(validIntervals);
}

Tuple<DateTime, DateTime> SelectDateForAppointment(List<(DateTime startDate, DateTime endDate)>  validIntervals) {
    Console.WriteLine("Pick an interval for your appointment from this list:");
    for (int i = 0; i < validIntervals.Count; ++i)
    {
        Console.WriteLine(i + " -> " + validIntervals[i].startDate + " - " + validIntervals[i].endDate);
    }
    var userInput = Int32.Parse(Console.ReadLine());
    return Tuple.Create(validIntervals[userInput].startDate, validIntervals[userInput].endDate);
}

// employeeId, customerName, hairServices
void SaveAppointment(Tuple<DateTime, DateTime> pickedInterval, int employeeId, string customerName, List<string> hairServices)
{
    Console.WriteLine("-> SaveAppointment()");
    Console.WriteLine("Under what name you want to save the appointment?");
    var userInput = Console.ReadLine();
    customerName = userInput;

    Console.WriteLine("---");
    Console.WriteLine("pickedInterval= " + pickedInterval.Item1 + " - " + pickedInterval.Item2);
    Console.WriteLine("employeeId= " + employeeId);
    Console.WriteLine("customerName= " + customerName);
    Console.WriteLine("hairsevices:");
    foreach(var services in hairServices)
    {
        Console.WriteLine(services);
    }
    Console.WriteLine("---");

    var app = new Appointment();
    app.CustomerName = customerName;
    app.EmployeeName = new EmployeeRepository().GetEmployeeById(employeeId).Name;
    for (int i = 0; i < hairServices.Count; ++i)
    {
        app.HairServiceName += hairServices[i];
        if (i < hairServices.Count - 1)
        {
            app.HairServiceName += ", ";
        }
    }
    app.StartDate = pickedInterval.Item1;
    app.EndDate = pickedInterval.Item2;

    new AppointmentRepository().CreateAppointment(app);

    // ??? Cum sa imi parsez aici obiectul, ca sa vad cum s-au salvat lucrurile in el? Nu ma ajuta, dar de curiozitate.
    // Asa am incercat dar erau ceva erori, adica nu puteam sa printez employeeName ci employeeId si nu inteleg de ce si proprietatile de startDate si endDate nici nu le gasea.
    //Console.WriteLine("@@@@@@@@@@@@@@@            merg prin obiectul in care am salvat:");
    //foreach(var ap in app.GetType().GetProperties())
    //{
    //    Console.WriteLine(ap.GetValue(customerName) + " - " + ap.GetValue(employeeId) + " - " + ap.GetValue(hairServices));
    //}

    // ??? Nu imi vad appointment-ul salvat.
    Console.WriteLine("new list of appointments:");
    foreach (var apps in new AppointmentRepository().GetAllAppointments())
    {
        Console.WriteLine($"'{apps.CustomerName}', '{apps.EmployeeName}', '{apps.HairServiceName}', '{apps.StartDate}', '{apps.EndDate}'.");
    }
}

List<(TimeSpan, TimeSpan)> GetTimeByNameOfDay(DateTime date)
{
    Console.WriteLine("-> GetTimeByNameOfDay");
    var timeOfDay = new List<(TimeSpan startTime, TimeSpan endTime)>();
    var nameOfDay = date.ToString("dddd");
    Console.WriteLine("nameOfDay= " + nameOfDay);

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
*/