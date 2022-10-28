using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webAplication.Models;
using Microsoft.AspNetCore.Http.Abstractions;

namespace webAplication
{
    public class AccountService
    {
        private readonly DbSet<User> _users;
        private readonly ILogger<AccountService> _logger;

        public AccountService(DbSet<User> users, ILogger<AccountService> logger)
        {
            _users = users;
            _logger = logger;
        }

        /*public async Task<Respons<ClaimsIdentity>> GenerateNewUser(string loggin)
        {
            try
            {
                var user = await _users.SingleOrDefaultAsync(x => x.Login == loggin);
                if (user != null)
                {
                    return BadRequestResult();
                }
            }
            catch (Exception ex)
            {

            }


            throw new NotImplementedException();
        }*/
    }
}
