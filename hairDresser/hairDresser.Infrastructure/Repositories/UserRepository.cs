using hairDresser.Application.Interfaces;
using hairDresser.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            // exemplu:
            //.Include(customers => customers.Customer)
            //.Include(employees => employees.Employee)
            //.Include(appointmentHairServices => appointmentHairServices.AppointmentHairServices)
            //.ThenInclude(hairServices => hairServices.HairService)

            var allUsers = context.Users;
            //.Include(roles => roles.Role)

            return allUsers;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
