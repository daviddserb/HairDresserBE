using hairDresser.Domain.Models;

List<Employee> EmployeeList = new List<Employee>();
EmployeeList.Add(new Employee("Matei Dima", "wash, cut"));
EmployeeList.Add(new Employee("Onofras Rica", "cut, dye"));

Console.WriteLine(" Employees: ");
foreach (var employee in EmployeeList)
{
    Console.WriteLine($"name: '{employee.Name}', specialization: '{employee.Specialization}'.");
}

List<Customer> CustomerList = new List<Customer>();
CustomerList.Add(new Customer("Serb David", "serbdavid", "parola123", "serbdavid@yahoo.com", "+40763023012", "Timis"));
CustomerList.Add(new Customer("Adrian Marin", "adrianmarin", "parola321", "adrianmarin@yahoo.com", "+40783231930", "Constanta"));
CustomerList.Add(new Customer("Vlad Apetrica", "vladapetrica", "parola333", "vladapetrica@yahoo.com", "+40732012993", "Sighet"));
CustomerList.Add(new Customer("Mircea Ghita", "mirceaghita", "333parola", "mirceaghita@yahoo.com", "+40712023982", "Bucuresti"));

Console.WriteLine("\n Customers: ");
foreach (var customer in CustomerList)
{
    Console.WriteLine($"name: '{customer.Name}', username: '{customer.Username}', from: '{customer.Address}'.");
}

List<Appointment> AppointmentList = new List<Appointment>();
AppointmentList.Add(new Appointment("Adrian Marin", "Matei Dima", "cut, wash", new DateTime(2022, 08, 02, 14, 40, 10), new DateTime(2022, 08, 02, 16, 00, 00)));
AppointmentList.Add(new Appointment("Serb David", "Matei Dima", "cut", new DateTime(2022, 08, 02, 10, 38, 10), new DateTime(2022, 08, 02, 13, 38, 10)));
AppointmentList.Add(new Appointment("Vlad Apetrica", "Onofras Rica", "dye", new DateTime(2022, 08, 02, 13, 38, 10), new DateTime(2022, 08, 02, 15, 20, 10)));

Console.WriteLine("\n Appointments");
foreach (var appointment in AppointmentList)
{
    Console.WriteLine($"customerName: '{appointment.CustomerName}', employeeName: '{appointment.EmployeeName}', start: '{appointment.StartDate}', end: '{appointment.EndDate}'.");
}

//global variables:
string[] service = new string[3]; // the length of the service array (3) -> all the different specialization of each employee added together (wash + cut + dye)
var availableTimesForAppointment = new List<(DateTime startDate, DateTime endDate)>();

// start:
letCustomerChooseHairServices();
CreateAppointment(availableTimesForAppointment);

void letCustomerChooseHairServices()
{
    Console.WriteLine("\nHi customer, what service/services do you want from: wash, cut, dye??");
    for (int i = 0; i < service.Length; i++)
    {
        // Type (wash/cut/dye) each on a new line.
        service[i] = Console.ReadLine();
    }
    checkIfEmployeeValidForServices();
}

void checkIfEmployeeValidForServices()
{
    Console.WriteLine("    checkIfEmployeeValidForServices()");
    List<string> ValidEmployees = new List<string>();
    int cnt_notValidEmployee = 0;

    foreach (var employee in EmployeeList)
    {
        Console.WriteLine("employeeName: " + employee.Name);
        int cnt_validServices = 0;
        int cnt_validEmployee = 0;

        for (int i = 0; i < service.Length; i++)
        {
            if (!(String.IsNullOrEmpty(service[i])))
            {
                ++cnt_validServices;
            }
            if (employee.Specialization.Contains(service[i]) && !(String.IsNullOrEmpty(service[i])))
            {
                ++cnt_validEmployee;
            }
        }

        if (cnt_validServices == cnt_validEmployee)
        {
            ValidEmployees.Add(employee.Name);
        } else
        {
            ++cnt_notValidEmployee;
        }
    }

    if (cnt_notValidEmployee == EmployeeList.Count)
    {
        Console.WriteLine($" Sorry, can't help you because not a single employee can do all the hair services together.");
    } else
    {
        Console.WriteLine("All valid employees:");
        foreach (var validEmployees in ValidEmployees)
        {
            Console.WriteLine($"'{validEmployees}'");
        }
        Console.WriteLine("---");

        getListOfAppointmentsFromValidEmployees(ValidEmployees);
    }
}

void getListOfAppointmentsFromValidEmployees(List <string> ValidEmployees)
{
    Console.WriteLine("    getListOfAppointmentsFromValidEmployee()");

    foreach (var validEmployees in ValidEmployees)
    {
        Console.WriteLine(" @@@@@@@@@@@ valid employee name:");
        Console.WriteLine(validEmployees);
        List<(DateTime, DateTime)> ValidEmployeesAppointments = new List<(DateTime startDate, DateTime endDate)>();

        foreach (var appointments in AppointmentList)
        {
            if (appointments.EmployeeName.Contains(validEmployees)) {
                ValidEmployeesAppointments.Add((appointments.StartDate, appointments.EndDate));
            }
        }
        
        Console.WriteLine(" his current appointments: ");
        foreach (var employeeDates in ValidEmployeesAppointments)
        {
            Console.WriteLine($"startDate= '{employeeDates.Item1}' -> endDate= '{employeeDates.Item2}'");
        }

        var sorted_ValidEmployeesAppointments = ValidEmployeesAppointments.OrderBy(d => d.Item1);

        Console.WriteLine(" his current appointments SORTED:");
        foreach (var x in sorted_ValidEmployeesAppointments)
        {
            Console.WriteLine($"startDate= '{x.Item1}' -> endDate= '{x.Item2}'");
        }
        Console.WriteLine("---");

        saveAllDatesFromValidEmployee(sorted_ValidEmployeesAppointments.ToList());
    }
}

void saveAllDatesFromValidEmployee (List<(DateTime, DateTime)> sorted_ValidEmployeesAppointments)
{
    // dupa ce am sortat lista de appointment-uri de la employee-ul care ii poate face toate cerintele de hair services a lui customer
    // le voi salva intr-o lista, in felul urmator: start date of current day, lista sortata de appointment-uri (aici pot fi mai multe), end date of current day
    Console.WriteLine("    saveAllDatesFromValidEmployee()");

    // Get the current day of the week.
    DayOfWeek currentDay = DateTime.Today.DayOfWeek;

    var workingDays = WorkingDays.GenerateWorkingDays();
    var CurrentWorkingDay = workingDays.Find(wd => wd.Name.Equals(currentDay.ToString()));
    Console.WriteLine(CurrentWorkingDay);

    TimeSpan startTimeCurrentWorkingDay = CurrentWorkingDay.startTime;
    TimeSpan endTimeCurrentWorkingDay = CurrentWorkingDay.endTime;

    var todayStart = DateTime.Now.Date + startTimeCurrentWorkingDay;
    var todayFinish = DateTime.Now.Date + endTimeCurrentWorkingDay;
    Console.WriteLine("start: " + todayStart + "-> finish: " + todayFinish);

    List<DateTime> dates = new List<DateTime>();

    dates.Add(todayStart);
    foreach (var sortedAppointments in sorted_ValidEmployeesAppointments)
    {
        dates.Add(sortedAppointments.Item1);
        dates.Add(sortedAppointments.Item2);
    }
    dates.Add(todayFinish);

    Console.WriteLine(" start time of current day + all appointments sorted (can be multiple) + end time of current day: ");
    foreach (var d in dates)
    {
        Console.WriteLine(d);
    }
    Console.WriteLine("---");

    giveCustomerAvailableDates(dates);
}

void giveCustomerAvailableDates(List<DateTime> dates)
{
    Console.WriteLine("    giveCustomerAvailableDates()");

    Console.WriteLine("time for the customer hair services: ");
    Console.WriteLine(calculateDuration());

    for (int i = 0; i < dates.Count - 1; i += 2)
    {
        Console.WriteLine("possible dates for appointments: ");
        Console.WriteLine(dates[i]);
        Console.WriteLine(dates[i + 1]);

        Console.WriteLine("availableTimeForAppointment: ");
        var availableTimeForAppointment = dates[i + 1] - dates[i];
        Console.WriteLine(availableTimeForAppointment);

        var compare_TimeAvailable_TimeAppointment = TimeSpan.Compare(availableTimeForAppointment, calculateDuration());
        if (compare_TimeAvailable_TimeAppointment != -1)
        {
            availableTimesForAppointment.Add((dates[i], dates[i + 1]));
            Console.WriteLine(" BINGO: You can chose an appointment in this interval of time.");
        }
    }
    Console.WriteLine("---");
}


TimeSpan calculateDuration()
{
    TimeSpan totalDurationTime = new TimeSpan(00, 00, 00);
    foreach (var services in service)
    {
        if (services == "wash")
        {
            TimeSpan durationTimeForWash = new TimeSpan(00, 30, 00);
            totalDurationTime += durationTimeForWash;
        }
        else if (services == "cut")
        {
            TimeSpan durationTimeForCut = new TimeSpan(01, 00, 00);
            totalDurationTime += durationTimeForCut;
        }
        else if (services == "dye")
        {
            TimeSpan durationTimeForDye = new TimeSpan(01, 30, 00);
            totalDurationTime += durationTimeForDye;
        }
    }
    return totalDurationTime;
}

void CreateAppointment(List<(DateTime, DateTime)> availableTimesForAppointment)
{
    Console.WriteLine("    CreateAppointment()");
    Console.WriteLine(" all the possible intervals from all employees:");
    foreach (var app in availableTimesForAppointment)
    {
        Console.WriteLine(app);
    }

    Console.WriteLine(" Under what name do you want to save the appointment?");
    var customerName = Console.ReadLine();

    Console.WriteLine(" Type the full name of the employee:");
    var selectedEmployee = Console.ReadLine();

    Console.WriteLine(" Pick a interval and type it's position from the interval (starting from 0):");
    var selectedAppointment = Console.ReadLine();
    DateTime startAppointment = availableTimesForAppointment[Int32.Parse(selectedAppointment)].Item1;
    DateTime endAppointment = availableTimesForAppointment[Int32.Parse(selectedAppointment)].Item2;

    AppointmentList.Add(new Appointment(customerName, selectedEmployee, "yyy", startAppointment, endAppointment));

    Console.WriteLine(" Appointments updated:");
    foreach (var app in AppointmentList)
    {
        Console.WriteLine($"customerName: '{app.CustomerName}', employeeName: '{app.EmployeeName}', start: '{app.StartDate}', end: '{app.EndDate}'.");
    }
    Console.WriteLine("---");
}


/* Asta a fost pt. Assignment de la Exception.
try
{
    Console.WriteLine("try");
    Appointment app1 = new Appointment("Mircea", "Andrei", "wash", new DateTime(2022, 12, 31, 5, 10, 20), new DateTime(2022, 11, 29, 4, 10, 20));
    app1.CheckIfAppointmentValid(app1);
}
catch (InvalidAppointmentException ex)
{
    Console.WriteLine("catch");
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("set wrong dates");
}
*/

