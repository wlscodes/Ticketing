using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(int id);

        public Task<User> GetUserByLoginAsync(string email);

        public Task<User> GetUserByEmailAsync(string login);

        public Task AddUser(User user);

        public Task<bool> UpdateUserRoleToAdmin(int id);

    }
}
