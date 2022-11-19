using hairDresser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hairDresser.Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);

        Task<IQueryable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        //Task<IQueryable> GetAllCustomersByRoleAsync();
        //Task<IQueryable> GetAllEmployeesByRoleAsync();
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
    }
}
