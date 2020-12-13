using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.ContextData;
using TicketingAPI.DatabaseModels;
using TicketingAPI.Repositories.Interfaces;

namespace TicketingAPI.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ticketingContext context) : base(context) { }

        public async Task AddUser(User user)
        {
            Context.Users.Add(user);

            SaveAsync();
        }

        public Task<User> GetUserByLoginAsync(string login)
            => Context.Users.Where(x => EF.Functions.ILike(login, x.Login)).FirstOrDefaultAsync();
        
        public Task<User> GetUserByIdAsync(int id)
            => Context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

        public Task<User> GetUserByEmailAsync(string email)
            => Context.Users.Where(x => EF.Functions.ILike(email, x.Email)).FirstOrDefaultAsync();

    }
}
