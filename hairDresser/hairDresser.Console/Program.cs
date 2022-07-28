using hairDresser.Domain.Models;

/*
Customer c = new Customer("Serb David", "serbdavid", "parola123", "serbdavid@yahoo.com", "+40763023012", "Timis");
Console.WriteLine(c);
Console.WriteLine(c.Password);
Console.WriteLine(c.Email);
*/

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

/*
Console.WriteLine("Enter an hour: ");
var hour = Console.ReadLine();

Console.WriteLine("Enter service: ");
string service = Console.ReadLine();
// CreateReadUpdateDelete -> C or R or U or D

int[] intervals = { 09, 11, 13, 17, 19 };
foreach (int i in intervals)
{
    // System.Console.Write("{0} ", i);
    System.Console.Write($"{i} ");
}
System.Console.Write("\n");

// ? de ce ii service null in cazul asta si in restul nu?
if (service.Equals('C'))
{
    System.Console.Write(service);
    foreach (int i in intervals)
    {
        if (i > Convert.ToInt32(hour))
        {
            System.Console.Write("{0} ", i);
        }

    }

}
else if (service.Equals('R'))
{
    System.Console.Write(service);

}
else if (service.Equals('U'))
{
    System.Console.Write(service + "\n");

}
else
{
    System.Console.Write(service);
}

System.Console.Write("\n");
foreach (int i in intervals)
{
    System.Console.Write("{0} ", i);
}
*/
