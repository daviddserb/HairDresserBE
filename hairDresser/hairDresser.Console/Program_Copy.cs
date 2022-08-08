/*

using hairDresser.Domain.Models;
using hairDresser.Infrastructure.Repositories;

// global variables:

// get all appointments ->
var appointmentRepository = new AppointmentRepository();
Console.WriteLine("allAppointments: ");
foreach (var app in appointmentRepository.GetAllAppointments())
{
    Console.WriteLine($"customerName: '{app.CustomerName}', employeeName: '{app.EmployeeName}', start: '{app.StartDate}', end: '{app.EndDate}'.");
}

// Testing:
//var x = appointmentRepository.GetAllAppointmentsByCustomerName("Serb David");
//Console.WriteLine("GetAllAppointmentsByCustomerName");
//foreach (var e in x)
//{
//    Console.WriteLine($"CustomerName: '{e.CustomerName}', EmployeeName: '{e.EmployeeName}', StartDate: '{e.StartDate}'");
//}

//var y = appointmentRepository.GetInWorkAppointmentsByEmployeeName("Onofras Rica");
//Console.WriteLine("GetInWorkAppointmentsByEmployeeName");
//foreach (var e in y)
//{
//    Console.WriteLine($"CustomerName: '{e.CustomerName}', EmployeeName: '{e.EmployeeName}', StartDate: '{e.StartDate}'");
//}


// get all employees ->
var employeeRepository = new EmployeeRepository();
Console.WriteLine("employeeRepository: ");
foreach (var e in employeeRepository.GetAllEmployees())
{
    Console.WriteLine($"employeeName: '{e.Name}', specialization: '{e.Specialization}'");
}

// get all customers (acum nu cred ca ma ajuta) ->

string[] service = new string[3]; // (wash + cut + dye)

var availableIntervalsForAppointment = new List<(DateTime startDate, DateTime endDate)>();

// start:
CreateAppointment();

void CreateAppointment()
{
    // STEPS:
    // 1. Let the customer choose what hair services he wants.
    // 2. Based on what he chose, give him all the Employees names that are valid for him (which means that each one of them can do all of his requirements).
    // 3. Let the customer look through each Employee's available dates so he can pick a valid interval for the appointment.

    LetCustomerChooseHairServices();
    GetCurrentAppointmentsFromValidEmployees(CheckWhichEmployeesAreValid());
    //FinalizeAppointment(availableIntervalsForAppointment);
}

void LetCustomerChooseHairServices()
{
    Console.WriteLine("-> LetCustomerChooseHairServices()");

    Console.WriteLine("Hi customer, what services do you want from: wash, cut and dye?");
    for (int i = 0; i < service.Length; i++)
    {
        // Type (wash/cut/dye) each on a new line.
        service[i] = Console.ReadLine();
    }
}

List<string> CheckWhichEmployeesAreValid()
{
    Console.WriteLine("-> CheckWhichEmployeesAreValid()");

    List<string> ValidEmployees = new List<string>();
    int cnt_notValidEmployee = 0;

    foreach (var employee in employeeRepository.GetAllEmployees())
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
        }
        else
        {
            ++cnt_notValidEmployee;
        }
    }

    if (cnt_notValidEmployee == employeeRepository.GetAllEmployees().Count())
    {
        Console.WriteLine($"Sorry, can't help you, because not a single employee can do all the hair services together.");
    }
    else
    {
        Console.WriteLine("All valid employees:");
        foreach (var validEmployees in ValidEmployees)
        {
            Console.WriteLine($"'{validEmployees}'");
        }
        Console.WriteLine("---");
    }
    return ValidEmployees;
}

void GetCurrentAppointmentsFromValidEmployees(List<string> ValidEmployees)
{
    Console.WriteLine("-> getListOfAppointmentsFromValidEmployee()");

    foreach (var validEmployees in ValidEmployees)
    {
        List<(DateTime, DateTime)> ValidEmployeesAppointments = new List<(DateTime startDate, DateTime endDate)>();

        Console.WriteLine("\n@@@@@@@@@@@@@@@@@@@@@@@@ foreach repetitiv @@@@@@@@@@@@@@@@@");
        Console.WriteLine("valid employee name:");
        Console.WriteLine(validEmployees);

        foreach (var appointments in appointmentRepository.GetAllAppointments())
        {
            if (appointments.EmployeeName.Contains(validEmployees))
            {
                ValidEmployeesAppointments.Add((appointments.StartDate, appointments.EndDate));
            }
        }

        Console.WriteLine("current appointments from the current valid empoyee:");
        foreach (var employeeDates in ValidEmployeesAppointments)
        {
            Console.WriteLine($"startDate= '{employeeDates.Item1}' -> endDate= '{employeeDates.Item2}'");
        }

        GetPossibleIntervalsForAppointment(SortCurrentAppointmentsFromValidEmployees(ValidEmployeesAppointments));
    }
}

List<(DateTime, DateTime)> SortCurrentAppointmentsFromValidEmployees(List<(DateTime, DateTime)> ValidEmployeesAppointments)
{
    Console.WriteLine("-> SortCurrentAppointmentsFromValidEmployees()");
    var sorted_ValidEmployeesAppointments = ValidEmployeesAppointments.OrderBy(s => s.Item1);

    Console.WriteLine("sorted current appointments from the current valid empoyee:");
    foreach (var x in sorted_ValidEmployeesAppointments)
    {
        Console.WriteLine($"startDate= '{x.Item1}' -> endDate= '{x.Item2}'");
    }

    return sorted_ValidEmployeesAppointments.ToList();
}

void GetPossibleIntervalsForAppointment(List<(DateTime, DateTime)> sorted_ValidEmployeesAppointments)
{
    Console.WriteLine("-> GetPossibleIntervalsForAppointment()");

    var todayStart = DateTime.Now.Date + GetStartTimeOfCurrentWorkingDay();
    var todayFinish = DateTime.Now.Date + GetEndTimeOfCurrentWorkingDay();
    Console.WriteLine("start: " + todayStart + "-> finish: " + todayFinish);

    List<DateTime> possibleIntervals = new List<DateTime>();

    possibleIntervals.Add(todayStart);
    foreach (var sortedAppointments in sorted_ValidEmployeesAppointments)
    {
        possibleIntervals.Add(sortedAppointments.Item1);
        possibleIntervals.Add(sortedAppointments.Item2);
    }
    possibleIntervals.Add(todayFinish);

    Console.WriteLine(" start time of current day + all appointments sorted (can be multiple) + end time of current day: ");
    foreach (var p in possibleIntervals)
    {
        Console.WriteLine(p);
    }

    CheckWhichIntervalsAreValidForAppointments(possibleIntervals);
}

DayOfWeek GetCurrentDayOfTheWeek()
{
    //Console.WriteLine("-> GetCurrentDayOfTheWeek:");
    var currentDayOfTheWeek = DateTime.Today.DayOfWeek;
    return currentDayOfTheWeek;
}

WorkingDays GetCurrentDayOfTheWorkingDays()
{
    Console.WriteLine("-> GetCurrentDayOfTheWorkingDays:");
    var workingDays = WorkingDays.GenerateWorkingDays();

    var currentWorkingDay = workingDays.Find(cwd => cwd.Name.Equals(GetCurrentDayOfTheWeek().ToString()));
    return currentWorkingDay;
}

TimeSpan GetStartTimeOfCurrentWorkingDay()
{
    Console.WriteLine("-> GetStartTimeOfCurrentWorkingDay:");
    TimeSpan startTimeCurrentWorkingDay = GetCurrentDayOfTheWorkingDays().startTime;
    return startTimeCurrentWorkingDay;
}

TimeSpan GetEndTimeOfCurrentWorkingDay()
{
    Console.WriteLine("-> GetStartTimeOfCurrentWorkingDay:");
    TimeSpan endTimeCurrentWorkingDay = GetCurrentDayOfTheWorkingDays().endTime;
    return endTimeCurrentWorkingDay;
}

void CheckWhichIntervalsAreValidForAppointments(List<DateTime> dates)
{
    Console.WriteLine("-> CheckWhichIntervalsAreValidForAppointments()");

    Console.WriteLine("time for the customer hair services: ");
    Console.WriteLine(GetTotalDurationForCustomerHairServices());

    for (int i = 0; i < dates.Count - 1; i += 2)
    {
        Console.WriteLine("possible intervals for appointments:");
        Console.WriteLine(dates[i]);
        Console.WriteLine(dates[i + 1]);

        Console.WriteLine("available time for appointment:");
        var availableTimeForAppointment = dates[i + 1] - dates[i];
        Console.WriteLine(availableTimeForAppointment);

        var compareAvailableTimeWithAppointmentTime = TimeSpan.Compare(availableTimeForAppointment, GetTotalDurationForCustomerHairServices());
        if (compareAvailableTimeWithAppointmentTime != -1)
        {
            availableIntervalsForAppointment.Add((dates[i], dates[i + 1]));
            Console.WriteLine("############### BINGO ############# You can make an appointment in this interval of time.");
        }
    }
}

TimeSpan GetTotalDurationForCustomerHairServices()
{
    Console.WriteLine("-> GetTotalDurationForCustomerHairServices()");

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

//void FinalizeAppointment(List<(DateTime, DateTime)> availableIntervalsForAppointment)
//{
//    Console.WriteLine("-> FinalizeAppointment()");

//    Console.WriteLine(" all possible intervals from all employees:");
//    foreach (var app in availableIntervalsForAppointment)
//    {
//        Console.WriteLine(app);
//    }

//    Console.WriteLine(" Under what name do you want to save the appointment?");
//    var customerName = Console.ReadLine();

//    Console.WriteLine(" Type the full name of the employee:");
//    var selectedEmployee = Console.ReadLine();

//    Console.WriteLine(" Pick a interval and type it's position from the interval (starting from 0):");
//    var selectedAppointment = Console.ReadLine();
//    DateTime startAppointment = availableIntervalsForAppointment[Int32.Parse(selectedAppointment)].Item1;
//    DateTime endAppointment = availableIntervalsForAppointment[Int32.Parse(selectedAppointment)].Item2;

//    AppointmentList.Add(new Appointment(customerName, selectedEmployee, "yyy", startAppointment, endAppointment));

//    Console.WriteLine("Appointments updated:");
//    foreach (var app in AppointmentList)
//    {
//        Console.WriteLine($"customerName: '{app.CustomerName}', employeeName: '{app.EmployeeName}', start: '{app.StartDate}', end: '{app.EndDate}'.");
//    }
//}

//Asta a fost pt. Assignment de la Exception.
//try
//{
//    Console.WriteLine("try");
//    Appointment app1 = new Appointment("Mircea", "Andrei", "wash", new DateTime(2022, 12, 31, 5, 10, 20), new DateTime(2022, 11, 29, 4, 10, 20));
//    app1.CheckIfAppointmentValid(app1);
//}
//catch (InvalidAppointmentException ex)
//{
//    Console.WriteLine("catch");
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    Console.WriteLine("set wrong dates");
//}

*/