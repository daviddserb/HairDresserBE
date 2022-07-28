using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Domain.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Customer(string name, string username, string password, string email, string phone, string address)
        {
            Name = name;
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // A Customer can request one or multiple hair services (some want maybe only a cut, but others maybe a cut and a dye, etc...).
        public void RequestHairServices()
        {

        }

        // Set the actual date and time.
        public void CreateAppointment()
        {

        }

        // View the appointment (??? probably in the Appointment's History...?)
        public void ReadAppointment()
        {

        }

        public void UpdateAppointment()
        {

        }

        public void DeleteAppointment()
        {

        }
    }
}
